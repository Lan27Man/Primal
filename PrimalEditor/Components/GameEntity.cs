// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

using PrimalEditor.Common;
using PrimalEditor.DllWrappers;
using PrimalEditor.GameProject;
using PrimalEditor.Utilities;

namespace PrimalEditor.Components
{
    [DataContract]
    [KnownType(typeof(Transform))]
    public class GameEntity : ViewModelBase
    {
        #region Fields

        private int _entityId = ID.INVALID_ID;

        private bool _isActive;

        private bool _isEnabled = true;

        private string _name;

        [DataMember(Name = nameof(Components))]
        private readonly ObservableCollection<Component> _components = new ObservableCollection<Component>();

        #endregion

        #region Properties

        public int EntityId
        {
            get { return _entityId; }
            set
            {
                if (_entityId != value)
                {
                    _entityId = value;
                    OnPropertyChanged(nameof(EntityId));
                }
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;

                    if (_isActive)
                    {
                        EntityId = EngineAPI.CreateGameEntity(this);
                        Debug.Assert(ID.IsValid(_entityId));
                    }
                    else
                    {
                        EngineAPI.RemoveGameEntity(this);
                    }

                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        [DataMember]
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        [DataMember]
        public Scene ParentScene { get; private set; }

        public ReadOnlyObservableCollection<Component> Components { get; private set; }

        #endregion

        #region Constructor

        public GameEntity(Scene scene)
        {
            Debug.Assert(scene != null);

            ParentScene = scene;
            _components.Add(new Transform(this));

            OnDeserialized(new StreamingContext());
        }

        #endregion

        #region Methods

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_components != null)
            {
                Components = new ReadOnlyObservableCollection<Component>(_components);
                OnPropertyChanged(nameof(Components));
            }
        }

        #endregion

        #region Functions

        public Component GetComponent(Type type)
        {
            return Components.FirstOrDefault(c => c.GetType() == type);
        }

        public T GetComponent<T>() where T : Component
        {
            return GetComponent(typeof(T)) as T;
        }

        #endregion
    }
}
