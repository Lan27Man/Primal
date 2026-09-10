// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

using PrimalEditor.Utilities;

namespace PrimalEditor.GameProject
{
    public class OpenProject
    {
        #region Fields

        private static readonly string _applicationDataPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\PrimalEditor\";

        private static readonly string _projectDataPath;

        private static readonly ObservableCollection<ProjectData> _projects = new ObservableCollection<ProjectData>();

        #endregion

        #region Properties

        public static ReadOnlyObservableCollection<ProjectData> Projects { get; }

        #endregion

        #region Constructor

        static OpenProject()
        {
            try
            {
                if (!Directory.Exists(_applicationDataPath))
                {
                    Directory.CreateDirectory(_applicationDataPath);
                }

                _projectDataPath = $@"{_applicationDataPath}ProjectData.xml";

                Projects = new ReadOnlyObservableCollection<ProjectData>(_projects);
                ReadProjectData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Logger.Log(MessageType.Error, $"Failed to read project data!");

                throw;
            }
        }

        #endregion

        #region Methods

        private static void ReadProjectData()
        {
            if (File.Exists(_projectDataPath))
            {
                var projects = Serializer.FromFile<ProjectDataList>(_projectDataPath).Projects.OrderByDescending(x => x.Date);

                _projects.Clear();

                foreach (var project in projects)
                {
                    if (File.Exists(project.FullPath))
                    {
                        project.Icon = File.ReadAllBytes($@"{project.ProjectPath}\.Primal\icon.png");
                        project.Screenshot = File.ReadAllBytes($@"{project.ProjectPath}\.Primal\screenshot.png");

                        _projects.Add(project);
                    }
                }
            }
        }

        private static void WriteProjectData()
        {
            var projects = _projects.OrderBy(x => x.Date).ToList();

            Serializer.ToFile(
                new ProjectDataList()
                {
                    Projects = projects
                },
                _projectDataPath
            );
        }

        #endregion

        #region Functions

        public static Project Open(ProjectData data)
        {
            ReadProjectData();

            var project = _projects.FirstOrDefault(x => x.FullPath == data.FullPath);

            if (project != null)
            {
                project.Date = DateTime.Now;
            }
            else
            {
                project = data;
                project.Date = DateTime.Now;

                _projects.Add(project);
            }

            WriteProjectData();

            return Project.Load(project.FullPath);
        }

        #endregion
    }
}
