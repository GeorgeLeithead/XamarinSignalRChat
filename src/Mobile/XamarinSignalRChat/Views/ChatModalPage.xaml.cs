// <copyright file="ChatModalPage.xaml.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Views
{
	using System;
	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;
	using XamarinSignalRChat.ViewModels;

	/// <summary>Chat modal page view.</summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatModalPage : ContentPage
	{
		private ChatModalViewModel vm;

		/// <summary>Initialises a new instance of the <see cref="ChatModalPage"/> class.</summary>
		public ChatModalPage()
		{
			this.InitializeComponent();
		}

		private ChatModalViewModel VM
		{
			get => this.vm ??= (ChatModalViewModel)this.BindingContext;
		}

		/// <summary>List tapped event.</summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">Item tapped event arguments.</param>
		public void OnListTapped(object sender, ItemTappedEventArgs e)
		{
			this.chatInput.UnFocusEntry();
		}

		/// <summary>Scroll tapped event.</summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">Event arguments.</param>
		public void ScrollTap(object sender, EventArgs e)
		{
			lock (new object())
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					while (this.VM.DelayedMessages.Count > 0)
					{
						this.VM.Messages.Insert(0, this.VM.DelayedMessages.Dequeue());
					}

					this.VM.ShowScrollTap = false;
					this.VM.LastMessageVisible = true;
					this.VM.PendingMessageCount = 0;
					this.ChatList?.ScrollToFirst();
				});
			}
		}

		/// <inheritdoc/>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!DesignMode.IsDesignModeEnabled)
			{
				this.VM.ConnectCommand.Execute(null);
			}

			this.ToolbarDone.Clicked += this.ToolbarDone_Clicked;
		}

		/// <inheritdoc/>
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			if (!DesignMode.IsDesignModeEnabled)
			{
				this.VM.DisconnectCommand.Execute(null);
				Shell.Current.GoToAsync($"//Chat");
			}

			this.ToolbarDone.Clicked -= this.ToolbarDone_Clicked;
		}

		private async void ToolbarDone_Clicked(object sender, EventArgs e)
		{
			await this.Navigation.PopToRootAsync();
		}
	}
}