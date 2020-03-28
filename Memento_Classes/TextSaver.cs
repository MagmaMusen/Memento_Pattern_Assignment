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
        private List<object> _historyList;
        private int _index;
        private IOriginator _textOriginator;


        public TextSaver(IOriginator originator)
        {
            _textOriginator = originator;
            _historyList = new List<object>();
            _index = -1;
            
            HandleSaveMemento(); // Start state
        }

        public void HandleUndo()
        {
            if (_historyList.Count > 0 && _index-1 >= 0)
            {
                --_index;
                _textOriginator.RestoreState(_historyList[_index]);
            }
        }

        public void HandleRedo()
        {
            if (_index < _historyList.Count - 1)
            {
                ++_index;
                _textOriginator.RestoreState(_historyList[_index]);
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

                while(_historyList.Count-1 > _index)
                    _historyList.RemoveAt(_historyList.Count-1);
            }
        }
    }
}
