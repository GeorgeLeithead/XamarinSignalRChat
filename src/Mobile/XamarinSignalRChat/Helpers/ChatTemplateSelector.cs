// <copyright file="ChatTemplateSelector.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Helpers
{
	using Xamarin.Forms;
	using XamarinSignalRChat.Models;
	using XamarinSignalRChat.Views.Cells;

	/// <summary>Chat template selector helper.</summary>
	public class ChatTemplateSelector : DataTemplateSelector
	{
		private readonly DataTemplate localUserDataTemplate;
		private readonly DataTemplate remoteUserDataTemplate;

		/// <summary>Initialises a new instance of the <see cref="ChatTemplateSelector"/> class.</summary>
		public ChatTemplateSelector()
		{
			this.localUserDataTemplate = new DataTemplate(typeof(LocalUserViewCell));
			this.remoteUserDataTemplate = new DataTemplate(typeof(RemoteUserViewCell));
		}

		/// <inheritdoc/>
		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			if (!(item is ChatMessage messageVm))
			{
				return null;
			}

			return messageVm.User == UserSettingsManager.DisplayName ? this.localUserDataTemplate : this.remoteUserDataTemplate;
		}
	}
}