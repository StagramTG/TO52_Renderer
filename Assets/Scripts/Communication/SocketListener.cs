using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UnityEngine;

public class SocketListener : MonoBehaviour
{
    /** Response constants */
    private const string RESPONSE_SUCCESS = "1";
    private const string RESPONSE_ERROR   = "0";

    TcpClient client;
    TcpListener server;
    Byte[] bytes;
    String data;

    NetworkStream networkStream;
    StreamWriter writer;
    StreamReader reader;

    public string HostAdress = "127.0.0.1";
    public int HostPort = 5111;

    /** Instance of Agents Manager */
    public AgentsManager agentsManager;

	void Start ()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(HostAdress), HostPort);
        server = new TcpListener(endPoint);
        server.Start();

        Debug.Log("Server Launched");
        Debug.Log("Wait for client connection...");

        /** Set async client connection */
        server.BeginAcceptTcpClient(DoAcceptTcpClientCallback, server);
	}
	
	void Update ()
    {
        if(client != null && client.Connected)
        {
            /** Receive messages */
            string data = "";
            while(networkStream.DataAvailable)
            {
                char[] bytes = new char[1024];
                reader.Read(bytes, 0, 1024);
                data += new string(bytes);
            }

            /** Process messages */
            if(data != "")
            {
                /** Look for message type */
                JObject obj = JObject.Parse(data);

                /** Get message type as integer */
                int msgType = int.Parse(obj["Type"].ToString());

                /** Process different cases */
                switch(msgType)
                {
                    case Messages.Types.BEGIN:
                        Debug.Log("Begin communication");
                        ProcessBeginMessage();
                        break;

                    case Messages.Types.INIT_DATA:
                        ProcessInitMessage(data);
                        break;

                    case Messages.Types.SIM_DATA:
                        ProcessSimMessage(data);
                        break;

                    case Messages.Types.END:
                        Debug.Log("End communication");
                        ProcessEndMessage();
                        break;
                }
            }
        }
	}
    
    void DoAcceptTcpClientCallback(IAsyncResult ar)
    {
        client = server.EndAcceptTcpClient(ar);

        /** Setup streams to read and write */
        networkStream = client.GetStream();
        writer = new StreamWriter(networkStream);
        reader = new StreamReader(networkStream);

        Debug.Log("Client connection completed");
    }

    void OnDestroy()
    {
        /** Clear socket connection */
        if (client != null && client.Connected) client.Close();
        server.Stop();
    }

    /*=======================================================
     
        MESSAGE PROCESSING METHODS

    =======================================================*/

    private void ProcessBeginMessage()
    {
        /** Send back begin message */
        Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(RESPONSE_SUCCESS);
        networkStream.Write(bytes, 0, bytes.Length);
    }

    private void ProcessEndMessage()
    {
        /** Send back begin message */
        Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(RESPONSE_SUCCESS);
        networkStream.Write(bytes, 0, bytes.Length);
    }

    private void ProcessInitMessage(string pdata)
    {
        Debug.Log("Receive Init data");

        /** Desarialize data */
        MessageData<List<CharacterAgentData>> data = JsonConvert.DeserializeObject<MessageData<List<CharacterAgentData>>>(pdata);

        /** Give data to agents manager for init */
        bool initSuccess = agentsManager.InitAgents(data.Data);
        if(!initSuccess)
        {
            /** Send back error message */
            Byte[] error = System.Text.Encoding.ASCII.GetBytes(RESPONSE_ERROR);
            networkStream.Write(error, 0, error.Length);
        }

        /** Send back begin message */
        Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(RESPONSE_SUCCESS);
        networkStream.Write(bytes, 0, bytes.Length);
    }

    private void ProcessSimMessage(string pdata)
    {
        /** Desarialize data */

    }
}
