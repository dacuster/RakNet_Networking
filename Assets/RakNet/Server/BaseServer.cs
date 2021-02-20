using System.Net.Sockets;
using System.Text;
using UnityEngine;

/// <summary>
/// The base class of the server with basic functionality
/// </summary>
[DisallowMultipleComponent]
public class BaseServer : MonoBehaviour
{
    protected RakPeer peer = null;
    public bool IsRunning { get; private set; } = false;

    private void Initialize()
    {
        if (peer == null)
        {
            peer = new RakPeer();
            OnInitialized();
        }
    }

    /// <summary>
    /// Server is active and ready to starting/stopping
    /// </summary>
    public bool IsActive()
    {
        return peer != null && peer.IsActive();
    }

    /// <summary>
    /// Server start with address and port binding, password and maximum number of connections
    /// </summary>
    /// <param name="address">bind server to ip</param>
    /// <param name="port">bind server to port</param>
    /// <param name="password">connection password</param>
    /// <param name="max_connections">maximum clients</param>
    /// <param name="insecure">if true, the server will not use security features (encryption, etc.), it is recommended to set false to prevent packet interception</param>
    /// <returns></returns>
    public StartupResult StartServer(string address, ushort port, string password = "", ushort max_connections = 10, bool insecure = false)
    {
        StartupResult result = peer.StartServer(address, port, max_connections, insecure);

        if (result == StartupResult.RAKNET_STARTED)
        {
            peer.SetPassword(password);
            OnServerStart();
            IsRunning = true;
            return result;
        }
        OnServerStartFailed(result);
        return result;
    }

    /// <summary>
    /// Stopping a running server and breaking all connections
    /// </summary>
    public void StopServer()
    {
        if (peer.IsActive())
        {
            peer.Shutdown();
            OnServerStop();
            IsRunning = false;
        }
    }

    /// <summary>
    /// Enable or disable allowing frequent connections from the same IP adderss.
    /// </summary>
    public void SetLimitIPConnectionFrequency(bool value)
    {
        peer.SetLimitIPConnectionFrequency(value);
    }

    /// <summary>
    /// Sets the maximum number of incoming connections allowed.
    /// If the number of incoming connections is less than the number of players currently connected, no more players will be allowed to connect.
    /// If this is greater than the maximum number of peers allowed, it will be reduced to the maximum number of peers allowed.
    /// Defaults to 0, meaning by default, nobody can connect to you
    /// </summary>
    public void SetMaxConnections(ushort max_connections)
    {
        peer.SetMaxConnections(max_connections);
    }

    /// <summary>
    /// Sets the password for the incoming connections. (Leave the field empty to remove the password)
    /// </summary>
    public void SetPassword(string password)
    {
        peer.SetPassword(password);
    }

    public bool HasPassword()
    {
        return peer.HasPassword();
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        peer?.Destroy();
        OnShutdown();
    }

    /// <summary>
    /// Call this method along with your game loop, if you don't do this, you won't be able to receive messages
    /// </summary>
    public void ServerUpdate()
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
                        case DefaultMessageIDTypes.ID_NEW_INCOMING_CONNECTION:
                            OnConnected(guid);
                            break;

                        case DefaultMessageIDTypes.ID_DISCONNECTION_NOTIFICATION:
                            OnDisconnected(guid, DisconnectReason.ConnectionClosed);
                            break;

                        case DefaultMessageIDTypes.ID_CONNECTION_LOST:
                            OnDisconnected(guid, DisconnectReason.ConnectionLost);
                            break;
                    }
                }
                else if (packet_id >= DefaultMessageIDTypes.ID_USER_PACKET_ENUM)
                {
                    bitStream.ResetReadPointer();
                    OnSerializeData(bitStream, guid, packet_size);
                }
            }
        }
    }

    /// <summary>
    /// Getting a client GUID by index
    /// </summary>
    public ulong GetGUIDFromIndex(uint index)
    {
        if (peer == null)
            return 0;

        return peer.GetGUIDFromIndex(index);
    }

    /// <summary>
    /// Getting the index from the client's GUID
    /// </summary>
    public uint GetIndexFromGUID(ulong guid)
    {
        if (peer == null)
            return 0;

        return peer.GetIndexFromGUID(guid);
    }

    /// <summary>
    /// Current number of connections.
    /// </summary>
    public uint NumberOfConnections()
    {
        if (peer == null)
            return 0;

        return peer.NumberOfConnections();
    }

    /// <summary>
    /// Total number of connections we are allowed.
    /// </summary>
    public uint GetMaximumConnections()
    {
        if (peer == null)
            return 0;

        return peer.GetMaximumConnections();
    }

    public void AddBanIP(string address, int seconds = 60)
    {
        peer?.AddBanIP(address, seconds);
    }

    public void RemoveBanIP(string address)
    {
        peer?.RemoveBanIP(address);
    }

    public bool IsBannedIP(string address)
    {
        return peer != null ? peer.IsBannedIP(address) : false;
    }

    /// <summary>
    /// Closing the connection with the client and specifying the reason (it is possible to close the connection without informing the client that the connection is closed)
    /// </summary>
    /// <param name="guid">client GUID</param>
    /// <param name="send_disconnect_notification">Tell the client that the server is closing the connection</param>
    /// <param name="text">reason</param>
    public void CloseConnection(ulong guid, bool send_disconnect_notification = true)
    {
        peer.CloseConnection(guid, send_disconnect_notification);
    }

    /// <summary>
    /// Sending data from a bitstream to a client using priorities, reliability, and a channel
    /// </summary>
    public void SendToClient(BitStream stream, ulong guid, PacketPriority priority = PacketPriority.IMMEDIATE_PRIORITY, PacketReliability reliability = PacketReliability.RELIABLE, byte channel = 0)
    {
        if (!peer.IsActive())
            return;

        peer.SendToClient(stream, guid, priority, reliability, channel);
    }

    /// <summary>
    /// Sending data from a bitstream to all clients using priorities, reliability, and a channel
    /// </summary>
    public void SendToAll(BitStream stream, PacketPriority priority = PacketPriority.IMMEDIATE_PRIORITY, PacketReliability reliability = PacketReliability.RELIABLE, byte channel = 0)
    {
        if (!peer.IsActive())
            return;

        peer.SendToAll(stream, priority, reliability, channel);
    }

    /// <summary>
    /// Sending data from the bitstream to all clients ignoring the specified client guid using priority, reliability and channel
    /// </summary>
    public void SendToAllIgnore(BitStream stream, ulong ignore_guid, PacketPriority priority = PacketPriority.IMMEDIATE_PRIORITY, PacketReliability reliability = PacketReliability.RELIABLE, byte channel = 0)
    {
        if (!peer.IsActive())
            return;

        peer.SendToAllIgnore(stream, ignore_guid, priority, reliability, channel);
    }

    /// <summary>
    /// Allow clients to query information about the server?
    /// </summary>
    public void AllowQuery(bool enabled)
    {
        if (!peer.IsActive())
            return;

        peer.AllowQuery(enabled);
    }

    /// <summary>
    /// Are clients allowed to query information?
    /// </summary>
    /// <returns></returns>
    public bool IsQueryAllowed()
    {
        if (!peer.IsActive())
            return false;

        return peer.IsQueryAllowed();
    }

    /// <summary>
    /// Set data to send to the requesting client as an array of bytes
    /// </summary>
    public void SetQueryResponce(byte[] data)
    {
        peer.SetQueryResponce(data);
    }

    /// <summary>
    /// The average ping between the server and the client
    /// </summary>
    public string GetAddress(ulong guid, bool with_port = false)
    {
        return peer.GetAddress(guid, with_port);
    }

    /// <summary>
    /// The average ping between the server and the client
    /// </summary>
    public int GetAveragePing(ulong guid)
    {
        return peer.GetAveragePing(guid);
    }

    /// <summary>
    /// The last ping between the server and the client
    /// </summary>
    public int GetLastPing(ulong guid)
    {
        return peer.GetLastPing(guid);
    }

    /// <summary>
    /// The lowest ping between the server and the client
    /// </summary>
    public int GetLowestPing(ulong guid)
    {
        return peer.GetLowestPing(guid);
    }

    /// <summary>
    /// Get statistics per second on index
    /// </summary>
    public ulong GetStatisticsLastSecond(uint index, RNSPerSecondMetrics metrics)
    {
        return peer.GetStatisticsLastSecond(index, metrics);
    }

    /// <summary>
    /// Get statistics total on index
    /// </summary>
    public ulong GetStatisticsTotal(uint index, RNSPerSecondMetrics metrics)
    {
        return peer.GetStatisticsTotal(index, metrics);
    }

    /// <summary>
    /// Get statistics full on index
    /// </summary>
    public bool GetStatisticsFull(uint index, out RakNetStatistics statistics)
    {
        return peer.GetStatisticsFull(index, out statistics);
    }

    public ConnectionState GetConnectionState(ulong guid)
    {
        return peer.GetConnectionState(guid);
    }

    public virtual void OnInitialized() { }
    public virtual void OnShutdown() { }
    public virtual void OnServerStart() { }
    public virtual void OnServerStop() { }
    public virtual void OnServerStartFailed(StartupResult result) { }
    public virtual void OnConnected(ulong guid) { }
    public virtual void OnDisconnected(ulong guid, DisconnectReason reason) { }

    /// <summary>
    /// When the server receives data from the client it returns you a bitstream for reading the data and a unique sender GUID
    /// </summary>
    public virtual void OnSerializeData(BitStream bitStream, ulong guid, int packet_size) { }
}
