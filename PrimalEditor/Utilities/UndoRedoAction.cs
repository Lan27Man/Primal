// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;

namespace PrimalEditor.Utilities
{
    public class UndoRedoAction : IUndoRedo
    {
        #region Fields

        private Action _undoAction;

        private Action _redoAction;

        #endregion

        #region Properties

        public string Name { get; }

        #endregion

        #region Constructors

        public UndoRedoAction(string name)
        {
            Name = name;
        }

        public UndoRedoAction(Action undo, Action redo, string name)
            : this(name)
        {
            Debug.Assert(undo != null && redo != null);

            _undoAction = undo;
            _redoAction = redo;
        }

        public UndoRedoAction(string property, object instance, object undoValue, object redoValue, string name)
            : this(
                  () => instance.GetType().GetProperty(property).SetValue(instance, undoValue),
                  () => instance.GetType().GetProperty(property).SetValue(instance, redoValue),
                  name
            )
        {

        }

        #endregion

        #region Methods

        public void Undo()
        {
            _undoAction.Invoke();
        }

        public void Redo()
        {
            _redoAction.Invoke();
        }

        #endregion
    }
}
