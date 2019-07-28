// Socket Libraries
using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class sendEasy : MonoBehaviour
{

    private int port = 5000;
    private string address = "127.0.0.1";
    
    private TcpClient client;
    private NetworkStream stream;
    private bool streamActive;


    // Start is called before the first frame update
    void Start()
    {
        client = new TcpClient(address, port);
        stream = client.GetStream();
        streamActive = true;
        InvokeRepeating("SendMessage", 1.0f, 1.0f);
    }

    void SendMessage()
    {
        if(streamActive)
        {
            string message = "Easy Level!";
            SendToServer(message);
            Debug.Log("Sent: " + message);
        }
    }

    void SendToServer(string message)
    {
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

        stream.Write(data, 0, data.Length);
    }

    public void EndServerStream()
    {
        streamActive = false;
        string message = "End Stream!";
        SendToServer(message);
        Debug.Log("Sent: " + message);
    }
}
