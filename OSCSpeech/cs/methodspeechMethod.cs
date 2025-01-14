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
			synth.Rate = vm.rate;
			int len = vm.TextSpeech.Length;
			for (int i = 0; i < len; i++)
			{
				vm.IsFocus = true;
				vm.selectionStart = i;
				vm.selectionLength = 1;
				await Task.Delay(100);
				synth.Speak(vm.TextSpeech.Substring(i, 1));
			}
			
		}
	}
}
