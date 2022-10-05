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

		public static Bitmap DrawImage(int x, int y)
		{
			
			Bitmap newBitmap = GetBitmap("C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\HorizontalXL..png");
			Graphics graphics = Graphics.FromImage(newBitmap);

			//misschien kloten met solidBrush?
			graphics.Clear(Color.Aquamarine);

			Bitmap clone = newBitmap.Clone(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), PixelFormat.Format32bppArgb);
			return (clone);
		}

		//public static Bitmap DrawImage(int x, int y)
		//{
		//	if (!_imageCache.ContainsKey("empty"))
		//	{
		//		Bitmap bitmap = new Bitmap(192, 192);
		//		Graphics graphics = Graphics.FromImage(bitmap);
		//		graphics.FillRegion(new SolidBrush(Color.White), new Region(new Rectangle(0, 0, 192, 192)));
		//		_imageCache.Add("empty", bitmap);
		//		return bitmap;
		//	}
		//	else return _imageCache["empty"];
		//}
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
