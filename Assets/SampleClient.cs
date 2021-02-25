using UnityEngine;

public class SampleClient : BaseClient
{
    public string username = "Player";
    public string address = "127.0.0.1";
    public ushort port = 7777;
    public string password = string.Empty;

    void OnGUI()
    {
        if (!IsConnected && !IsConnecting)
        {
            if (GUILayout.Button("Connect"))
            {
                Connect(address, port, password);
            }
        }
        else
        {
            if (GUILayout.Button("Disconnect"))
            {
                Disconnect();
            }
        }
    }

    private void Update()
    {
        ClientUpdate();
    }

    public override void OnConnecting(string address, ushort port, string password)
    {
        Debug.LogWarning("[Client] Connecting to server " + address+":"+port+" with password "+password);
    }

    public override void OnConnected()
    {
        Debug.LogWarning("[Client] Connected to server");

        //example using poolable bitStream
		using (PooledBitStream bitStream = BitStreamPool.GetBitStream())
        {
			bitStream.Reset();
			bitStream.Write((byte)CustomIDs.CLIENT_DATA);
			bitStream.Write(username);

			SendToServer(bitStream, PacketPriority.IMMEDIATE_PRIORITY, PacketReliability.RELIABLE, 0);
        }
    }

    public override void OnDisconnected(DisconnectReason reason)
    {
        Debug.LogWarning("[Client] Disconnected from server (" + reason+")");
    }

    public override void OnShutdown()
    {
        Debug.LogWarning("[Client] Shutdown");
    }

    public override void OnSerializeData(BitStream bitStream, int packet_size)
    {
        CustomIDs packet_id = (CustomIDs)bitStream.ReadByte();

        switch (packet_id)
        {
            case CustomIDs.CLIENT_DATA_ACCEPTED:
                Debug.LogWarning("[Client] Server accepted client data...   Text = "+bitStream.ReadString());
                break;
        }
    }
}
