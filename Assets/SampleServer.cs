using System.Collections.Generic;
using UnityEngine;

public struct ClientData
{
    public ulong guid;
    public string username;

    public ClientData(ulong guid, string username)
    {
        this.guid = guid;
        this.username = username;
    }
}

public class SampleServer : BaseServer
{
    public ushort port = 7777;
    public string password = string.Empty;
    public ushort max_connections = 10;

    public Dictionary<ulong, ClientData> Clients = new Dictionary<ulong, ClientData>();//accepted clients dictionary

    void OnGUI()
    {
        GUILayout.Space(100);
        if (!IsRunning)
        {
            if(GUILayout.Button("Start Server"))
            {
                StartServer(string.Empty, port, password, max_connections);
            }
        }
        else
        {
            if (GUILayout.Button("Stop Server"))
            {
                StopServer();
            }

            GUILayout.Box("Clients");
            GUILayout.BeginVertical("box");
            foreach (ClientData data in Clients.Values)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Box(data.username);
                if (GUILayout.Button("Kick"))
                {
                    peer.CloseConnection(data.guid, true);//close connection
                }
                if (GUILayout.Button("Kick (silent)"))
                {
                    peer.CloseConnection(data.guid, false);//close the connection without notifying the client that the connection is closed
                }
                if (GUILayout.Button("Ban"))
                {
                    peer.AddBanIP(peer.GetAddress(data.guid));//ban ip
                    peer.CloseConnection(data.guid,true);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
    }

    void Update()
    {
        ServerUpdate();//update the server, receiving received packets from the queue and process them
    }


    public override void OnSerializeData(BitStream bitStream, ulong guid, int packet_size)
    {
        CustomIDs packet_id = (CustomIDs)bitStream.ReadByte();//the first byte of the packet is id, the rest is data

        switch (packet_id)
        {
            case CustomIDs.CLIENT_DATA:
                Debug.Log("[Server] Received client data from " + peer.GetAddress(guid,true)+" with guid "+guid);

                //creating a client data structure and putting it in a dictionary
                Clients.Add(guid, new ClientData(guid, bitStream.ReadString()));

                /* we inform the client that his data is accepted, we send it via a reliable channel */
		using (PooledBitStream bsIn = BitStreamPool.GetBitStream())
                {
                    //be sure to reset the bitstream! If you do not do this the old recorded data will be sent
                    bsIn.Reset();

                    //Write the packet id as a byte so that the client knows how to process the packet
                    bsIn.Write((byte)CustomIDs.CLIENT_DATA_ACCEPTED);

                    //As an example, we will send the text to the client in the load
                    bsIn.Write("RakNet top... SosiPisos");

		    //we send data from bitstream to the client using its unique guid
		    SendToClient(bitStream, guid, PacketPriority.IMMEDIATE_PRIORITY, PacketReliability.RELIABLE, 0);
		}
                break;
        }
    }

    public override void OnDisconnected(ulong guid, DisconnectReason reason)
    {
        if (Clients.ContainsKey(guid))
        {
            Debug.Log("[Server] Client " + Clients[guid].username+" disconnected! ("+reason+")");
            Clients.Remove(guid);//deleting client data from the dictionary
        }
    }

    public override void OnServerStart()
    {
        Debug.LogWarning("[Server] Server started on port " + port);
    }

    public override void OnServerStop()
    {
        Debug.LogWarning("[Server] Server stopped");
    }

    public override void OnServerStartFailed(StartupResult result)
    {
        Debug.LogError("[Server] Server starting failed! " + result);
    }

    public override void OnShutdown()
    {
        Debug.LogWarning("[Server] Shutdown");
    }
}
