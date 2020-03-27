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

            KeyDown += MainWindow_KeyCommand;

            content = new PageTextContent();
            textSaver = new TextSaver((IOriginator)content);
        }

        private void MainWindow_KeyCommand(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.Z) //Undo
                {
                    if (textSaver.HandleUndo())
                    {
                        var revertedText = content.GetText();

                    }

                }
                else if (e.Key == Key.Y) //Redo
                {
                    if (textSaver.HandleRedo())
                    {
                        var revertedText = content.GetText();
                        //this.
                    }
                }
                else if (e.Key == Key.S) //Save
                {
                    textSaver.HandleSaveMemento();
                }
            }
        }

    }
}
