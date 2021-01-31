﻿using UnityEngine;

public class SampleClient : BaseClient
{
    public BitStream client_bitStream = new BitStream();

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

        client_bitStream.Reset();
        client_bitStream.Write((byte)CustomIDs.CLIENT_DATA);
        client_bitStream.Write(username);

        SendToServer(client_bitStream, PacketPriority.IMMEDIATE_PRIORITY, PacketReliability.RELIABLE, 0);
    }

    public override void OnDisconnected(DisconnectReason reason)
    {
        Debug.LogWarning("[Client] Disconnected from server (" + reason+")");
    }

    public override void OnShutdown()
    {
        Debug.LogWarning("[Client] Shutdown");
        client_bitStream?.Close();
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
