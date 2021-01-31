using System;
using UnityEngine;

/// <summary>
/// Basic client class with basic functionality
/// </summary>
[DisallowMultipleComponent]
public class BaseClient : MonoBehaviour
{
    protected RakPeer peer = null;
    
    public bool IsConnecting { get; private set; } = false;
    public bool IsConnected { get; private set; } = false;

    public ulong ClientGUID { get; private set; } = 0;
    public ulong ServerGUID { get; private set; } = 0;

    private void Initialize()
    {
        if (peer == null)
        {
            peer = new RakPeer();
            OnInitialized();
        }
    }

    private void Destroy()
    {
        peer?.Destroy();
        OnShutdown();
    }

    /// <summary>
    /// Client is active and ready to connect/disconnect
    /// </summary>
    public bool IsActive()
    {
        return peer != null && peer.IsActive();
    }

    /// <summary>
    /// Connecting to the server by IP address, specifying the password for connection and connection attempts (All connection errors will be returned by the OnDisconnected method)
    /// </summary>
    public ConnectionAttemptResult Connect(string address, ushort port, string password = "", short attempts = 20)
    {
        ConnectionAttemptResult result = peer.StartClient(address, port, password, attempts);
        if (result == ConnectionAttemptResult.CONNECTION_ATTEMPT_STARTED)
        {
            IsConnecting = true;
            IsConnected = false;
            OnConnecting(address, port, password);
        }
        return result;
    }

    /// <summary>
    /// Disconnecting from the server
    /// </summary>
    public void Disconnect()
    {
        if (peer.IsActive())
        {
            IsConnecting = false;
            IsConnected = false;
            peer.Shutdown();
            OnDisconnected(DisconnectReason.ByUser);
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        Destroy();
    }

    /// <summary>
    /// Call this method along with your game loop, if you don't do this, you won't be able to receive messages
    /// </summary>
    public void ClientUpdate()
    {
        if (IsActive())
        {
            while (peer.HasReceived(out BitStream bitStream, out ulong guid, out int packet_size))
            {
                DefaultMessageIDTypes packet_id = (DefaultMessageIDTypes)bitStream.ReadByte();

                if (packet_id < DefaultMessageIDTypes.ID_USER_PACKET_ENUM)
                {
                    switch (packet_id)
                    {
                        case DefaultMessageIDTypes.ID_CONNECTION_REQUEST_ACCEPTED:
                            IsConnecting = false;
                            IsConnected = true;
                            ClientGUID = peer.GetMyGUID();
                            ServerGUID = guid;
                            OnConnected();
                            break;

                        case DefaultMessageIDTypes.ID_DISCONNECTION_NOTIFICATION:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.ConnectionClosed);
                            break;

                        case DefaultMessageIDTypes.ID_CONNECTION_LOST:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.ConnectionLost);
                            break;

                        case DefaultMessageIDTypes.ID_CONNECTION_BANNED:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.IsBanned);
                            break;

                        case DefaultMessageIDTypes.ID_INCOMPATIBLE_PROTOCOL_VERSION:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.IncompatibleProtocol);
                            break;

                        case DefaultMessageIDTypes.ID_INVALID_PASSWORD:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.InvalidPassword);
                            break;

                        case DefaultMessageIDTypes.ID_PUBLIC_KEY_MISMATCH:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.SecurityError);
                            break;

                        case DefaultMessageIDTypes.ID_REMOTE_SYSTEM_REQUIRES_PUBLIC_KEY:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.SecurityError);
                            break;

                        case DefaultMessageIDTypes.ID_OUR_SYSTEM_REQUIRES_SECURITY:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.SecurityError);
                            break;

                        case DefaultMessageIDTypes.ID_CONNECTION_ATTEMPT_FAILED:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.AttemptFailed);
                            break;

                        case DefaultMessageIDTypes.ID_NO_FREE_INCOMING_CONNECTIONS:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.ServerIsFull);
                            break;

                        case DefaultMessageIDTypes.ID_IP_RECENTLY_CONNECTED:
                            IsConnecting = IsConnected = false;
                            peer.Shutdown();
                            OnDisconnected(DisconnectReason.ConnectionRecently);
                            break;
                    }
                }
                else if (packet_id >= DefaultMessageIDTypes.ID_USER_PACKET_ENUM)
                {
                    bitStream.ResetReadPointer();
                    OnSerializeData(bitStream, packet_size);
                }
            }
        }
    }

    /// <summary>
    /// Sending data from a bitstream to server using priorities, reliability, and a channel
    /// </summary>
    public void SendToServer(BitStream stream, PacketPriority priority = PacketPriority.IMMEDIATE_PRIORITY, PacketReliability reliability = PacketReliability.RELIABLE, byte channel = 0)
    {
        peer.SendToServer(stream, priority, reliability, channel);
    }

    /// <summary>
    /// The average ping between the client and the server
    /// </summary>
    public int GetAveragePing()
    {
        return peer.GetAveragePing(peer.GetGUIDFromIndex(0));
    }

    /// <summary>
    /// The last ping between the client and the server
    /// </summary>
    public int GetLastPing()
    {
        return peer.GetLastPing(peer.GetGUIDFromIndex(0));
    }

    /// <summary>
    /// The lowest ping between the client and the server
    /// </summary>
    public int GetLowestPing()
    {
        return peer.GetLowestPing(peer.GetGUIDFromIndex(0));
    }

    /// <summary>
    /// Get statistics per second
    /// </summary>
    public ulong GetStatisticsLastSecond(RNSPerSecondMetrics metrics)
    {
        return peer.GetStatisticsLastSecond(0, metrics);
    }

    /// <summary>
    /// Get statistics total
    /// </summary>
    public ulong GetStatisticsTotal(RNSPerSecondMetrics metrics)
    {
        return peer.GetStatisticsTotal(0, metrics);
    }

    /// <summary>
    /// Get statistics full
    /// </summary>
    public bool GetStatisticsFull(out RakNetStatistics statistics)
    {
        return peer.GetStatisticsFull(0, out statistics);
    }

    public ConnectionState GetConnectionState(ulong guid)
    {
        return peer.GetConnectionState(guid);
    }

    public virtual void OnInitialized() { }
    public virtual void OnShutdown() { }
    public virtual void OnConnecting(string address, ushort port, string password) { }
    public virtual void OnConnected() { }
    public virtual void OnDisconnected(DisconnectReason reason) { }

    /// <summary>
    /// When the client receives data from the server it returns you the bitstream for reading the data and the size of the incoming network packet
    /// </summary>
    public virtual void OnSerializeData(BitStream bitStream, int packet_size) { }
}
