
namespace XamarinSignalRChat.Droid
{
	using Plugin.CurrentActivity;
	using XamarinSignalRChat.Interfaces;

	/// <summary>Simple platform specific service that is responsible for locating a parent window service.</summary>
	internal class AndroidParentWindowLocatorService : IParentWindowLocatorService
	{
		/// <summary>Get current parent window.</summary>
		/// <returns>Current parent window object.</returns>
		public object GetCurrentParentWindow()
		{
			return CrossCurrentActivity.Current.Activity;
		}
	}
}