﻿using System;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Fields;
using SPMeta2.Enumerations;
using SPMeta2.Utils;

namespace SPMeta2.CSOM.ModelHandlers.Fields
{
    public class LookupFieldModelHandler : FieldModelHandler
    {
        #region properties

        public override Type TargetType
        {
            get { return typeof(LookupFieldDefinition); }
        }

        protected override Type GetTargetFieldType(FieldDefinition model)
        {
            return typeof(FieldLookup);
        }

        #endregion

        #region methods

        protected override void ProcessFieldProperties(Field field, FieldDefinition fieldModel)
        {
            // let base setting be setup
            base.ProcessFieldProperties(field, fieldModel);

        }

        protected override void ProcessSPFieldXElement(XElement fieldTemplate, FieldDefinition fieldModel)
        {
            base.ProcessSPFieldXElement(fieldTemplate, fieldModel);

            var typedFieldModel = fieldModel.WithAssertAndCast<LookupFieldDefinition>("model", value => value.RequireNotNull());

            fieldTemplate.SetAttribute(BuiltInFieldAttributes.AllowMultipleValues, typedFieldModel.AllowMultipleValues.ToString().ToUpper());

            if (typedFieldModel.LookupWebId.HasValue)
                fieldTemplate.SetAttribute(BuiltInFieldAttributes.LookupWebId, typedFieldModel.LookupWebId.Value.ToString("B"));

            if (!string.IsNullOrEmpty(typedFieldModel.LookupList))
                fieldTemplate.SetAttribute(BuiltInFieldAttributes.LookupList, typedFieldModel.LookupList);

            if (!string.IsNullOrEmpty(typedFieldModel.LookupField))
                fieldTemplate.SetAttribute(BuiltInFieldAttributes.LookupField, typedFieldModel.LookupField);
        }

        #endregion
    }
}
