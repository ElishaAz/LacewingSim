using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Lacewing
{
	public class UdpNetworking : MonoBehaviour
	{
		// [SerializeField] private string bindAddress;
		[SerializeField] private int bindPort;

		public UnityEvent<string> onMessageReceived;

		/// <summary> 
		/// Background thread for TcpServer workload. 	
		/// </summary> 	
		private Thread listenerThread;

		private UdpClient udpClient;

		private IPEndPoint connectedIp;

		private bool run = true;


		// Use this for initialization
		protected virtual void Start()
		{
			run = true;
			// Start TcpServer background thread 		
			listenerThread = new Thread(ListenForIncomingRequests)
			{
				IsBackground = true
			};
			listenerThread.Start();
		}

		// Update is called once per frame
		protected virtual void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SendUdpMessage("Space!");
			}
		}

		protected virtual void OnDestroy()
		{
			run = false;
			listenerThread.Interrupt();
			listenerThread.Join(100);
			udpClient.Close();
		}

		/// <summary> 	
		/// Runs in background TcpServerThread; Handles incoming TcpClient requests 	
		/// </summary> 	
		private void ListenForIncomingRequests()
		{
			udpClient = new UdpClient(bindPort);
			while (run)
			{
				try
				{
					byte[] data = udpClient.Receive(ref connectedIp);

					string text = Encoding.UTF8.GetString(data);
					if (!run) return;
					onMessageReceived.Invoke(text);
				}
				catch (Exception err)
				{
					if (!run) return;
					Debug.LogError(err);
				}
			}
		}

		/// <summary> 	
		/// Send message to client using socket connection. 	
		/// </summary> 	
		protected void SendUdpMessage(string message)
		{
			if (connectedIp == null)
			{
				return;
			}

			try
			{
				// Get a stream object for writing. 
				byte[] messageBytes = Encoding.UTF8.GetBytes(message);
				udpClient.Send(messageBytes, messageBytes.Length, connectedIp);
			}
			catch (SocketException socketException)
			{
				Debug.LogError("Socket exception: " + socketException);
			}
		}
	}
}