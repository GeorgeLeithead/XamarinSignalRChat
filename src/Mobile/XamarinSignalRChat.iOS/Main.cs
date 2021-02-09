namespace XamarinSignalRChat.iOS
{
	using UIKit;

	/// <inheritdoc/>
	public class Application
    {
		/// <summary>This is the main entry point of the application.</summary>
		/// <param name="args">Entry arguments.</param>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate"); // if you want to use a different Application Delegate class from "AppDelegate" you can specify it here.
		}
    }
}
