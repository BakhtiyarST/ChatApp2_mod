using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp2
{
	internal class Server
	{
		static bool flagContinue=true;
		public static void AcceptMsg()
		{
			Console.WriteLine("The server is ready for accepting messages.");
			IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
			UdpClient udpClient = new UdpClient(12345);
			do
			{
				byte[] buffer = udpClient.Receive(ref endPoint);
				string data = Encoding.UTF8.GetString(buffer);

				new Thread(() =>
				{
					Message msg = Message.FromJson(data);
					Console.WriteLine(msg.ToString());
					string text = msg.Text;
					if (text.ToLower() == "exit")
					{
						flagContinue = false;
						Console.WriteLine("Exit signal is received.");
					}
					else
					{
						Message msgResponse = new Message("Server", "Message accepted on server side");
						string msgResponseJson = msgResponse.ToJson();
						byte[] dataResponse = Encoding.UTF8.GetBytes(msgResponseJson);
						udpClient.Send(dataResponse, endPoint);
					}
				}).Start();
			} while (flagContinue);
		}
	}
}
