using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento_Classes
{
    public interface ITextSaver
    {
        void HandleUndo();
        void HandleRedo();
        void HandleSaveMemento();
    }

    public class TextSaver : ITextSaver
    {
        //Fields
        IOriginator _textOriginator;
        List<object> _undoList;
        List<object> _redoList;

        public TextSaver()
        {
            _textOriginator = new TextEditor();
            _undoList = new List<object>();
            _redoList = new List<object>();
        }

        public void HandleUndo()
        {
            if(_undoList.Count > 0)
            {
                // Create new memento from current state and add to redoList
                var newMemento = _textOriginator.CreateState();
                _redoList.Add(newMemento);

                // Get newest undo-memento, restore the state and delete from list
                var mementoToRestore = _undoList.Last();
                _textOriginator.RestoreState(mementoToRestore);
                _undoList.Remove(mementoToRestore);
            }
        }

        public void HandleRedo()
        {
            if (_redoList.Count > 0)
            {
                // Create new memento from current state and add to redoList
                var newMemento = _textOriginator.CreateState();
                _undoList.Add(newMemento);

                // Get newest redo-memento, restore the state and delete from list
                var mementoToRestore = _redoList.Last();
                _textOriginator.RestoreState(mementoToRestore);
                _redoList.Remove(mementoToRestore);
            }
        }

        public void HandleSaveMemento()
        {
            // Check : Current state for PageTextContent != the newest saved change
            if(_textOriginator.CreateState() != _undoList.Last())
            {
                var newMemento = _textOriginator.CreateState();
                _undoList.Add(newMemento);

                // Clear redoList
                _redoList.Clear();
            }
        }
    }
}
