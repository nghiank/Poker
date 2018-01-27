using System;
using UnityEngine.Networking;


// TODO : abstract it
using UnityEngine;
using FlatBuffers;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class NetworkManager
{
	// ManualResetEvent instances signal completion.
	private static ManualResetEvent connectDone = 
		new ManualResetEvent(false);
	private static ManualResetEvent sendDone = 
		new ManualResetEvent(false);
	private static ManualResetEvent receiveDone = 
		new ManualResetEvent(false);
	
	public NetworkManager ()
	{
		// Establish the remote endpoint for the socket.
		// The name of the 
		// remote device is "host.contoso.com".
		string hostId = "127.0.0.1";
		int port = 8080;
		IPHostEntry ipHostInfo = Dns.Resolve("127.0.0.1");
		IPAddress ipAddress = ipHostInfo.AddressList[0];
		IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

		// Create a TCP/IP socket.
		Socket client = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);

		// Connect to the remote endpoint.
		client.BeginConnect( remoteEP, 
			new AsyncCallback(ConnectCallback), client);
		connectDone.WaitOne();


		byte[] buffer = SchemaBuilder.buildJoinRoom("Singapore", UserSession.Instance.getAuthToken()).SizedByteArray();
		byte[] bytes = BitConverter.GetBytes(buffer.Length);
		if (BitConverter.IsLittleEndian)
			Array.Reverse(bytes);
		// Send test data to the remote device.
		//Send(client, );
		sendDone.WaitOne();

		Debug.Log ("Error1 when connect to 127.0.0.1:");

	}

	private static void ConnectCallback(IAsyncResult ar) {
		try {
			// Retrieve the socket from the state object.
			Socket client = (Socket) ar.AsyncState;
			// Complete the connection.
			client.EndConnect(ar);

			Console.WriteLine("Socket connected to {0}",
				client.RemoteEndPoint.ToString());
			// Signal that the connection has been made.
			connectDone.Set();
		} catch (Exception e) {
			Debug.Log (e.ToString());
		}
	}

	private static void Send(Socket client, byte[] byteData) {
		// Begin sending the data to the remote device.
		client.BeginSend(byteData, 0, byteData.Length, 0,
			new AsyncCallback(SendCallback), client);
	}

	private static void SendCallback(IAsyncResult ar) {
		try {
			// Retrieve the socket from the state object.
			Socket client = (Socket) ar.AsyncState;

			// Complete sending the data to the remote device.
			int bytesSent = client.EndSend(ar);
			Console.WriteLine("Sent {0} bytes to server.", bytesSent);

			// Signal that all bytes have been sent.
			sendDone.Set();
		} catch (Exception e) {
			Debug.Log (e.ToString());
		}
	}


}


