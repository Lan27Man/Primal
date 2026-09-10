// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PrimalEditor.GameProject
{
    [DataContract]
    public class ProjectTemplate
    {
        #region Properties

        [DataMember]
        public string ProjectType { get; set; }

        [DataMember]
        public string ProjectFile { get; set; }

        [DataMember]
        public List<string> Folders { get; set; }

        public byte[] Icon { get; set; }

        public byte[] Screenshot { get; set; }

        public string IconFilePath { get; set; }

        public string ScreenshotFilePath { get; set; }

        public string ProjectFilePath { get; set; }

        #endregion

        #region Constructor

        public ProjectTemplate()
        {

        }

        #endregion
    }
}
