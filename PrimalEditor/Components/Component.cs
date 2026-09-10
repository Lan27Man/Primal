// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.Serialization;

using PrimalEditor.Common;

namespace PrimalEditor.Components
{
    [DataContract]
    public abstract class Component : ViewModelBase
    {
        #region Properties

        [DataMember]
        public GameEntity Owner { get; private set; }

        #endregion

        #region Constructor

        protected Component(GameEntity owner)
        {
            Debug.Assert(owner != null);

            Owner = owner;
        }

        #endregion
    }
}
