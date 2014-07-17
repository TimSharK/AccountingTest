using System;
using Accounting.Models;
using Accounting.ViewModels;

namespace Accounting.Helpers
{
    public static class ViewModelExtensions
    {
        public static ParameterViewModel ToViewModel(this Parameter parameter)
        {
            if (parameter == null)
                return null;

            var vm = new ParameterViewModel
                         {
                             Id = parameter.Id,
                             IsInSchema = parameter.IsInSchema,
                             Name = parameter.Name,
                             Type = Enum.GetName(typeof(PropertyType), parameter.Type) // here we can use some dictionary or add Display attributes to model-first (?) context
                         };

            return vm;
        }
    }
}