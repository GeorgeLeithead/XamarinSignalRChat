// <copyright file="Program.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Console
{
	using System;
	using System.Threading.Tasks;
	using XamarinSignalRChat.Shared;
	using XamarinSignalRChat.Shared.EventHandlers;

	/// <summary>Console application program.</summary>
	public class Program
	{
		/// <summary>Name of chat user.</summary>
		private static string name;

		/// <summary>Name of room to join.</summary>
		private static string room;

		/// <summary>Chat service.</summary>
		private static ChatService service;

		/// <summary>Main method.</summary>
		/// <param name="args">Application arguments.</param>
		/// <returns>Async Task.</returns>
		public static async Task Main(string[] args)
		{
			service = new ChatService();
			service.OnReceivedMessage += Service_OnReceivedMessage;
			service.OnEnteredOrExited += Service_OnEnteredOrExited;
			Console.WriteLine("Enter user name:");
			name = Console.ReadLine();
			Console.WriteLine("Enter IP:");
			string ip = Console.ReadLine();
			service.Init(ip, ip != "localhost");

			await service.ConnectAsync();
			Console.WriteLine("You are connected...");

			bool keepGoing = await JoinRoom();
			if (!keepGoing)
			{
				return;
			}

			do
			{
				string text = Console.ReadLine();
				if (text == "exit")
				{
					keepGoing = false;
				}
				else if (text == "leave")
				{
					await service.LeaveChannelAsync(room, name);
					keepGoing = await JoinRoom();
				}
				else
				{
					await service.SendMessageAsync(room, name, text);
				}
			}
			while (keepGoing);
		}

		private static async Task<bool> JoinRoom()
		{
			Console.WriteLine($"Enter room ({string.Join(",", service.GetRooms())}):");
			room = Console.ReadLine();
			if (room.ToUpperInvariant().Trim() == "EXIT")
			{
				return false;
			}

			await service.JoinChannelAsync(room, name);
			return true;
		}

		private static void Service_OnEnteredOrExited(object sender, MessageEventArgs e)
		{
			if (e.User == name)
			{
				return;
			}

			if (e.Message.EndsWith(" joined."))
			{
				Console.WriteLine(e.User + " entered the room...");
			}
			else
			{
				Console.WriteLine(e.User + " left the room...");
			}
		}

		private static void Service_OnReceivedMessage(object sender, MessageEventArgs e)
		{
			if (e.User == name)
			{
				return;
			}

			Console.WriteLine(e.User + ": " + e.Message);
		}
	}
}