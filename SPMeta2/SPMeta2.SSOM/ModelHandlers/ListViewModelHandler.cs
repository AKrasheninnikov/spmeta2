﻿using System;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.SharePoint;
using SPMeta2.Common;
using SPMeta2.Definitions;
using SPMeta2.Definitions.Base;
using SPMeta2.ModelHandlers;
using SPMeta2.Services;
using SPMeta2.SSOM.Extensions;
using SPMeta2.Utils;
using SPMeta2.SSOM.ModelHosts;

namespace SPMeta2.SSOM.ModelHandlers
{
    public class ListViewModelHandler : SSOMModelHandlerBase
    {
        #region methods

        public override Type TargetType
        {
            get { return typeof(ListViewDefinition); }
        }

        public override void DeployModel(object modelHost, DefinitionBase model)
        {
            var listModelHost = modelHost.WithAssertAndCast<ListModelHost>("modelHost", value => value.RequireNotNull());
            var listViewModel = model.WithAssertAndCast<ListViewDefinition>("model", value => value.RequireNotNull());

            var list = listModelHost.HostList;

            ProcessView(modelHost, list, listViewModel);
        }

        protected void ProcessView(object modelHost, SPList targetList, ListViewDefinition listViewModel)
        {
            var currentView = targetList.Views.FindByName(listViewModel.Title);

            InvokeOnModelEvent(this, new ModelEventArgs
            {
                CurrentModelNode = null,
                Model = null,
                EventType = ModelEventType.OnProvisioning,
                Object = currentView,
                ObjectType = typeof(SPView),
                ObjectDefinition = listViewModel,
                ModelHost = modelHost
            });

            if (currentView == null)
            {
                TraceService.Information((int)LogEventId.ModelProvisionProcessingNewObject, "Processing new list view");

                var viewFields = new StringCollection();
                viewFields.AddRange(listViewModel.Fields.ToArray());

                // TODO, handle personal view creation
                currentView = targetList.Views.Add(
                            string.IsNullOrEmpty(listViewModel.Url) ? listViewModel.Title : listViewModel.Url,
                            viewFields,
                            listViewModel.Query,
                            (uint)listViewModel.RowLimit,
                            listViewModel.IsPaged,
                            listViewModel.IsDefault);

                currentView.Title = listViewModel.Title;
            }
            else
            {
                TraceService.Information((int)LogEventId.ModelProvisionProcessingExistingObject, "Processing existing list view");
            }

            // viewModel.InvokeOnDeployingModelEvents<ListViewDefinition, SPView>(currentView);

            // if any fields specified, overwrite
            if (listViewModel.Fields.Any())
            {
                currentView.ViewFields.DeleteAll();

                foreach (var viewField in listViewModel.Fields)
                    currentView.ViewFields.Add(viewField);
            }

            if (!string.IsNullOrEmpty(listViewModel.JSLink))
                currentView.JSLink = listViewModel.JSLink;

            // viewModel.InvokeOnModelUpdatedEvents<ListViewDefinition, SPView>(currentView);

            InvokeOnModelEvent(this, new ModelEventArgs
            {
                CurrentModelNode = null,
                Model = null,
                EventType = ModelEventType.OnProvisioned,
                Object = currentView,
                ObjectType = typeof(SPView),
                ObjectDefinition = listViewModel,
                ModelHost = modelHost
            });

            TraceService.Verbose((int)LogEventId.ModelProvisionCoreCall, "Calling currentView.Update()");
            currentView.Update();
        }

        #endregion
    }
}
