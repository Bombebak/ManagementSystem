using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Api;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Services
{
    public class ModelStateService : IModelStateService
    {
        public List<ValidationItem> ValidateRequest(ModelStateDictionary modelState)
        {
            var validationItems = new List<ValidationItem>();
            if (modelState.IsValid)
            {
                return validationItems;
            }

            validationItems = ConvertValidationItems(modelState);

            return validationItems;
        }

        private List<ValidationItem> ConvertValidationItems(ModelStateDictionary modelState)
        {
            var validationItems = new List<ValidationItem>();
            if (!modelState.Values.Any())
            {
                return validationItems;
            }

            var modelStateItems = modelState.Keys.Where(k => modelState[k].Errors.Count > 0).Select(k => new { PropertyName = k, ErrorMessage = modelState[k].Errors[0].ErrorMessage });

            validationItems.AddRange(modelStateItems.Select(e => new ValidationItem()
            {
                FieldName = e.PropertyName,
                Message = e.ErrorMessage,
                ValidationType = Models.Enums.ValidationTypes.Error
            }));

            return validationItems;
        }
    }
}
