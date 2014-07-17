using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Accounting.Helpers;
using Accounting.Models;
using Accounting.ViewModels;

namespace Accounting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult Manage()
        {
            using (var context = new ItemsDb())
            {
                var allParameters = context.Parameters.AsEnumerable().Select(x => x.ToViewModel())
                                                      .ToList();
                var vm = new ParametersSchemaViewModel
                {
                    Parameters = allParameters
                };

                return View(vm);
            }
        }

        public ActionResult Report()
        {
            using (var context = new ItemsDb())
            {
                var schemaParameters = context.Parameters.Where(x => x.IsInSchema).Select(x => x.Id).ToArray();
                var report = context.GetAccountingReportJson(schemaParameters);
                return Content(report);
            }
        }

        public ActionResult ReportUpdate()
        {
            var modelsJson = HttpUtility.UrlDecode(Request.QueryString.ToString(), Encoding.Default);
            var editedModels = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ItemsViewModel>>(modelsJson, ItemsViewModelConverter.Instance).ToList();

            using (var context = new ItemsDb())
            {
                foreach (var editedModel in editedModels)
                {
                    var item = context.Items.Single(x => x.Id == editedModel.Id);
                    item.Number = editedModel.Number;
                    item.Name = editedModel.Name;
                    item.Price = editedModel.Price;

                    foreach (var parameter in editedModel.Parameters)
                    {
                        var parameterValue = context.ParameterValues.Single(x => x.ParameterId == parameter.Key && x.ItemId == editedModel.Id);
                        parameterValue.Value = parameter.Value;
                    }
                }

                context.SaveChanges();
            }

            return Content(modelsJson);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SetSchema(ParametersSchemaViewModel viewModel)
        {
            using (var context = new ItemsDb())
            {
                foreach (var parameter in context.Parameters.ToArray())
                {
                    var parameterViewModel = viewModel.Parameters.Single(x => x.Id == parameter.Id);
                    var needUpdate = parameter.IsInSchema != parameterViewModel.IsInSchema;

                    if (needUpdate)
                        parameter.IsInSchema = parameterViewModel.IsInSchema;
                }

                context.SaveChanges();
            }

            return RedirectToAction("Manage");
        }
    }
}
