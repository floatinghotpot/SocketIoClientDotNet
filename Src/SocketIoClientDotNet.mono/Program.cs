using System;
using System.Runtime.CompilerServices;
using Quobject.SocketIoClientDotNet.Client;

namespace iotest
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			Quobject.EngineIoClientDotNet.Modules.LogManager.Enabled = true;

			var socket = IO.Socket("http://127.0.0.1:7000");
			socket.On(Socket.EVENT_CONNECT, () =>
			{
				Console.WriteLine("Connected.");

				socket.Emit("hello");
				
			});

			socket.On("notify", (data) =>
			{
				Console.WriteLine("notify: " + data);
			});

			socket.On("rpc_ret", (data) =>
			{
				Console.WriteLine("rpc_ret: " + data);
			});

			while (true)
			{
				string str = Console.ReadLine();
				if (str == "quit" || str == "exit") break;
				else socket.Emit("rpc", str);
			}

			socket.Disconnect();
		}
	}
}
