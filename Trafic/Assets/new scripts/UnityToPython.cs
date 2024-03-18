using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;

public class UnityToPython : MonoBehaviour
{
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

    void Start()
    {
        Reconnect = false;
        connected = false;
        // Define host and port
        connect();
    }

    private void OnDestroy()
    {
        // Close the stream and the client
        stream.Close();
        client.Close();
        Debug.Log("System Disconnected.");
    }

    private string sendToPython(string message)
    {
        //string message = "";

        //for (int i = 0; i < input.Length; i++)
        //{
        //    message += input[i].ToString();
        //    if (i < input.Length - 1)
        //    {
        //        message += ", ";
        //    }
        //}

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
        if (send)
        {
            string response= sendToPython(numbers);
            // Convert the numbers to bytes and send them to the server


            int result = Convert.ToInt32(response);
            Debug.Log($"Sum of numbers is: {result}");

            // Convert the response to an integer and print it


            send = false;
        }


        if(!connected && Reconnect)
        {//for reconnection
            connect();
            Reconnect = false;
        }
    }
}
