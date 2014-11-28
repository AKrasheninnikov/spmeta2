﻿using System;
using SPMeta2.Containers.Services.Base;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Base;
using SPMeta2.Enumerations;

namespace SPMeta2.Containers.DefinitionGenerators
{
    public class ContentTypeLinkDefinitionGenerator : TypedDefinitionGeneratorServiceBase<ContentTypeLinkDefinition>
    {
        public override DefinitionBase GenerateRandomDefinition(Action<DefinitionBase> action)
        {
            return WithEmptyDefinition(def =>
            {
                def.ContentTypeId = BuiltInContentTypeId.Task;
                def.ContentTypeName = "Task";
            });
        }
    }
}
