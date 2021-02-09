// <copyright file="SettingsViewModel.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.ViewModels
{
	using Xamarin.Forms;
	using XamarinSignalRChat.Helpers;
	using XamarinSignalRChat.ViewModels.Base;

	/// <summary>Settings view model.</summary>
	public class SettingsViewModel : ViewModelBase
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="SettingsViewModel"/> class.
		/// </summary>
		public SettingsViewModel()
		{
			if (DesignMode.IsDesignModeEnabled)
			{
				return;
			}

			this.IsBusy = true;
			this.Title = "Settings";
			this.IsBusy = false;
		}

		/// <summary>Gets or sets the chat service address.</summary>
		public string ChatEndPoint
		{
			get => UserSettingsManager.ChatServerIP;
			set
			{
				if (value != UserSettingsManager.ChatServerIP)
				{
					UserSettingsManager.ChatServerIP = value;
					this.NotifyPropertyChanged(() => this.ChatEndPoint);
				}
			}
		}

		/// <summary>Gets or sets the chat display name.</summary>
		public string DisplayName
		{
			get => UserSettingsManager.DisplayName;
			set
			{
				if (value != UserSettingsManager.DisplayName)
				{
					UserSettingsManager.DisplayName = value;
					this.NotifyPropertyChanged(() => this.DisplayName);
				}
			}
		}
	}
}