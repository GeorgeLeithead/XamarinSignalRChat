// <copyright file="ExtendedEditorControl.cs" company="InternetWideWorld.com">
// Copyright (c) George Leithead, InternetWideWorld.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>

namespace XamarinSignalRChat.Controls
{
	using Xamarin.Forms;

	/// <summary>Extended editor control.</summary>
	public class ExtendedEditorControl : Editor
	{
		/// <summary>Gets or sets the HasRoundedCorner Property, and it is a bindable property.</summary>
		public static readonly BindableProperty HasRoundedCornerProperty = BindableProperty.Create(nameof(HasRoundedCorner), typeof(bool), typeof(ExtendedEditorControl), false);

		/// <summary>Gets or sets the IsExpandable Property, and it is a bindable property.</summary>
		public static readonly BindableProperty IsExpandableProperty = BindableProperty.Create(nameof(IsExpandable), typeof(bool), typeof(ExtendedEditorControl), false);

		/// <summary>Gets or sets the PlaceholderColor Property, and it is a bindable property.</summary>
		public static new readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(ExtendedEditorControl), Color.LightGray);

		/// <summary>Gets or sets the PlaceholderProperty Property, and it is a bindable property.</summary>
		public static new readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ExtendedEditorControl));

		/// <summary>Initialises a new instance of the <see cref="ExtendedEditorControl"/> class.</summary>
		public ExtendedEditorControl()
		{
			this.TextChanged += this.OnTextChanged;
		}

		/// <summary>Finalises an instance of the <see cref="ExtendedEditorControl"/> class.</summary>
		~ExtendedEditorControl()
		{
			this.TextChanged -= this.OnTextChanged;
		}

		/// <summary>Gets or sets a value indicating whether the control has rounded corners.</summary>
		public bool HasRoundedCorner
		{
			get { return (bool)this.GetValue(HasRoundedCornerProperty); }
			set { this.SetValue(HasRoundedCornerProperty, value); }
		}

		/// <summary>Gets or sets a value indicating whether the control is expandable.</summary>
		public bool IsExpandable
		{
			get { return (bool)this.GetValue(IsExpandableProperty); }
			set { this.SetValue(IsExpandableProperty, value); }
		}

		/// <summary>Gets or sets the place holder.</summary>
		public new string Placeholder
		{
			get { return (string)this.GetValue(PlaceholderProperty); }
			set { this.SetValue(PlaceholderProperty, value); }
		}

		/// <summary>Gets or sets the place holder color.</summary>
		public new Color PlaceholderColor
		{
			get { return (Color)this.GetValue(PlaceholderColorProperty); }
			set { this.SetValue(PlaceholderColorProperty, value); }
		}

		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.IsExpandable)
			{
				this.InvalidateMeasure();
			}
		}
	}
}