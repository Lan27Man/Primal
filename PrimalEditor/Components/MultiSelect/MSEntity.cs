// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

using PrimalEditor.Common;
using PrimalEditor.Utilities;

namespace PrimalEditor.Components
{
    public abstract class MSEntity : ViewModelBase
    {
        #region Fields

        private bool? _isEnabled;

        private string _name;

        private readonly ObservableCollection<IMSComponent> _components = new ObservableCollection<IMSComponent>();

        // Enables updates to selected entities.
        private bool _enableUpdates = true;

        #endregion

        #region Properties

        public bool? IsEnabled
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

        public ReadOnlyObservableCollection<IMSComponent> Components { get; }

        public List<GameEntity> SelectedEntities { get; }

        #endregion

        #region Constructor

        protected MSEntity(List<GameEntity> entities)
            : base()
        {
            Debug.Assert(entities?.Any() == true);

            Components = new ReadOnlyObservableCollection<IMSComponent>(_components);
            SelectedEntities = entities;

            PropertyChanged += (s, e) =>
            {
                if (_enableUpdates)
                {
                    UpdateGameEntities(e.PropertyName);
                }
            };
        }

        #endregion

        #region Methods

        public void Refresh()
        {
            _enableUpdates = false;
            UpdateMSGameEntity();
            _enableUpdates = true;
        }

        #endregion

        #region Functions

        protected virtual bool UpdateGameEntities(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IsEnabled):
                    SelectedEntities.ForEach(x => x.IsEnabled = IsEnabled.Value);
                    return true;
                case nameof(Name):
                    SelectedEntities.ForEach(x => x.Name = Name);
                    return true;
            }

            return false;
        }

        protected virtual bool UpdateMSGameEntity()
        {
            IsEnabled = GetMixedValue(SelectedEntities, new Func<GameEntity, bool>(x => x.IsEnabled));
            Name = GetMixedValue(SelectedEntities, new Func<GameEntity, string>(x => x.Name));

            return true;
        }

        public static float? GetMixedValue(List<GameEntity> entities, Func<GameEntity, float> getProperty)
        {
            var value = getProperty.Invoke(entities.First());

            foreach (var entity in entities.Skip(1))
            {
                if (!value.IsTheSameAs(getProperty.Invoke(entity)))
                {
                    return null;
                }
            }

            return value;
        }

        public static bool? GetMixedValue(List<GameEntity> entities, Func<GameEntity, bool> getProperty)
        {
            var value = getProperty.Invoke(entities.First());

            foreach (var entity in entities.Skip(1))
            {
                if (value != getProperty.Invoke(entity))
                {
                    return null;
                }
            }

            return value;
        }

        public static string GetMixedValue(List<GameEntity> entities, Func<GameEntity, string> getProperty)
        {
            var value = getProperty.Invoke(entities.First());

            foreach (var entity in entities.Skip(1))
            {
                if (value != getProperty.Invoke(entity))
                {
                    return null;
                }
            }

            return value;
        }

        #endregion
    }
}
