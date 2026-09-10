// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

namespace PrimalEditor.Utilities
{
    public interface IUndoRedo
    {
        #region Properties

        string Name { get; }

        #endregion

        #region Methods

        void Undo();

        void Redo();

        #endregion
    }
}
