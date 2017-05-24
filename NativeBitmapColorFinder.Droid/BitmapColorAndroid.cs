using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using NativeBitmapColorFinder.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(BitmapColorAndroid))]

namespace NativeBitmapColorFinder.Droid
{
    public class BitmapColorAndroid : IBitmapColor
	{
		public BitmapColorAndroid()
		{
		}

        public async void Convert(ImageSource imageSource)
		{
            Bitmap bitmap = await GetBitmap(imageSource);
            var colorInt = bitmap.GetPixel(bitmap.Width - 10, bitmap.Height/2);
		}

		async Task<Bitmap> GetBitmap(Xamarin.Forms.ImageSource image)
		{
			return await GetImageFromImageSource(image, Forms.Context);
		}

		private async Task<Bitmap> GetImageFromImageSource(ImageSource imageSource, Context context)
		{
			IImageSourceHandler handler;

			if (imageSource is FileImageSource)
			{
				handler = new FileImageSourceHandler();
			}
			else if (imageSource is StreamImageSource)
			{
				handler = new StreamImagesourceHandler(); // sic
			}
			else if (imageSource is UriImageSource)
			{
				handler = new ImageLoaderSourceHandler(); // sic
			}
			else
			{
				throw new NotImplementedException();
			}

            return await handler.LoadImageAsync(imageSource, context);
		}
	}
}
