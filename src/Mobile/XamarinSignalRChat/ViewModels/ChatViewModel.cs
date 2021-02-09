// <copyright file="ChatViewModel.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.ViewModels
{
	using System.Threading.Tasks;
	using System.Windows.Input;
	using Xamarin.Forms;
	using XamarinSignalRChat.ViewModels.Base;
	using XamarinSignalRChat.Views;

	/// <summary>Chat view model.</summary>
	public class ChatViewModel : ViewModelBase
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="ChatViewModel"/> class.
		/// </summary>
		public ChatViewModel()
		{
			if (DesignMode.IsDesignModeEnabled)
			{
				return;
			}

			this.IsBusy = true;
			this.Title = "Real-time chat";
			this.LaunchChatCommand = new Command(async () => await this.LaunchChat());
			this.IsBusy = false;
		}

		/// <summary>Gets or sets launch chat command.</summary>
		public ICommand LaunchChatCommand { get; set; }

		private async Task LaunchChat()
		{
			Routing.RegisterRoute("ChatModal", typeof(ChatModalPage));
			await Shell.Current.GoToAsync("/ChatModal");
		}
	}
}