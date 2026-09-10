// Copyright (c) Arash Khatami
// Distributed under the MIT License. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PrimalEditor.GameProject
{
    [DataContract]
    public class ProjectDataList
    {
        #region Properties

        [DataMember]
        public List<ProjectData> Projects { get; set; }

        #endregion

        #region Constructor

        public ProjectDataList()
        {

        }

        #endregion
    }
}
