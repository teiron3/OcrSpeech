using System;
using System.Text;
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

using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using System.Globalization;
namespace OcrSpeech
{
	public static partial class MethodClass
	{

		public static async Task GetTextFromPicture(ViewModel vm, object parameter)
		{

			if (!Clipboard.ContainsImage())
			{
				MessageBox.Show("クリップボードに画像がありません。");
				return;
			}

			try
			{
				CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

			using (var memoryStream = new MemoryStream())
			{
				vm.OcrText = "";
				var bitmapSource = Clipboard.GetImage();
				var maxPixelSize = OcrEngine.MaxImageDimension * 0.8;

				var scale = (maxPixelSize / bitmapSource.Width) > (maxPixelSize / bitmapSource.Height) ? (maxPixelSize / bitmapSource.Height) : (maxPixelSize / bitmapSource.Width);

				var transeformeBitmap = new TransformedBitmap();
				transeformeBitmap.BeginInit();
				transeformeBitmap.Source = bitmapSource;
				transeformeBitmap.Transform = new ScaleTransform(scale, scale);
				transeformeBitmap.EndInit();

				var encoder = new System.Windows.Media.Imaging.BmpBitmapEncoder();
				encoder.Frames.Add(
					System.Windows.Media.Imaging.BitmapFrame.Create(transeformeBitmap)
				);
				encoder.Save(memoryStream);
				var softwareBitmap =
					await (
						await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(memoryStream.AsRandomAccessStream())
					).GetSoftwareBitmapAsync();
				var ocrResult = await OcrEngine.TryCreateFromUserProfileLanguages().RecognizeAsync(softwareBitmap);

				var stringBuilder = new StringBuilder();
				foreach (var line in ocrResult.Lines)
				{
					stringBuilder.Append(line.Text);
				}
				vm.OcrText = stringBuilder.ToString().Replace(" ", "");
			}

			CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture("ja");
			return;
		}
		public static async Task toTextSpeech(ViewModel vm, object parameter)
		{
			if (vm.OcrText.Length < 1) return;
			if (vm.IsSpeakTextPosition)
			{
				if (vm.selectionStart == 0)
				{
					vm.TextSpeech = vm.OcrText + vm.TextSpeech;
					vm.selectionStart = vm.OcrText.Length;
					return;
				}
				if (vm.selectionStart == vm.TextSpeech.Length)
				{
					vm.TextSpeech += vm.OcrText;
					vm.selectionStart = vm.TextSpeech.Length;
					return;
				}
				int len = vm.selectionStart + vm.OcrText.Length;
				vm.TextSpeech = vm.TextSpeech.Substring(0, vm.selectionStart) + vm.OcrText + vm.TextSpeech.Substring(vm.selectionStart);
				vm.selectionStart = len;
				return;
			}
			if (vm.IsSpeakTextEnd)
			{
				vm.TextSpeech += vm.OcrText;
				vm.selectionStart = vm.TextSpeech.Length;
				return;
			}
			if (vm.IsSpeakTextNewLine)
			{
				vm.TextSpeech += "\n" + vm.OcrText;
				vm.selectionStart = vm.TextSpeech.Length;
				return;
			}

		}
		public static async Task ClearSpeakText(ViewModel vm, object parameter)
		{
			vm.TextSpeech = "";
		}

		public static async Task SetClipboardText(ViewModel vm, object parameter)
		{
			Clipboard.SetData(DataFormats.Text, (object)vm.TextSpeech);
		}
	}
}
