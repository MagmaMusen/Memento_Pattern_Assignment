using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Memento_Classes
{
    public interface IOriginator
    {
        // Returns a Memento of current state.
        object CreateState();

        // Sets state based on a given Memento.
        void RestoreState(object state);
    }

    public interface IPageTextContent
    {

        string GetText();
        void SetText(string text);

    }

    public class PageTextContent : IOriginator, IPageTextContent
    {
        private string _currentText = "";

        // Returns current text in a new TextState object (TextState is a Memento).
        public object CreateState()
        {
            TextState currentTextState = new TextState();
            currentTextState.SetState(_currentText);
            return currentTextState;
        }

        // Sets current text equal to given TextState object's text (TextState is a Memento).
        public void RestoreState(object state)
        {
            _currentText = ((TextState)state).GetState();
        }

        /* Simulates that the textbox in WPF is this class (Should actually have extended textbox in wpf with the functionality of this class) */
        public string GetText()
        {
            return _currentText;
        }
        public void SetText(string text)
        {
            _currentText = text;
        }

    }

    internal class TextState
    {
        // Memento data.
        private string _text;

        public string GetState()
        {
            return _text;
        }

        public void SetState(string text)
        {
            _text = text;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TextState t = (TextState)obj;
                return (_text == t._text);
            }
        }
    }
}