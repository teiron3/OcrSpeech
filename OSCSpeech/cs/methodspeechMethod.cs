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
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace OcrSpeech
{
	public static partial class MethodClass
	{
		public static async Task SpeakChar(ViewModel vm, object parameter)
		{
			SpeechSynthesizer synth = new SpeechSynthesizer();
			synth.SetOutputToDefaultAudioDevice();
			synth.Volume = vm.volume;
			synth.Rate = -5;
			vm.selectionLength = 0;
			int len = vm.TextSpeech.Length;
			int start = vm.selectionStart;
			string text = vm.TextSpeech;
			await Task.Delay(100);
			for (int i = start; i < len; i++)
			{
				vm.IsFocus = true;
				vm.selectionStart = i;
				vm.selectionLength = 1;
				await Task.Run(() => synth.Speak(text.Substring(i, 1)));
			}
				vm.IsFocus = false;
			
		}
	}
}
