using System;
using Xamarin.Forms;

namespace NativeBitmapColorFinder
{
public class BitmapData
{
	public BitmapData(Size size, int[] pixelBuffer)
	{
		Size = size;
		PixelBuffer = pixelBuffer;
	}

	public int[] PixelBuffer { get; }

	public Size Size { get; }

	public Color GetPixel(Point point) => GetPixel(point.X, point.Y);

	public Color GetPixel(double x, double y) => Color.FromUint((uint)PixelBuffer[(int)x * (int)y]);

	public void SetPixel(Point point, Color color) => SetPixel((int)point.X, (int)point.Y, color);

	public void SetPixel(double x, double y, Color color) => PixelBuffer[(int)(x * y)] = (int)(color.A * byte.MaxValue) << 24 |
																						((int)color.R * byte.MaxValue) << 16 |
																						((int)color.G * byte.MaxValue) << 8 |
																						((int)color.B * byte.MaxValue) << 0;


}
}
