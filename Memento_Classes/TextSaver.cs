using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento_Classes
{
    /* Interface to caretaker */
    public interface ITextSaver
    {
        void HandleUndo();
        void HandleRedo();
        void HandleSaveMemento();
    }

    /* Caretaker */
    public class TextSaver : ITextSaver
    {
        //Fields
        private List<object> _historyList;      // List that holds all mementos currently saved
        private int _index;                     // Current memento index in list
        private IOriginator _textOriginator;    // Interface to Originator


        public TextSaver(IOriginator originator)
        {
            _textOriginator = originator;
            _historyList = new List<object>();
            _index = -1;
            
            HandleSaveMemento(); // Start state (first memento in list = "")
        }

        public void HandleUndo()
        {
            // If we can undo (There are mementos in the list AND current index is on at least second memento in list)
            if (_historyList.Count > 0 && _index-1 >= 0)
            {
                --_index;
                _textOriginator.RestoreState(_historyList[_index]); // UNDO
            }
        }

        public void HandleRedo()
        {
            // If we can redo (current index is less than amount of mementos)
            if (_index < _historyList.Count - 1)
            {
                ++_index;
                _textOriginator.RestoreState(_historyList[_index]); // REDO
            }
        }

        public void HandleSaveMemento()
        {
            var newMemento = _textOriginator.CreateState();

            // First check is for when the constructor calls this function to add white space "" as first "save"
            // Second check is to make sure we don't make duplicate saves (index is never less than 0 after
            // this function is called in the constructor)
            if (_historyList.Count <= 0 || !(newMemento.Equals(_historyList[_index])))
            {
                ++_index;
                
                _historyList.Insert(_index,newMemento);

                while (_historyList.Count - 1 > _index)
                    _historyList.RemoveAt(_historyList.Count - 1);

            }
        }
    }
}
