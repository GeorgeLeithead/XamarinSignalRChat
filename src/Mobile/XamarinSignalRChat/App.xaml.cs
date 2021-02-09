// <copyright file="App.xaml.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat
{
	using Xamarin.Forms;
	using XamarinSignalRChat.Interfaces;
	using XamarinSignalRChat.Shared;

	/// <summary>App constructor.</summary>
	public partial class App : Application
	{
		/// <summary>Initialises a new instance of the <see cref="App"/> class.</summary>
		public App()
		{
			// StyleSheetRegistrar.RegisterStyle("-xf-horizontal-options", typeof(VisualElement), nameof(View.HorizontalOptionsProperty));
			// StyleSheetRegistrar.RegisterStyle("-xf-shell-navbarhasshadow", typeof(Shell), nameof(Shell.NavBarHasShadowProperty));
			this.InitializeComponent();
			DependencyService.Register<ChatService>();
			DependencyService.Resolve<IDialogService>();

			this.MainPage = new AppShell();
		}

		/// <summary>Gets or sets the UI parent object.</summary>
		public static object UIParent { get; set; } = null;

		/// <inheritdoc/>
		protected override void OnResume()
		{
		}

		/// <inheritdoc/>
		protected override void OnSleep()
		{
		}

		/// <inheritdoc/>
		protected override void OnStart()
		{
			base.OnStart();
		}
	}
}