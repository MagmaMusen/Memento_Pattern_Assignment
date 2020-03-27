using Memento_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memento_WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPageContent content;
        private ITextSaver textSaver;

        public MainWindow()
        {
            InitializeComponent();

            //KeyDown += MainWindow_KeyCommand;

            content = new PageTextContent();
            textSaver = new TextSaver((IOriginator)content);
        }

        //private void MainWindow_KeyCommand(object sender, KeyEventArgs e)
        //{
        //    if (Keyboard.Modifiers == ModifierKeys.Control)
        //    {
        //        if (e.Key == Key.U) //Undo
        //        {
        //            if (textSaver.HandleUndo())
        //            {
        //                var revertedText = content.GetText();
        //                EditorTxb.Text = revertedText;

        //            }

        //        }
        //        else if (e.Key == Key.R) //Redo
        //        {
        //            if (textSaver.HandleRedo())
        //            {
        //                var revertedText = content.GetText();
        //                EditorTxb.Text = revertedText;
        //            }
        //        }
        //        else if (e.Key == Key.S) //Save
        //        {
        //            textSaver.HandleSaveMemento();
        //        }
        //    }
        //}

        private void EditorTxb_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            content.SetText(EditorTxb.Text);
        }

        private void UndoBut_OnClick(object sender, RoutedEventArgs e)
        {
            if (textSaver.HandleUndo())
            {
                EditorTxb.Text = content.GetText();

            }
        }

        private void RedoBut_OnClick(object sender, RoutedEventArgs e)
        {
            if (textSaver.HandleRedo())
            {
                EditorTxb.Text = content.GetText();
            }
        }

        private void SaveBut_OnClick(object sender, RoutedEventArgs e)
        {
            textSaver.HandleSaveMemento();
        }
    }
}
