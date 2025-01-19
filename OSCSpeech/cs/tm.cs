using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;


namespace OcrSpeech
{
	public static partial class MethodClass
	{
		private static void GetOcrImage(ViewModel vm)
		{
			try
			{
				if (!Clipboard.ContainsImage()) return;

				if (!vm.IsThreshold)
				{
					vm.OcrImage = Clipboard.GetImage();
					return;
				}

				WriteableBitmap wb = new WriteableBitmap(Clipboard.GetImage());

				int width = wb.PixelWidth;
				int height = wb.PixelHeight;
				int stride = width * wb.Format.BitsPerPixel / 8;
				int bytesPerPixel = (wb.Format.BitsPerPixel + 7) / 8;
				byte[] pixels = new byte[height * stride];
				wb.CopyPixels(pixels, stride, 0);

				// gray
				wb = new WriteableBitmap(width, height, 96, 96, PixelFormats.Gray8, null);
				stride = width * wb.Format.BitsPerPixel / 8;
				byte[] graypixels = new byte[height * stride];

				for (int srcIndex = 0; srcIndex < pixels.Length; srcIndex += bytesPerPixel)
				{
					int dstIndex = srcIndex / bytesPerPixel;
					double stack = 0;
					stack += vm.IsRed ? 0.299 * pixels[srcIndex + 2] : 0;
					stack += vm.IsGreen ? 0.587 * pixels[srcIndex + 1] : 0;
					stack += vm.IsBlue ? 0.114 * pixels[srcIndex] : 0;
					//byte graypixel = (byte)(0.299 * pixels[srcIndex + 2] + 0.587 * pixels[srcIndex + 1] + 0.114 * pixels[srcIndex]);
					graypixels[dstIndex] = (byte)stack;
				}
				//wb.WritePixels(new Int32Rect(0, 0, width, height), graypixels, stride, 0);
				//vm.graybmp = wb;

				// binarize
				wb = new WriteableBitmap(width, height, 96, 96, PixelFormats.BlackWhite, null);
				stride = (width + 7) / 8;
				byte[] binpixels = new byte[stride * height];

				int lineIndex = 0;
				int index = 0;

				for (int srcIndex = 0; srcIndex < graypixels.Length; srcIndex++)
				{
					var bin = (byte)(graypixels[srcIndex] > vm.Threshold ^ vm.IsInvert ? 0 : 1);
					int binIndex = lineIndex % 8;
					binpixels[index] |= (byte)(bin << (7 - binIndex));

					if (lineIndex == width - 1 || lineIndex % 8 == 7) index++;
					lineIndex++;
					if (lineIndex == width) lineIndex = 0;
				}
				wb.WritePixels(new Int32Rect(0, 0, width, height), binpixels, stride, 0);
				vm.OcrImage = wb;
				wb = null;
				GC.Collect();
			}
			catch (Exception ex) { MessageBox.Show(ex.ToString()); }
		}
	}
}

