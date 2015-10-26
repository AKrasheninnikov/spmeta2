﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SPMeta2.Definitions.Webparts;
using SPMeta2.Models;

namespace SPMeta2.Syntax.Default
{

    [Serializable]
    [DataContract]
    public class PageViewerWebPartModelNode : WebPartModelNode
    {

    }

    public static class PageViewerWebPartDefinitionSyntax
    {
        #region methods

        public static TModelNode AddPageViewerWebPart<TModelNode>(this TModelNode model, PageViewerWebPartDefinition definition)
            where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            return AddPageViewerWebPart(model, definition, null);
        }

        public static TModelNode AddPageViewerWebPart<TModelNode>(this TModelNode model, PageViewerWebPartDefinition definition,
            Action<PageViewerWebPartModelNode> action)
            where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            return model.AddTypedDefinitionNode(definition, action);
        }

        #endregion

        #region array overload

        public static TModelNode AddPageViewerWebParts<TModelNode>(this TModelNode model, IEnumerable<PageViewerWebPartDefinition> definitions)
           where TModelNode : ModelNode, IWebpartHostModelNode, new()
        {
            foreach (var definition in definitions)
                model.AddDefinitionNode(definition);

            return model;
        }

        #endregion


    }
}
