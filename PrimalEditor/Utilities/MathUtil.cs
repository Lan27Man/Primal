// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;

namespace PrimalEditor.Utilities
{
    public static class MathUtil
    {
        #region Constants

        public const float Epsilon = 0.00001f;

        #endregion

        #region Extension Functions

        public static bool IsTheSameAs(this float value, float other)
        {
            return Math.Abs(value - other) < Epsilon;
        }

        public static bool IsTheSameAs(this float? value, float? other)
        {
            if (!value.HasValue || !other.HasValue)
            {
                return false;
            }

            return Math.Abs(value.Value - other.Value) < Epsilon;
        }

        #endregion
    }
}
