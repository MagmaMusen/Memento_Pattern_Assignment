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
    public partial class MainWindow : Window
    {
        private IPageTextContent content; //Interface to originator
        private ITextSaver textSaver; //Interface to caretaker

        public MainWindow()
        {
            InitializeComponent();

            content = new PageTextContent();
            textSaver = new TextSaver((IOriginator)content); //textSaver needs reference to same originator as content
        }

        // Links EditorTxb to instance "content"
        // On change --> update content via interface IPageContent
        private void EditorTxb_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            content.SetText(EditorTxb.Text);
        }

        // Event: Push Undo-button
        private void UndoBut_OnClick(object sender, RoutedEventArgs e)
        {
            textSaver.HandleUndo();
            EditorTxb.Text = content.GetText();
        }

        // Event: Push Redo-button
        private void RedoBut_OnClick(object sender, RoutedEventArgs e)
        {
            textSaver.HandleRedo();
            EditorTxb.Text = content.GetText();
        }

        // Event: Push Save-button
        private void SaveBut_OnClick(object sender, RoutedEventArgs e)
        {
            textSaver.HandleSaveMemento();
        }
    }
}
