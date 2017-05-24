using Xamarin.Forms;

namespace NativeBitmapColorFinder.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			Globals.colorWheel = ColorWheel;
		}
	}
}

