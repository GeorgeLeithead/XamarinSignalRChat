namespace XamarinSignalRChat.iOS
{
	using Foundation;
	using UIKit;

	/// <summary>The UIApplicationDelegate for the application. This class is responsible for launching the User Interface of the application, as well as listening (and optionally responding) to application events from iOS.</summary>
	[Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		/// <summary>This method is invoked when the application has loaded and is ready to run. In this method you should instantiate the window, load the UI into it and then make the window visible.</summary>
		/// <param name="app">UI Application</param>
		/// <param name="options">Name Space Dictionary Options.</param>
		/// <returns>Finished launching app.</returns>
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
