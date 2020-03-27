using Memento_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// Måske er det lettere bare at bruge WPF fra starten af.
namespace Memento_App
{
    class Program
    {
        static void Main(string[] args)
        {

            //var wind = new TextEditorWindow();

            //wind.Activate();

            //while(wind.IsActive) { };

        }
    }

    public partial class TextEditorWindow : Window
    {
        private IPageContent content;
        private ITextSaver textSaver;

        public TextEditorWindow()
        {
            //InitializeComponent(); ??
            this.Title = "TextEditor-3000";
            this.Height = 300;
            this.Width = 300;
            this.KeyUp += TextEditorWindow_KeyUp;
            var txb1 = new TextBox();
            this.AddChild(txb1);

            content = new PageTextContent();
            textSaver = new TextSaver((IOriginator)content);

        }

        private void TextEditorWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.Z) //Undo
                {
                    if(textSaver.HandleUndo())
                    {
                        var revertedText = content.GetText();

                    }

                }
                else if(e.Key == Key.Y) //Redo
                {
                    if(textSaver.HandleRedo())
                    {
                        var revertedText = content.GetText();
                        //this.
                    }
                }
                else if(e.Key == Key.S) //Save
                {
                    textSaver.HandleSaveMemento();
                }
            }
            else // Write text in textbox
            {

            }
        }
    }
}
