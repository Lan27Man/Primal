// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

using PrimalEditor.Components;
using PrimalEditor.EngineAPIStructs;

namespace PrimalEditor.DllWrappers
{
    internal static class EngineAPI
    {
        #region Fields

        private const string _dllName = "EngineDLL.dll";

        #endregion

        #region External Functions

        [DllImport(_dllName)]
        private static extern int CreateGameEntity(GameEntityDescriptor desc);

        [DllImport(_dllName)]
        private static extern void RemoveGameEntity(int id);

        #endregion

        #region Functions

        public static int CreateGameEntity(GameEntity entity)
        {
            GameEntityDescriptor desc = new GameEntityDescriptor();

            // Transform
            {
                var c = entity.GetComponent<Transform>();
                desc.Transform.Position = c.Position;
                desc.Transform.Rotation = c.Rotation;
                desc.Transform.Scale = c.Scale;
            }

            return CreateGameEntity(desc);
        }

        public static void RemoveGameEntity(GameEntity entity)
        {
            RemoveGameEntity(entity.EntityId);
        }

        #endregion
    }
}
