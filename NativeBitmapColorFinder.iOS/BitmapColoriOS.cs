using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using CoreGraphics;
using NativeBitmapColorFinder.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(BitmapColoriOS))]


namespace NativeBitmapColorFinder.iOS
{
	public class BitmapColoriOS : IBitmapColor
	{
		public BitmapColoriOS()
		{
		}

		public BitmapData Convert(object nativeBitmap)
		{
			throw new NotImplementedException();
		}

        public async void Convert(ImageSource nativeBitmap)
        {
            UIImage iOSImage = await ToUIImage(nativeBitmap);
            var pixels = GetPixels(iOSImage);
        }

        private ImageLoaderSourceHandler s_imageLoaderSourceHandler = new ImageLoaderSourceHandler();

		public Task<UIImage> ToUIImage(ImageSource imageSource)
		{
			return s_imageLoaderSourceHandler.LoadImageAsync(imageSource);
		}


		private static int[] GetPixels(UIImage image)
		{
			const int bytesPerPixel = 4;
			const int bitsPerComponent = 8;
			const CGBitmapFlags flags = CGBitmapFlags.ByteOrder32Big | CGBitmapFlags.PremultipliedLast;

			var width = (int)image.CGImage.Width;
			var height = (int)image.CGImage.Height;
			var bytesPerRow = bytesPerPixel * width;
			var buffer = new byte[bytesPerRow * height];
			var pixels = new int[width * height];

			var handle = GCHandle.Alloc(buffer);
			try
			{
				using (var colorSpace = CGColorSpace.CreateGenericRgb())
				using (var context = new CGBitmapContext(buffer, width, height, bitsPerComponent, bytesPerRow, colorSpace, flags))
					context.DrawImage(new RectangleF(0, 0, width, height), image.CGImage);

                for (var y = height/2; y < (height/2)+1; y++)
				{
					var offset = y * width;
                    for (var x = width-10; x < width -9; x++)
					{
						var idx = bytesPerPixel * (offset + x);
						var r = buffer[idx + 0];
						var g = buffer[idx + 1];
						var b = buffer[idx + 2];
						var a = buffer[idx + 3];
						pixels[x * y] = a << 24 | r << 16 | g << 8 | b << 0;
					}
				}
			}
			finally
			{
				handle.Free();
			}

			return pixels;
		}

		private static IImageSourceHandler GetHandler(ImageSource source)
		{
			IImageSourceHandler returnValue = null;
			if (source is UriImageSource)
			{
				returnValue = new ImageLoaderSourceHandler();
			}
			else if (source is FileImageSource)
			{
				returnValue = new FileImageSourceHandler();
			}
			else if (source is StreamImageSource)
			{
				returnValue = new StreamImagesourceHandler();
			}
			return returnValue;
		}

		public static async Task<UIImage> GetUIImageFromImageSourceAsync(ImageSource source)
		{
			var handler = GetHandler(source);
			var returnValue = (UIImage)null;

            returnValue = await handler.LoadImageAsync(source);

			return returnValue;
		}

	
    }
}
