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
			if(vm.playing == "再生中"){
				vm.playing = "停止処理中";
				return;
			}
			if(vm.playing == "停止処理中"){
				return;
			}
			vm.playing = "再生中";
			SpeechSynthesizer synth = new SpeechSynthesizer();
			synth.SetOutputToDefaultAudioDevice();
			vm.selectionLength = 0;
			int len = vm.TextSpeech.Length;
			int start = vm.selectionStart;
			string text = vm.TextSpeech;
			await Task.Delay(100);
			for (int i = start; i < len; i++)
			{
				if(vm.playing == "停止処理中"){
					break;
				}
				synth.Volume = vm.volume;
				synth.Rate = vm.rate;
				vm.IsFocus = true;
				vm.selectionStart = i;
				vm.selectionLength = 1;
				await Task.Run(() => synth.Speak( GetspeakString(text.Substring(i, 1))));
			}
				vm.playing = "再生";
				vm.IsFocus = true;
				vm.IsFocus = false;
		}
		
		private static string GetspeakString(string text){
			string result;
			switch (text)
			{
				case "0":
					result = "ゼロ";
					break;
				case "1":
					result = "ひと";
					break;
				case "2":
					result = "ふた";
					break;
				case "9":
					result = "数字の9";
					break;

				case "a":
				case "A":
					result = "えい";
					break;

				case "c":
					result = "小文字のC";
					break;
				case "C":
					result = "大文字のC";
					break;

				case "o":
					result = "小文字のオー";
					break;
				case "O":
					result = "大文字のオー";
					break;

				case "q":
				case "Q":
					result = "アルファベットのQ";
					break;

				case "s":
					result = "小文字のS";
					break;
				case "S":
					result = "大文字のS";
					break;

				case "x":
					result = "小文字のx";
					break;
				case "X":
					result = "大文字のX";
					break;
				default:
					result = text;
					break;
			}
			return result;
		}

	}
}
