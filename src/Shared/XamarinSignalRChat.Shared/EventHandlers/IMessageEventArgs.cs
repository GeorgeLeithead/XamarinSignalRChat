// <copyright file="IMessageEventArgs.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Shared.EventHandlers
{
	/// <summary>Message event arguments interface.</summary>
	public interface IMessageEventArgs
	{
		/// <summary>Gets the Message for the event argument.</summary>
		string Message { get; }
	}
}