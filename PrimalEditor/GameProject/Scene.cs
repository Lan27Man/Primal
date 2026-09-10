// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Input;

using PrimalEditor.Common;
using PrimalEditor.Components;
using PrimalEditor.Utilities;

namespace PrimalEditor.GameProject
{
    [DataContract]
    public class Scene : ViewModelBase
    {
        #region Fields

        private string _name;

        private bool _isActive;

        [DataMember(Name = nameof(GameEntities))]
        private readonly ObservableCollection<GameEntity> _gameEntities = new ObservableCollection<GameEntity>();

        #endregion

        #region Properties

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
        public Project Project { get; private set; }

        [DataMember]
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        public ReadOnlyObservableCollection<GameEntity> GameEntities { get; private set; }

        public ICommand AddGameEntityCommand { get; private set; }

        public ICommand RemoveGameEntityCommand { get; private set; }

        #endregion

        #region Constructor

        public Scene(Project project, string name)
        {
            Debug.Assert(project != null);

            Project = project;
            Name = name;

            OnDeserialized(new StreamingContext());
        }

        #endregion

        #region Methods

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_gameEntities != null)
            {
                GameEntities = new ReadOnlyObservableCollection<GameEntity>(_gameEntities);
                OnPropertyChanged(nameof(GameEntities));
            }

            foreach (var entity in _gameEntities)
            {
                entity.IsActive = IsActive;
            }

            AddGameEntityCommand = new RelayCommand<GameEntity>(x =>
            {
                AddGameEntity(x);

                var entityIndex = _gameEntities.Count - 1;

                Project.UndoRedo.Add(
                    new UndoRedoAction(
                        () => RemoveGameEntity(x),
                        () => AddGameEntity(x, entityIndex),
                        $"Add {x.Name} to {Name}"
                    )
                );
            });

            RemoveGameEntityCommand = new RelayCommand<GameEntity>(x =>
            {
                var entityIndex = _gameEntities.IndexOf(x);

                RemoveGameEntity(x);

                Project.UndoRedo.Add(
                    new UndoRedoAction(
                        () => AddGameEntity(x, entityIndex),
                        () => RemoveGameEntity(x),
                        $"Remove {x.Name}"
                    )
                );
            });
        }

        private void AddGameEntity(GameEntity entity, int index = -1)
        {
            Debug.Assert(!_gameEntities.Contains(entity));

            entity.IsActive = IsActive;

            if (index == -1)
            {
                _gameEntities.Add(entity);
            }
            else
            {
                _gameEntities.Insert(index, entity);
            }
        }

        private void RemoveGameEntity(GameEntity entity)
        {
            Debug.Assert(_gameEntities.Contains(entity));

            entity.IsActive = false;
            _gameEntities.Remove(entity);
        }

        #endregion
    }
}
