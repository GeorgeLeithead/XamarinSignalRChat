// <copyright file="ViewModelBase.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.ViewModels.Base
{
	using System.Runtime.Serialization;
	using System.Threading.Tasks;
	using Xamarin.Forms;
	using Xamarin.Forms.Internals;
	using XamarinSignalRChat.Interfaces;
	using XamarinSignalRChat.Shared;

	/// <summary>View model base class.</summary>
	[Preserve(AllMembers = true)]
	[DataContract]
	public abstract class ViewModelBase : ExtendedBindableObject
	{
		private ChatService chatService;

		private IDialogService dialogService;

		/// <summary>View is busy.</summary>
		private bool isBusy;

		/// <summary>View title.</summary>
		private string title;

		/// <summary>Initialises a new instance of the <see cref="ViewModelBase" /> class.</summary>
		public ViewModelBase()
		{
		}

		/// <summary>Gets the chat service dependency.</summary>
		public ChatService ChatService => this.chatService ??= DependencyService.Resolve<ChatService>();

		/// <summary>Gets the dialog service dependency.</summary>
		public IDialogService DialogService => this.dialogService ??= DependencyService.Resolve<IDialogService>();

		/// <summary>Gets or sets a value indicating whether the view is busy.</summary>
		public bool IsBusy
		{
			get => this.isBusy;
			set
			{
				this.isBusy = value;
				this.NotifyPropertyChanged(() => this.IsBusy);
			}
		}

		/// <summary>gets or sets a value for the view title.</summary>
		public string Title
		{
			get => this.title;
			set
			{
				this.title = value;
				this.NotifyPropertyChanged(() => this.Title);
			}
		}

		/// <summary>Initialises the view model.</summary>
		/// <param name="parameter">Initianlisation parameter.</param>
		/// <returns>Task result.</returns>
		public virtual Task InitializeAsync(object parameter)
		{
			return Task.FromResult(false);
		}
	}
}