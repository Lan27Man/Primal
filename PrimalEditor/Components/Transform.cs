// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Numerics;
using System.Runtime.Serialization;

namespace PrimalEditor.Components
{
    [DataContract]
    public class Transform : Component
    {
        #region Fields

        private Vector3 _position;

        private Vector3 _rotation;

        private Vector3 _scale;

        #endregion

        #region Properties

        [DataMember]
        public Vector3 Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged(nameof(Position));
                }
            }
        }

        [DataMember]
        public Vector3 Rotation
        {
            get { return _rotation; }
            set
            {
                if (_rotation != value)
                {
                    _rotation = value;
                    OnPropertyChanged(nameof(Rotation));
                }
            }
        }

        [DataMember]
        public Vector3 Scale
        {
            get { return _scale; }
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    OnPropertyChanged(nameof(Scale));
                }
            }
        }

        #endregion

        #region Constructor

        public Transform(GameEntity owner)
            : base(owner)
        {

        }

        #endregion
    }
}
