// <copyright file="SettingsPage.xaml.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Views
{
	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;

	/// <summary>Settings page.</summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="SettingsPage"/> class.
		/// </summary>
		public SettingsPage()
		{
			this.InitializeComponent();
		}

		/// <inheritdoc/>
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			if (!DesignMode.IsDesignModeEnabled)
			{
				Shell.Current.GoToAsync($"//Chat");
			}
		}
	}
}