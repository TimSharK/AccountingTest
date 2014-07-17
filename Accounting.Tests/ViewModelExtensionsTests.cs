using System;
using System.Linq;
using Accounting.Helpers;
using Accounting.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Accounting.Tests
{
    [TestClass]
    public class ViewModelExtensionsTests
    {
        [TestMethod]
        public void Parameter_FieldsCount_Test()
        {
            Assert.AreEqual(7, typeof(Parameter).GetProperties().Count());
        }

        [TestMethod]
        public void ParameterViewModel_ToViewModel_Parameter_Test()
        {
            var parameterId = 5;
            var parameterName = Guid.NewGuid().ToString();
            var isInSchema = true;
            var type = PropertyType.File;

            var parameter = new Parameter
            {
                Id = parameterId,
                IsInSchema = isInSchema,
                Name = parameterName,
                Type = type
            };

            var vm = parameter.ToViewModel();

            Assert.AreEqual(parameterId, vm.Id);
            Assert.AreEqual(isInSchema, vm.IsInSchema);
            Assert.AreEqual(type.ToString(), vm.Type);
            Assert.AreEqual(parameterName, vm.Name);
        }
    }
}
