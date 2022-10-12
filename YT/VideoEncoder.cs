using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT
{
	internal class VideoEncoder
	{
		// 1 - Define a delegate
		// 2 - Define an event based on that delegate
		// 3 - Raise the event

		// 1 delegate holds a reference to a method
		public delegate void VideoEncodedEventHandler(object sender, EventArgs args);
		// 2
		public event VideoEncodedEventHandler VideoEncoded;

		public void Encode(Video video)
		{
			Console.WriteLine("Encoding");
			OnVideoEncoded();
		}

		protected virtual void OnVideoEncoded()
		{
			if (VideoEncoded != null)
			{
				VideoEncoded(this, EventArgs.Empty);
			}
		}

	}
}
