// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

namespace PrimalEditor.Utilities
{
    public static class ID
    {
        #region Constants

        public const int INVALID_ID = -1;

        #endregion

        #region Functions

        public static bool IsValid(int id)
        {
            return id != INVALID_ID;
        }

        #endregion
    }
}
