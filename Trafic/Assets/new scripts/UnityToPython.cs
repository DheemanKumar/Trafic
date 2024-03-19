using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;

public class UnityToPython : MonoBehaviour
{
    public bool active=true;

    public string numbers;

    public bool send=false;

    public bool Reconnect = false;    //for reconnection if connection not established
    private bool connected = false;


    string host = "127.0.0.1";
    int port = 12345;


    TcpClient client;
    NetworkStream stream;

    void connect()
    {
        try
        {
            // Create a TCP client
            client = new TcpClient(host, port);

            // Create a network stream
            stream = client.GetStream();
            connected = true;

            Debug.Log("Connection established.");
        }
        catch (Exception e)
        {
            Debug.Log($"Error: {e}");
        }
    }

    private void Awake()
    {
        Reconnect = false;
        connected = false;
        // Define host and port
        if (active)
        {
            connect();
        }
    }


    private void OnDestroy()
    {
        // Close the stream and the client
        if (active)
        {
            stream.Close();
            client.Close();
            Debug.Log("System Disconnected.");
        }
    }

    public string sendToPython(string message)
    {

        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);

        // Receive the result from the server
        byte[] responseData = new byte[1024];
        int bytesRead = stream.Read(responseData, 0, responseData.Length);
        string response = Encoding.UTF8.GetString(responseData, 0, bytesRead);

        return response;

    }

    private void Update()
    {
        if (active)
        {
            if (send)
            {
                string response = sendToPython(numbers);
                Debug.Log($"Response is: {response}");
                send = false;
            }


            if (!connected && Reconnect)
            {//for reconnection
                connect();
                Reconnect = false;
            }
        }
    }
}
