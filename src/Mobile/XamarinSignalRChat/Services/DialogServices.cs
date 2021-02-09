// <copyright file="DialogServices.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Services
{
	using System.Threading.Tasks;
	using Xamarin.Forms;
	using XamarinSignalRChat.Interfaces;

	/// <summary>Dialog service.</summary>
	public class DialogServices : IDialogService
	{
		/// <summary>Display alert dialog.</summary>
		/// <param name="title">Alert title.</param>
		/// <param name="message">Alert message.</param>
		/// <param name="cancel">Cancel button text.</param>
		/// <returns>Task.</returns>
		public Task DisplayAlert(string title, string message, string cancel)
		{
			Page page = Application.Current.MainPage;
			if (page == null)
			{
				return Task.CompletedTask;
			}

			return page.DisplayAlert(title, message, cancel);
		}

		/// <summary>Display alert dialog.</summary>
		/// <param name="title">Alert title.</param>
		/// <param name="message">Alert message.</param>
		/// <param name="accept">Accept button text.</param>
		/// <param name="cancel">Cancel button text.</param>
		/// <returns>Task{bool} accept or cancel.</returns>
		public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
		{
			Page page = Application.Current.MainPage;
			if (page == null)
			{
				return Task.FromResult(false);
			}

			return page.DisplayAlert(title, message, accept, cancel);
		}
	}
}