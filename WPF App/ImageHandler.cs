using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace WPF_App
{
	public static class ImageHandler
	{
		private static Dictionary<string, Bitmap> _imageCache = new Dictionary<string, Bitmap>();

		public static Bitmap GetBitmap(String strng)
		{
			if (!_imageCache.ContainsKey(strng))
			{
				_imageCache.Add(strng, new Bitmap(strng));
			}
			return _imageCache[strng];
		}
		public static void Clear()
		{
			_imageCache.Clear();
		}

		public static Bitmap DrawImage(int x, int y, string image)
		{
			Bitmap newBitmap = GetBitmap(image);
			Bitmap clone = newBitmap.Clone(new Rectangle(x, y, newBitmap.Width, newBitmap.Height), PixelFormat.Format32bppArgb);
			return (clone);
		}

		public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
		{
			if (bitmap == null)
				throw new ArgumentNullException("bitmap");

			var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

			var bitmapData = bitmap.LockBits(
				rect,
				ImageLockMode.ReadWrite,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			try
			{
				var size = (rect.Width * rect.Height) * 4;

				return BitmapSource.Create(
					bitmap.Width,
					bitmap.Height,
					bitmap.HorizontalResolution,
					bitmap.VerticalResolution,
					PixelFormats.Bgra32,
					null,
					bitmapData.Scan0,
					size,
					bitmapData.Stride);
			}
			finally
			{
				bitmap.UnlockBits(bitmapData);
			}
		}


	}
}
