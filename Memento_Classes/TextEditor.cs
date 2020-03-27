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
        Object CreateState();
        void RestoreState(Object state);
    }

    public class TextEditor : IOriginator
    {
        TextState _currentTextState;


        public object CreateState()
        {
            throw new NotImplementedException();
        }

        public void RestoreState(object state)
        {
            _currentTextState = (TextState)state;
        }
    }

    internal class TextState
    {
        private string _text;

        string GetState()
        {
            return _text;
        }

        void SetState(string text)
        {
            _text = text;
        }        
    }
}