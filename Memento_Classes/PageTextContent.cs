using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Memento_Classes
{
    internal interface IOriginator
    {
        // Returns a Momento of current state.
        object CreateState();

        // Sets state based on a given Momento.
        void RestoreState(object state);
    }

    public interface IPageContent
    {
        void DisplayContent();

        void AddText(string text);
    }

    public class PageTextContent : IOriginator, IPageContent
    {
        string _currentText;

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

        public void DisplayContent()
        {
            Console.WriteLine(_currentText);
        }

        public void AddText(string text)
        {
            _currentText += text;
        }
    }

    internal class TextState
    {
        // Momento data.
        private string _text;

        public string GetState()
        {
            return _text;
        }

        public void SetState(string text)
        {
            _text = text;
        }        
    }
}