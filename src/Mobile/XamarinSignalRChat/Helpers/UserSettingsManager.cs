// <copyright file="UserSettingsManager.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Helpers
{
	using Xamarin.Essentials;

	/// <summary>User settings manager.</summary>
	public static class UserSettingsManager
	{
#if DEBUG
		private static readonly string DefaultChatIP = DeviceInfo.Platform == DevicePlatform.Android ? "10.0.2.2" : "localhost"; // NOTE: 10.0.2.2 is the Android equivalent of localhost
#else
		private static readonly string DefaultChatIP = "XamarinSignalRChatService.azurewebsites.net;
#endif

		/// <summary>Gets or sets the user display name.</summary>
		public static string DisplayName
		{
			get => Preferences.Get(nameof(DisplayName), "George");
			set => Preferences.Set(nameof(DisplayName), value);
		}

		/// <summary>Gets or sets the chat group.</summary>
		public static string Group
		{
			get => Preferences.Get(nameof(Group), "General"); // Default to the 'General' group
			set => Preferences.Set(nameof(Group), value);
		}

		/// <summary>Gets or sets the chat service address.</summary>
		public static string ChatServerIP
		{
			get => Preferences.Get(nameof(ChatServerIP), DefaultChatIP);
			set => Preferences.Set(nameof(ChatServerIP), value);
		}

		/// <summary>Gets a value indicating whether to use HTTPS.</summary>
		public static bool UseHttps => DefaultChatIP != "localhost" && DefaultChatIP != "10.0.2.2";
	}
}