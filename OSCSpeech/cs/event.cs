using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
namespace OcrSpeech
{
	public partial class MainWindow : Window
	{
		private void FileListBox_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
				foreach (var name in fileNames)
				{
					//処理
				}
			}
		}

		private void FileListBox_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effects = DragDropEffects.All;
			}
			else
			{
				e.Effects = DragDropEffects.None;
			}
			e.Handled = true;
		}

		private void DoubleClick(object sender, MouseButtonEventArgs e)
		{
			//処理
		}
	}
}
/*	

	
	public class TextBoxHelper
	{
		public static readonly DependencyProperty SelectionStartProperty =
			DependencyProperty.RegisterAttached(
				"SelectionStart",
				typeof(int),
				typeof(TextBoxHelper),
				new PropertyMetadata(OnSelectionStartChanged)
			);
		public static int GetSelectionStart(DependencyObject obj)
		{
			return (int)obj.GetValue(SelectionStartProperty);
		}
		public static void SetSelectionStart(DependencyObject obj, int value)
		{
			if (GetSelectionStart(obj) != value)
			{
				obj.SetValue(SelectionStartProperty, value);
			}
		}
		private static void OnSelectionStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var textBox = d as TextBox;
			if (textBox != null && textBox.SelectionStart != (int)e.NewValue)
			{
				textBox.SelectionChanged -= TextBox_SelectionChanged;
				textBox.SelectionChanged += TextBox_SelectionChanged;
				textBox.SelectionStart = (int)e.NewValue;
			}
		}
		private static void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			var textBox = sender as TextBox;
			if (textBox == null) return;
			SetSelectionStart(textBox, textBox.SelectionStart);
			SetSelectionLength(textBox, textBox.SelectionLength);
		}

		public static readonly DependencyProperty SelectionLengthProperty =
			DependencyProperty.RegisterAttached(
				"SelectionLength",
				typeof(int),
				typeof(TextBoxHelper),
				new PropertyMetadata(OnSelectionLengthChanged)
			);
		public static int GetSelectionLength(DependencyObject obj)
		{
			return (int)obj.GetValue(SelectionLengthProperty);
		}
		public static void SetSelectionLength(DependencyObject obj, int value)
		{
			obj.SetValue(SelectionLengthProperty, value);
		}

		private static void OnSelectionLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var textBox = d as TextBox;
			if (textBox == null) return;
			textBox.SelectionLength = (int)e.NewValue;
		}

		public static readonly DependencyProperty getFocusProperty =
			DependencyProperty.RegisterAttached(
				"getFocus",
				typeof(int),
				typeof(TextBoxHelper),
				new PropertyMetadata(OngetFocusChanged)
			);
		public static bool GetgetFocus(DependencyObject obj)
		{
			return (bool)obj.GetValue(getFocusProperty);
		}
		public static void SetgetFocus(DependencyObject obj, bool value)
		{
			obj.SetValue(getFocusProperty, value);
			if (value)
			{
				(obj as TextBox).Focus();
			}
		}

		private static void OngetFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{

		}
}

