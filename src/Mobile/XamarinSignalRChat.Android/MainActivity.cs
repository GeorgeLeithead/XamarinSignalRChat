namespace XamarinSignalRChat.Droid
{
	using Android.App;
	using Android.Content.PM;
	using Android.OS;
	using Android.Runtime;
	using Plugin.CurrentActivity;
	using Xamarin.Forms;
	using XamarinSignalRChat.Interfaces;

	/// <inheritdoc/>
	[Activity(Label = "XamarinSignalRChat", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		/// <inheritdoc/>
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		/// <inheritdoc/>
		protected override void OnCreate(Bundle savedInstanceState)
		{
			CrossCurrentActivity.Current.Init(this, savedInstanceState);
			DependencyService.Register<IParentWindowLocatorService, AndroidParentWindowLocatorService>();

			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			Forms.Init(this, savedInstanceState);
			CrossCurrentActivity.Current.Init(this, savedInstanceState);
			LoadApplication(new App());
		}
	}
}