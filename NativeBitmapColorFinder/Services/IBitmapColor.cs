using System;
using Xamarin.Forms;

namespace NativeBitmapColorFinder
{
	public interface IBitmapColor
	{
		void Convert(ImageSource nativeBitmap);

	}
}
