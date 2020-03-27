using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento_Classes
{
    public interface ITextSaver
    {
        bool HandleUndo();
        bool HandleRedo();
        void HandleSaveMemento();
    }

    public class TextSaver : ITextSaver
    {
        //Fields
        private IOriginator _textOriginator;
        private List<object> _undoList;
        private object currentMemento;
        private List<object> _redoList;

        public TextSaver(IOriginator originator)
        {
            _textOriginator = originator;
            _undoList = new List<object>();
            _redoList = new List<object>();
            HandleSaveMemento(); // Start state
        }

        public bool HandleUndo()
        {
            if(_undoList.Count > 0)
            {
                // Create new memento from current state and add to redoList
                var newMemento = _textOriginator.CreateState();

                // equals --> Update current memento and delete from list
                if (currentMemento.Equals(newMemento))
                {
                    currentMemento = _undoList.Last();
                    _undoList.Remove(currentMemento);
                }
                else
                {
                    _redoList.Clear();
                }

                _redoList.Add(newMemento);

                // restore the state
                _textOriginator.RestoreState(currentMemento);

                return true;
            }

            return false;
        }

        public bool HandleRedo()
        {
            if (_redoList.Count > 0)
            {
                // Create new memento from current state and add to redoList
                var newMemento = _textOriginator.CreateState();

                // if equals --> Redo
                if (currentMemento.Equals(newMemento))
                {
                    // Add newMemento
                    _undoList.Add(newMemento);

                    // Set current to last redo-memento and remove
                    currentMemento = _redoList.Last();
                    _redoList.Remove(currentMemento);

                    // Restore state for current
                    _textOriginator.RestoreState(currentMemento);

                    return true;

                }
            }
            
            return false;
        }

        public void HandleSaveMemento()
        {
            var newMemento = _textOriginator.CreateState();

            // Check : Current state for PageTextContent != the newest saved change
            if (_undoList.Count <= 0 || !(newMemento.Equals(_undoList.Last())))
            {
                if(currentMemento != null)
                    _undoList.Add(currentMemento);

                currentMemento = newMemento;

                // Clear redoList
                _redoList.Clear();
            }
        }
    }
}
