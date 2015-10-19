﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Attributes;
using SPMeta2.Attributes.Capabilities;
using SPMeta2.Attributes.Regression;

namespace SPMeta2.Definitions
{

    /// <summary>
    /// Allows to install SharePoint application to the target web.
    /// </summary>
    [SPObjectType(SPObjectModelType.SSOM, "Microsoft.SharePoint.SPWeb", "Microsoft.SharePoint")]
    [SPObjectType(SPObjectModelType.CSOM, "Microsoft.SharePoint.Client.Web", "Microsoft.SharePoint.Client")]

    [DefaultRootHost(typeof(SiteDefinition))]
    [DefaultParentHost(typeof(WebDefinition))]

    [Serializable]
    [DataContract]

    //[ExpectWithExtensionMethod]
    //[ExpectArrayExtensionMethod]

    [ExpectManyInstances]

    [ParentHostCapability(typeof(WebDefinition))]
    public class ClearRecycleBinDefinition : DefinitionBase
    {
        #region constructors

        public ClearRecycleBinDefinition()
        {

        }

        #endregion

        #region properties

        [DataMember]
        public bool RestoreAll { get; set; }

        [ExpectValidation]
        [DataMember]
        public bool DeleteAll { get; set; }

        [DataMember]
        public bool MoveAllToSecondStage { get; set; }

        [DataMember]
        public bool DeleteAllInSecondStage { get; set; }

        #endregion
    }
}
