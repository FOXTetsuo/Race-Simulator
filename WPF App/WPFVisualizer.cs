using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPF_App
{
	public static class WPFVisualizer
	{
		public static BitmapSource DrawTrack(Track track)
		{
			BitmapSource bitmapSource = ImageHandler.CreateBitmapSourceFromGdiBitmap(ImageHandler.DrawImage(0, 0));
			return bitmapSource;
		}
	}
}
