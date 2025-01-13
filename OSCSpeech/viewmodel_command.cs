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

namespace OcrSpeech{
    public partial class ViewModel{
        private ICommand _GetTextFromPicture; public ICommand GetTextFromPicture{
            get{
                if(_GetTextFromPicture == null){
                    _GetTextFromPicture = new MakeCommandClass(this, new Func<ViewModel, object, Task>(MethodClass.GetTextFromPicture));
                }
                return _GetTextFromPicture;
            }
        }         private ICommand _toTextSpeech; public ICommand toTextSpeech{
            get{
                if(_toTextSpeech == null){
                    _toTextSpeech = new MakeCommandClass(this, new Func<ViewModel, object, Task>(MethodClass.toTextSpeech));
                }
                return _toTextSpeech;
            }
        }         private ICommand _SetClipboardText; public ICommand SetClipboardText{
            get{
                if(_SetClipboardText == null){
                    _SetClipboardText = new MakeCommandClass(this, new Func<ViewModel, object, Task>(MethodClass.SetClipboardText));
                }
                return _SetClipboardText;
            }
        } 	}
}
