// <copyright file="RemoteUserViewCell.xaml.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Views.Cells
{
	using Xamarin.Forms;
	using Xamarin.Forms.Xaml;

	/// <summary>Incoming chat view cell.</summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RemoteUserViewCell : ViewCell
	{
		/// <summary>Initialises a new instance of the <see cref="RemoteUserViewCell"/> class.</summary>
		public RemoteUserViewCell()
		{
			this.InitializeComponent();
		}
	}
}