using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

 namespace OcrSpeech
{ public partial class ViewModel{
 		private System.Windows.Media.Imaging.BitmapSource _OcrImage = null;
        public System.Windows.Media.Imaging.BitmapSource OcrImage{
            get{ return _OcrImage;}
            set{
                if(_OcrImage == value){
                    return;
                }else{
                    _OcrImage = value;
                    NotifyPropertyChanged("OcrImage");
                }
            }
        }
		private bool _IsThreshold = false;
        public bool IsThreshold{
            get{ return _IsThreshold;}
            set{
                if(_IsThreshold == value){
                    return;
                }else{
                    _IsThreshold = value;
                    NotifyPropertyChanged("IsThreshold");
                }
            }
        }
		private int _Threshold = 100;
        public int Threshold{
            get{ return _Threshold;}
            set{
                if(_Threshold == value){
                    return;
                }else{
                    _Threshold = value;
                    NotifyPropertyChanged("Threshold");
                }
            }
        }
		private bool _IsInvert = false;
        public bool IsInvert{
            get{ return _IsInvert;}
            set{
                if(_IsInvert == value){
                    return;
                }else{
                    _IsInvert = value;
                    NotifyPropertyChanged("IsInvert");
                }
            }
        }
		private string _OcrText = "";
        public string OcrText{
            get{ return _OcrText;}
            set{
                if(_OcrText == value){
                    return;
                }else{
                    _OcrText = value;
                    NotifyPropertyChanged("OcrText");
                }
            }
        }
		private bool _IsSpeakTextPosition = true;
        public bool IsSpeakTextPosition{
            get{ return _IsSpeakTextPosition;}
            set{
                if(_IsSpeakTextPosition == value){
                    return;
                }else{
                    _IsSpeakTextPosition = value;
                    NotifyPropertyChanged("IsSpeakTextPosition");
                }
            }
        }
		private bool _IsSpeakTextEnd      = false;
        public bool IsSpeakTextEnd     {
            get{ return _IsSpeakTextEnd     ;}
            set{
                if(_IsSpeakTextEnd      == value){
                    return;
                }else{
                    _IsSpeakTextEnd      = value;
                    NotifyPropertyChanged("IsSpeakTextEnd     ");
                }
            }
        }
		private bool _IsSpeakTextNewLine  = false;
        public bool IsSpeakTextNewLine {
            get{ return _IsSpeakTextNewLine ;}
            set{
                if(_IsSpeakTextNewLine  == value){
                    return;
                }else{
                    _IsSpeakTextNewLine  = value;
                    NotifyPropertyChanged("IsSpeakTextNewLine ");
                }
            }
        }
		private string _TextSpeech = "";
        public string TextSpeech{
            get{ return _TextSpeech;}
            set{
                if(_TextSpeech == value){
                    return;
                }else{
                    _TextSpeech = value;
                    NotifyPropertyChanged("TextSpeech");
                }
            }
        }
	}
}
