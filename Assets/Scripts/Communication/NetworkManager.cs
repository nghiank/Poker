using System;
using UnityEngine.Networking;
using UnityEngine;
using FlatBuffers;
using System.Net;
using System.Net.Sockets;
using System.Threading;

// State object for receiving data from remote device.
public class StateObject {
	// Client socket.
	public Socket workSocket = null;
	// Size of receive buffer.
	public const int BufferSize = 256;
	// Receive buffer.
	public byte[] buffer = new byte[BufferSize];
}

public class NetworkManager
{
	public delegate void NetworkCallback(IAsyncResult ar);

	// Client socket
	private Socket client;
	private FlatBuffersDecoder decoder;

	private String hostId;
	private int port;

	// ManualResetEvent instances signal completion.
	private static ManualResetEvent connectDone = 
		new ManualResetEvent(false);
	private static ManualResetEvent sendDone = 
		new ManualResetEvent(false);
	private static ManualResetEvent receiveDone = 
		new ManualResetEvent(false);

	// TODO: pass these directly in the function call
	NetworkCallback connectCallback;
	NetworkCallback secondCallback;
	NetworkCallback receiveCallback;

	public NetworkManager (string hostId, int port)
	{
		this.hostId = hostId;
		this.port = port;
	}

	public void Connect(NetworkCallback callback) {
		IPHostEntry ipHostInfo = Dns.Resolve(this.hostId);
		IPAddress ipAddress = ipHostInfo.AddressList[0];
		IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
		// Create a TCP/IP socket.
		this.client = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);
		// Connect to the remote endpoint.
		// TODO : find a way to pass it directly into BeginConnect
		this.connectCallback = callback;
		this.client.BeginConnect( remoteEP, 
			new AsyncCallback(ConnectCallback), client);
	}
		
	public void Disconnect() {
		// Release the socket.
		client.Shutdown(SocketShutdown.Both);
		client.Close();
	}

	private void ConnectCallback(IAsyncResult ar) {
		try {
			// Retrieve the socket from the state object.
			Socket client = (Socket) ar.AsyncState;
			// Complete the connection.
			client.EndConnect(ar);
			Debug.Log("Socket connected to " + client.RemoteEndPoint.ToString());
			if (this.connectCallback != null) {
				this.connectCallback(ar);
			}
			// Signal that the connection has been made.
			connectDone.Set();
		} catch (Exception e) {
			Debug.Log (e.ToString());
		}
	}

	public void Send(byte[] byteData, NetworkCallback sendCallback) {
		// Begin sending the data to the remote device.
		this.secondCallback = sendCallback;
		client.BeginSend(byteData, 0, byteData.Length, 0,
			new AsyncCallback(SendCallback), this.client);
	}

	private void SendCallback(IAsyncResult ar) {
		try {
			// Retrieve the socket from the state object.
			Socket client = (Socket) ar.AsyncState;

			// Complete sending the data to the remote device.
			int bytesSent = client.EndSend(ar);
			Debug.Log("Sent " + bytesSent + "bytes to server. + ");

			if (this.secondCallback!=null) {
				this.secondCallback(ar);
			}
			// Signal that all bytes have been sent.
			sendDone.Set();
		} catch (Exception e) {
			Debug.Log (e.ToString());
		}
	}

	public void Receive(NetworkCallback receiveCallback) {
		try {
			// Create the state object.
			StateObject state = new StateObject();
			state.workSocket = this.client;
			this.decoder = new FlatBuffersDecoder();

			// Begin receiving the data from the remote device.
			client.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,
				new AsyncCallback(ReceiveCallback), state);
		} catch (Exception e) {
			Console.WriteLine(e.ToString());
		}
	}

	private void ReceiveCallback( IAsyncResult ar ) {
		try {
			// Retrieve the state object and the client socket 
			// from the asynchronous state object.
			StateObject state = (StateObject) ar.AsyncState;
			Socket client = state.workSocket;

			// Read data from the remote device.
			int bytesRead = client.EndReceive(ar);

			if (bytesRead > 0) {
				this.decoder.Fetch(state.buffer);
				// Get the rest of the data.
				client.BeginReceive(state.buffer,0,StateObject.BufferSize,0,
					new AsyncCallback(ReceiveCallback), state);
			} else {
				// All the data has arrived; put it in response.
				// Signal that all bytes have been received.
				receiveDone.Set();
			}
		} catch (Exception e) {
			Console.WriteLine(e.ToString());
		}
	}

	public FlatBuffersDecoder getDecoder() {
		return decoder;
	}

	public ManualResetEvent getConnectDoneEvent() {
		return connectDone;
	}

	public ManualResetEvent getSendDoneEvent() {
		return sendDone;
	}

	public ManualResetEvent getReceiveDoneEvent() {
		return receiveDone;
	}
}


