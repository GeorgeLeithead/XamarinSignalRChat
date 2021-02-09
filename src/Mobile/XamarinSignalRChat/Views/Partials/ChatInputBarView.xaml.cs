// <copyright file="ChatInputBarView.xaml.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Views.Partials
{
	using System;
	using Xamarin.Forms;
	using XamarinSignalRChat.ViewModels;

	/// <summary>Chat input bar view.</summary>
	public partial class ChatInputBarView : ContentView
	{
		/// <summary>Initialises a new instance of the <see cref="ChatInputBarView"/> class.</summary>
		public ChatInputBarView()
		{
			this.InitializeComponent();

			if (Device.RuntimePlatform == Device.iOS)
			{
				this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, null, null, null, this.chatTextInput));
			}
		}

		/// <summary>Event handler for completed.</summary>
		/// <param name="sender">Object sender.</param>
		/// <param name="e">Event arguments.</param>
		public void Handle_Completed(object sender, EventArgs e)
		{
			(this.Parent.Parent.BindingContext as ChatModalViewModel).OnSendCommand.Execute(null);
			this.chatTextInput.Focus();
		}

		/// <summary>Un-focus entry.</summary>
		public void UnFocusEntry()
		{
			this.chatTextInput?.Unfocus();
		}
	}
}