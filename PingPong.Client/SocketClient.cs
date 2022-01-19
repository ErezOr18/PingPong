using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using PingPong.Common;

namespace PingPong.Client
{

    public  class SocketClient
    {

        private IPEndPoint _server;

        private ManualResetEvent connectDone;
        private ManualResetEvent sendDone;
        private ManualResetEvent receiveDone;

        // The response from the remote device.  
        private String response = String.Empty;


        public SocketClient(IPEndPoint server)
        {
            _server = server;
            connectDone = new ManualResetEvent(false);
            sendDone = new ManualResetEvent(false);
            receiveDone = new ManualResetEvent(false);
        }
        public void RunClient()
        {
            try
            {


                Socket client = new Socket(_server.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                client.BeginConnect(_server,
                            new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                Send(client, "This is a test<EOF>");
                sendDone.WaitOne();


                Receive(client);
                receiveDone.WaitOne();


                Console.WriteLine("Response received : {0}", response);

                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.WorkSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.MessageBuffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.WorkSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.MessageStringBuilder.Append(Encoding.ASCII.GetString(state.MessageBuffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.MessageBuffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.MessageStringBuilder.Length > 1)
                    {
                        response = state.MessageStringBuilder.ToString();
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }


        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}