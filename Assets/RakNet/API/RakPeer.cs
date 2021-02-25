using System;
using System.Runtime.InteropServices;
using static RakNetDLL;

/// <summary>
/// We strongly do not recommend using methods from this class! MAY CAUSE THE APP TO CRASH
/// </summary>
public unsafe class RakPeer_Native
{
    [DllImport(DLL_NAME)]
    public static extern bool NET_IsActive(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern IntPtr NET_Create();

    [DllImport(DLL_NAME)]
    public static extern StartupResult NET_Startup(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern void NET_Destroy(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern void NET_Shutdown(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern StartupResult NET_StartServer(IntPtr instance_ptr, string address, ushort port, ushort max_connections = 32, bool insecure = false);

    [DllImport(DLL_NAME)]
    public static extern void NET_SetMaxConnections(IntPtr instance_ptr, ushort max_connections);

    [DllImport(DLL_NAME)]
    public static extern void NET_SetPassword(IntPtr instance_ptr, string password);

    [DllImport(DLL_NAME)]
    public static extern bool NET_HasPassword(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern ConnectionAttemptResult NET_StartClient(IntPtr instance_ptr, string address, ushort port, string password = "", short attempts = 3);

    [DllImport(DLL_NAME)]
    public static extern int NET_ReceiveCount(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern bool NET_Receive(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern IntPtr NET_Packet(IntPtr instance_ptr, ref ulong guid, ref int packet_size);

    [DllImport(DLL_NAME)]
    public static extern ulong NET_MyGUID(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern void NET_AddBanIP(IntPtr instance_ptr, string address, int seconds);

    [DllImport(DLL_NAME)]
    public static extern void NET_RemoveBanIP(IntPtr instance_ptr, string address);

    [DllImport(DLL_NAME)]
    public static extern bool NET_IsBannedIP(IntPtr instance_ptr, string address);

    [DllImport(DLL_NAME)]
    public static extern int NET_GetAveragePing(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern int NET_GetLastPing(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern int NET_GetLowestPing(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern int NET_GetAveragePing(IntPtr instance_ptr, ulong guid);

    [DllImport(DLL_NAME)]
    public static extern int NET_GetLastPing(IntPtr instance_ptr, ulong guid);

    [DllImport(DLL_NAME)]
    public static extern int NET_GetLowestPing(IntPtr instance_ptr, ulong guid);

    [DllImport(DLL_NAME)]
    public static extern void NET_LimitBandwidth(IntPtr instance_ptr, uint bitsPerSecond);

    [DllImport(DLL_NAME)]
    public static extern void NET_SetLimitIPConnectionFrequency(IntPtr instance_ptr, bool value);

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr NET_GetAddress(IntPtr instance_ptr, ulong guid, bool with_port);

    [DllImport(DLL_NAME)]
    public static extern ushort NET_GetPort(IntPtr instance_ptr, ulong guid);

    [DllImport(DLL_NAME)]
    public static extern ushort NET_NumberOfConnections(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern ulong NET_GetGUIDFromIndex(IntPtr instance_ptr, uint index);

    [DllImport(DLL_NAME)]
    public static extern uint NET_GetIndexFromGUID(IntPtr instance_ptr, ulong guid);

    [DllImport(DLL_NAME)]
    public static extern uint NET_GetMaximumConnections(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern void NET_CloseConnection(IntPtr instance_ptr, ulong guid, bool send_disconnection_notification = true);

    [DllImport(DLL_NAME)]
    public static extern byte NETSND_ToServer(IntPtr instance_ptr, IntPtr bitstream_ptr, PacketPriority priority, PacketReliability reliablitity, int channel);

    [DllImport(DLL_NAME)]
    public static extern byte NETSND_ToClient(IntPtr instance_ptr, IntPtr bitstream_ptr, ulong guid, PacketPriority priority, PacketReliability reliablitity, int channel);

    [DllImport(DLL_NAME)]
    public static extern void NETSND_ToAddress(IntPtr instance_ptr, byte[] data, int length, string target_address, ushort target_port);

    [DllImport(DLL_NAME)]
    public static extern byte NETSND_ToAll(IntPtr instance_ptr, IntPtr bitstream_ptr, PacketPriority priority, PacketReliability reliablitity, int channel);

    [DllImport(DLL_NAME)]
    public static extern byte NETSND_ToAllIgnore(IntPtr instance_ptr, ulong ignore_guid, IntPtr bistream_ptr, PacketPriority priority, PacketReliability reliablitity, int channel);

    [DllImport(DLL_NAME)]
    public static extern void NET_AllowQuery(IntPtr instance_ptr, bool enabled);

    [DllImport(DLL_NAME)]
    public static extern bool NET_IsQueryAllowed(IntPtr instance_ptr);

    [DllImport(DLL_NAME)]
    public static extern void NET_SetQueryResponce(IntPtr instance_ptr, byte[] data);

    [DllImport(DLL_NAME)]
    public static extern ulong NET_GetStatisticsLastSeconds(IntPtr instance_ptr, uint index, RNSPerSecondMetrics metrics);

    [DllImport(DLL_NAME)]
    public static extern ulong NET_GetStatisticsTotal(IntPtr instance_ptr, uint index, RNSPerSecondMetrics metrics);

    [DllImport(DLL_NAME)]
    public static extern bool NET_Statistics(IntPtr instance_ptr, uint index, ref RakNetStatistics statistics);

    [DllImport(DLL_NAME)]
    public static extern ConnectionState NET_ConnectionState(IntPtr instance_ptr, ulong guid);

    [DllImport(DLL_NAME)]
    public static extern bool NET_Ping(IntPtr instance_ptr, string address, ushort port, bool onlyReplyOnAcceptingConnections = false);


    /* PLUGINS FEATURE */

    [DllImport(DLL_NAME)]
    public static extern void NET_AttachPlugin(IntPtr instance_ptr, IntPtr plugin_ptr);

    [DllImport(DLL_NAME)]
    public static extern void NET_DetachPlugin(IntPtr instance_ptr, IntPtr plugin_ptr);
}

public enum PeerType
{
    Unknown,
    Client,
    Server
}

/// <summary>
/// The main class for creating a client / server and managing connections (TO CREATE OR CONNECT, USE THE APPROPRIATE CLASSES)
/// </summary>
public class RakPeer : IDisposable
{
    public IntPtr pointer { get; private set; } = IntPtr.Zero;
    public PeerType Type { get; private set; } = PeerType.Unknown;

    public RakPeer()
    {
        pointer = RakPeer_Native.NET_Create();
    }

    #region Disposing
    private bool disposed = false;
    protected void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Destroy();
            }
            disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~RakPeer()
    {
        Dispose(false);
    }
    #endregion

    public void Destroy()
    {
        Shutdown();

        if (pointer != IntPtr.Zero)
        {
            RakPeer_Native.NET_Destroy(pointer);
        }
    }

    public StartupResult Startup()
    {
        return RakPeer_Native.NET_Startup(pointer);
    }

    StartupResult StartupResult;

    public StartupResult StartServer(string address, ushort port, ushort max_connections = 32, bool insecure = false)
    {
        StartupResult = RakPeer_Native.NET_StartServer(pointer, address, port, max_connections, insecure);

        if (StartupResult == StartupResult.RAKNET_STARTED)
        {
            Type = PeerType.Server;
            is_shutteddown = false;
        }

        return StartupResult;
    }

    public void SetMaxConnections(ushort max_connections)
    {
        RakPeer_Native.NET_SetMaxConnections(pointer, max_connections);
    }

    public void SetPassword(string password)
    {
        RakPeer_Native.NET_SetPassword(pointer, password);
    }

    public bool HasPassword()
    {
        return RakPeer_Native.NET_HasPassword(pointer);
    }

    ConnectionAttemptResult ConnectionAttemptResult;

    public ConnectionAttemptResult StartClient(string address, ushort port, string password = "", short attempts = 10)
    {
        ConnectionAttemptResult = RakPeer_Native.NET_StartClient(pointer, address, port, password, attempts);

        if (ConnectionAttemptResult == ConnectionAttemptResult.CONNECTION_ATTEMPT_STARTED)
        {
            Type = PeerType.Client;
            is_shutteddown = false;
        }

        return ConnectionAttemptResult;
    }

    //  NEEED FIX!!!! CHECK NATIVE CODE!!!!
    private bool is_shutteddown = false;//Damn knows what the mistake is, but I'm too lazy to fix it... (See Shutdown())

    public void Shutdown()
    {
        if (pointer == IntPtr.Zero || is_shutteddown)
            return;

        is_shutteddown = true;
        RakPeer_Native.NET_Shutdown(pointer);
    }

    public void SetLimitBandwidth(uint bytesPerSecond)
    {
        if (Type != PeerType.Server)
            return;

        RakPeer_Native.NET_LimitBandwidth(pointer, bytesPerSecond * 8);
    }

    public void SetLimitIPConnectionFrequency(bool value)
    {
        RakPeer_Native.NET_SetLimitIPConnectionFrequency(pointer, value);
    }

    public void AddBanIP(string address, int seconds = 60)
    {
        RakPeer_Native.NET_AddBanIP(pointer, address, seconds);
    }

    public void RemoveBanIP(string address)
    {
        RakPeer_Native.NET_RemoveBanIP(pointer, address);
    }

    public bool IsBannedIP(string address)
    {
        return RakPeer_Native.NET_IsBannedIP(pointer, address);
    }

    public ushort NumberOfConnections()
    {
        return RakPeer_Native.NET_NumberOfConnections(pointer);
    }

    public ulong GetGUIDFromIndex(uint index)
    {
        return RakPeer_Native.NET_GetGUIDFromIndex(pointer, index);
    }

    public uint GetIndexFromGUID(ulong guid)
    {
        return RakPeer_Native.NET_GetIndexFromGUID(pointer, guid);
    }

    public uint GetMaximumConnections()
    {
        return RakPeer_Native.NET_GetMaximumConnections(pointer);
    }

    public void CloseConnection(ulong guid, bool send_disconnect_notification = true)
    {
        RakPeer_Native.NET_CloseConnection(pointer, guid, send_disconnect_notification);
    }

    public bool IsActive()
    {

        if (pointer == IntPtr.Zero)
        {
            return false;
        }

        return RakPeer_Native.NET_IsActive(pointer);
    }

    BitStream bitStream = new BitStream();

    public bool HasReceived(out BitStream _bitStream, out ulong guid, out int packet_size)
    {
        IntPtr packet_ptr = IntPtr.Zero;
        _bitStream = bitStream;
        guid = 0;
        packet_size = 0;
        if (RakPeer_Native.NET_Receive(pointer))
        {
            packet_ptr = RakPeer_Native.NET_Packet(pointer, ref guid, ref packet_size);
            if (packet_ptr != IntPtr.Zero && _bitStream != null && _bitStream.pointer != IntPtr.Zero)
            {
                _bitStream.ReadPacket(packet_ptr);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public int ReceiveCount()
    {
        if (pointer == IntPtr.Zero)
            return 0;

        return RakPeer_Native.NET_ReceiveCount(pointer);
    }

    public void SendToAddress(byte[] data, string target_address, ushort target_port)
    {
        if (pointer != IntPtr.Zero)
        {
            RakPeer_Native.NETSND_ToAddress(pointer, data, data.Length, target_address, target_port);
        }
    }

    public byte SendToClient(BitStream stream, ulong guid, PacketPriority priority = PacketPriority.IMMEDIATE_PRIORITY, PacketReliability reliability = PacketReliability.RELIABLE, byte channel = 0)
    {
        if (pointer != IntPtr.Zero && stream != null && stream.pointer != IntPtr.Zero)
        {
            return RakPeer_Native.NETSND_ToClient(pointer, stream.pointer, guid, priority, reliability, channel);
        }
        return 0;
    }

    public byte SendToAll(BitStream stream, PacketPriority priority = PacketPriority.IMMEDIATE_PRIORITY, PacketReliability reliability = PacketReliability.RELIABLE, byte channel = 0)
    {
        if (pointer != IntPtr.Zero && stream != null && stream.pointer != IntPtr.Zero)
        {
            return RakPeer_Native.NETSND_ToAll(pointer, stream.pointer, priority, reliability, channel);
        }
        return 0;
    }

    public byte SendToAllIgnore(BitStream stream, ulong ignore_guid, PacketPriority priority = PacketPriority.IMMEDIATE_PRIORITY, PacketReliability reliability = PacketReliability.RELIABLE, byte channel = 0)
    {
        if (pointer != IntPtr.Zero && stream != null && stream.pointer != IntPtr.Zero)
        {
            return RakPeer_Native.NETSND_ToAllIgnore(pointer, ignore_guid, stream.pointer, priority, reliability, channel);
        }
        return 0;
    }

    public void AllowQuery(bool enabled)
    {
        if (pointer != IntPtr.Zero)
        {
            RakPeer_Native.NET_AllowQuery(pointer, enabled);
        }
    }

    public bool IsQueryAllowed()
    {
        if (pointer != IntPtr.Zero)
        {
            return RakPeer_Native.NET_IsQueryAllowed(pointer);
        }

        return false;
    }

    public void SetQueryResponce(byte[] data)
    {
        if (pointer != IntPtr.Zero)
        {
            RakPeer_Native.NET_SetQueryResponce(pointer, data);
        }
    }

    public byte SendToServer(BitStream stream, PacketPriority priority = PacketPriority.IMMEDIATE_PRIORITY, PacketReliability reliability = PacketReliability.RELIABLE, byte channel = 0)
    {
        if (pointer != IntPtr.Zero && stream != null && stream.pointer != IntPtr.Zero)
        {
            return RakPeer_Native.NETSND_ToServer(pointer, stream.pointer, priority, reliability, channel);
        }
        return 0;
    }

    public ulong GetMyGUID()
    {
        return RakPeer_Native.NET_MyGUID(pointer);
    }

    public int GetAveragePing(ulong guid = 0)
    {
        return RakPeer_Native.NET_GetAveragePing(pointer, guid);
    }

    public int GetLastPing(ulong guid = 0)
    {
        return RakPeer_Native.NET_GetLastPing(pointer, guid);
    }

    public int GetLowestPing(ulong guid = 0)
    {
        return RakPeer_Native.NET_GetLowestPing(pointer, guid);
    }

    public string GetAddress(ulong guid = 0, bool with_port = false)
    {
        if (pointer == IntPtr.Zero)
            return string.Empty;

        IntPtr ptr = RakPeer_Native.NET_GetAddress(pointer, guid, with_port);

        if (ptr != IntPtr.Zero)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }
        else
        {
            return string.Empty;
        }
    }

    public ushort GetPort(ulong guid = 0)
    {
        return RakPeer_Native.NET_GetPort(pointer, guid);
    }

    public ulong GetStatisticsLastSecond(uint index, RNSPerSecondMetrics metrics)
    {
        return RakPeer_Native.NET_GetStatisticsLastSeconds(pointer, index, metrics);
    }

    public ulong GetStatisticsTotal(uint index, RNSPerSecondMetrics metrics)
    {
        return RakPeer_Native.NET_GetStatisticsTotal(pointer, index, metrics);
    }

    public bool GetStatisticsFull(uint index, out RakNetStatistics _statistics)
    {
        _statistics = new RakNetStatistics();
        return RakPeer_Native.NET_Statistics(pointer, index, ref _statistics);
    }

    public ConnectionState GetConnectionState(ulong guid)
    {
        return RakPeer_Native.NET_ConnectionState(pointer, guid);
    }

    public bool Ping(string address, ushort port, bool onlyReplyOnAcceptingConnections = false)
    {
        StartupResult result = Startup();

        if (result == StartupResult.RAKNET_STARTED || result == StartupResult.RAKNET_ALREADY_STARTED)
        {
            return RakPeer_Native.NET_Ping(pointer, address, port, onlyReplyOnAcceptingConnections);
        }
        else
        {
            return false;
        }
    }
}
