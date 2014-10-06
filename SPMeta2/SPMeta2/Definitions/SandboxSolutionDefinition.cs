﻿using System.Net.Mail;
using SPMeta2.Attributes;
using SPMeta2.Attributes.Regression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Definitions.Base;

namespace SPMeta2.Definitions
{
    /// <summary>
    /// Allows to define and deploy SharePoint sandbox solution.
    /// </summary>
    /// 

    [SPObjectTypeAttribute(SPObjectModelType.SSOM, "Microsoft.SharePoint.SPUserSolution", "Microsoft.SharePoint")]

    [DefaultParentHostAttribute(typeof(SiteDefinition))]
    [DefaultRootHostAttribute(typeof(SiteDefinition))]

    [Serializable]

    public class SandboxSolutionDefinition : SolutionDefinitionBase
    {
        #region constructors

        public SandboxSolutionDefinition()
        {
            Content = new byte[0];
        }

        #endregion

        #region properties

        /// <summary>
        /// Should the solution be activated.
        /// </summary>
        [ExpectValidation]
        public bool Activate { get; set; }

        #endregion
    }
}
