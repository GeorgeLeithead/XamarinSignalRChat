// <copyright file="IDialogService.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Interfaces
{
	using System.Threading.Tasks;

	/// <summary>Dialog service interface.</summary>
	public interface IDialogService
	{
		/// <summary>Display alert dialog.</summary>
		/// <param name="title">Alert title.</param>
		/// <param name="message">Alert message.</param>
		/// <param name="accept">Accept button text.</param>
		/// <param name="cancel">Cancel button text.</param>
		/// <returns>Task{bool} accept or cancel.</returns>
		Task<bool> DisplayAlert(string title, string message, string accept, string cancel);

		/// <summary>Display alert dialog.</summary>
		/// <param name="title">Alert title.</param>
		/// <param name="message">Alert message.</param>
		/// <param name="cancel">Cancel button text.</param>
		/// <returns>Task.</returns>
		Task DisplayAlert(string title, string message, string cancel);
	}
}