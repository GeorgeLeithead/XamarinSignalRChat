// <copyright file="ExtendedListView.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Controls
{
	using System;
	using System.Linq;
	using System.Windows.Input;
	using Xamarin.Forms;

	/// <summary>Extended list view controls with scroll to bottom.</summary>
	public class ExtendedListView : ListView
	{
		/// <summary>Gets or sets the ItemAppearingCommand Property, and it is a bindable property.</summary>
		public static readonly BindableProperty ItemAppearingCommandProperty = BindableProperty.Create(nameof(ItemAppearingCommand), typeof(ICommand), typeof(ExtendedListView), default(ICommand));

		/// <summary>Gets or sets the ItemDisappearingCommand Property, and it is a bindable property.</summary>
		public static readonly BindableProperty ItemDisappearingCommandProperty = BindableProperty.Create(nameof(ItemDisappearingCommand), typeof(ICommand), typeof(ExtendedListView), default(ICommand));

		/// <summary>Gets or sets the TappedCommand Property, and it is a bindable property.</summary>
		public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(ExtendedListView), default(ICommand));

		/// <summary>Initialises a new instance of the <see cref="ExtendedListView"/> class.</summary>
		public ExtendedListView()
			: this(ListViewCachingStrategy.RecycleElement)
		{
		}

		/// <summary>Initialises a new instance of the <see cref="ExtendedListView"/> class.</summary>
		/// <param name="cachingStrategy">Cache strategy.</param>
		public ExtendedListView(ListViewCachingStrategy cachingStrategy)
			: base(cachingStrategy)
		{
			this.ItemSelected += this.OnItemSelected;
			this.ItemTapped += this.OnItemTapped;
			this.ItemAppearing += this.OnItemAppearing;
			this.ItemDisappearing += this.OnItemDisappering;
		}

		/// <summary>Gets or sets the Item appearing command.</summary>
		public ICommand ItemAppearingCommand
		{
			get => (ICommand)this.GetValue(ItemAppearingCommandProperty);
			set => this.SetValue(ItemAppearingCommandProperty, value);
		}

		/// <summary>Gets or sets the item disappearing command.</summary>
		public ICommand ItemDisappearingCommand
		{
			get => (ICommand)this.GetValue(ItemDisappearingCommandProperty);
			set => this.SetValue(ItemDisappearingCommandProperty, value);
		}

		/// <summary>Gets or sets the tapped command.</summary>
		public ICommand TappedCommand
		{
			get => (ICommand)this.GetValue(TappedCommandProperty);
			set => this.SetValue(TappedCommandProperty, value);
		}

		/// <summary>Scroll to fist item in list.</summary>
		public void ScrollToFirst()
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				try
				{
					if (this.ItemsSource != null && this.ItemsSource.Cast<object>().Count() > 0)
					{
						object msg = this.ItemsSource.Cast<object>().FirstOrDefault();
						if (msg != null)
						{
							this.ScrollTo(msg, ScrollToPosition.Start, false);
						}
					}
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.ToString());
				}
			});
		}

		/// <summary>Scroll to last item in list.</summary>
		public void ScrollToLast()
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				try
				{
					if (this.ItemsSource != null && this.ItemsSource.Cast<object>().Count() > 0)
					{
						object msg = this.ItemsSource.Cast<object>().LastOrDefault();
						if (msg != null)
						{
							this.ScrollTo(msg, ScrollToPosition.End, false);
						}
					}
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.ToString());
				}
			});
		}

		private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			if (this.ItemAppearingCommand != null)
			{
				this.ItemAppearingCommand?.Execute(e.Item);
			}
		}

		private void OnItemDisappering(object sender, ItemVisibilityEventArgs e)
		{
			this.ItemDisappearingCommand?.Execute(e.Item);
		}

		private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			ExtendedListView listView = (ExtendedListView)sender;
			if (e == null)
			{
				return;
			}

			listView.SelectedItem = null;
		}

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (this.TappedCommand != null)
			{
				this.TappedCommand?.Execute(e.Item);
			}

			this.SelectedItem = null;
		}
	}
}