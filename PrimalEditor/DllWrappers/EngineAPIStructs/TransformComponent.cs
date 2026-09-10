// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Numerics;
using System.Runtime.InteropServices;

namespace PrimalEditor.EngineAPIStructs
{
    [StructLayout(LayoutKind.Sequential)]
    internal class TransformComponent
    {
        #region Fields

        public Vector3 Position;

        public Vector3 Rotation;

        public Vector3 Scale = new Vector3(1, 1, 1);

        #endregion

        #region Constructor

        public TransformComponent()
        {

        }

        #endregion
    }
}
