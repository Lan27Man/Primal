// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.Serialization;

namespace PrimalEditor.GameProject
{
    [DataContract]
    public class ProjectData
    {
        #region Properties

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string ProjectPath { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        public string FullPath
        {
            get { return $"{ProjectPath}{ProjectName}{Project.Extension}"; }
        }

        public byte[] Icon { get; set; }

        public byte[] Screenshot { get; set; }

        #endregion

        #region Constructor

        public ProjectData()
        {

        }

        #endregion
    }
}
