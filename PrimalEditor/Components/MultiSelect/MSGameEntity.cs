// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace PrimalEditor.Components
{
    public class MSGameEntity : MSEntity
    {
        #region Constructor

        public MSGameEntity(List<GameEntity> entities)
            : base(entities)
        {
            Refresh();
        }

        #endregion
    }
}
