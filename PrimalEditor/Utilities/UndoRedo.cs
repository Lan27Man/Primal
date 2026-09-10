// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Collections.ObjectModel;
using System.Linq;

namespace PrimalEditor.Utilities
{
    public class UndoRedo
    {
        #region Fields

        private bool _enableAdd = true;

        private readonly ObservableCollection<IUndoRedo> _undoList = new ObservableCollection<IUndoRedo>();

        private readonly ObservableCollection<IUndoRedo> _redoList = new ObservableCollection<IUndoRedo>();

        #endregion

        #region Properties

        public ReadOnlyObservableCollection<IUndoRedo> UndoList { get; }

        public ReadOnlyObservableCollection<IUndoRedo> RedoList { get; }

        #endregion

        #region Constructor

        public UndoRedo()
        {
            UndoList = new ReadOnlyObservableCollection<IUndoRedo>(_undoList);
            RedoList = new ReadOnlyObservableCollection<IUndoRedo>(_redoList);
        }

        #endregion

        #region Methods

        public void Reset()
        {
            _undoList.Clear();
            _redoList.Clear();
        }

        public void Add(IUndoRedo cmd)
        {
            if (_enableAdd)
            {
                _undoList.Add(cmd);
                _redoList.Clear();
            }
        }

        public void Undo()
        {
            if (_undoList.Any())
            {
                var cmd = _undoList.Last();

                _undoList.RemoveAt(_undoList.Count - 1);
                _enableAdd = false;

                cmd.Undo();

                _enableAdd = true;
                _redoList.Insert(0, cmd);
            }
        }

        public void Redo()
        {
            if (_redoList.Any())
            {
                var cmd = _redoList.First();

                _redoList.RemoveAt(0);
                _enableAdd = false;

                cmd.Redo();

                _enableAdd = true;
                _undoList.Add(cmd);
            }
        }

        #endregion
    }
}
