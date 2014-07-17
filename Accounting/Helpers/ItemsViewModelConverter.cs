using System;
using System.Collections.Generic;
using System.Linq;
using Accounting.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Accounting.Helpers
{
    public class ItemsViewModelConverter : JsonConverter
    {
        private static readonly object SyncLock = new object();
        private static volatile ItemsViewModelConverter _instance;
        public static ItemsViewModelConverter Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (SyncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ItemsViewModelConverter();
                    }
                }

                return _instance;
            }
        }

        private ItemsViewModelConverter()
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var vms = new List<ItemsViewModel>();
            var jObjectRoot = JObject.Load(reader);

            foreach (var jObject in jObjectRoot["models"].Children<JObject>())
            {
                var vm = new ItemsViewModel
                {
                    Id = jObject["Id"].Value<long>(),
                    Number = jObject["Number"].Value<long>(),
                    Name = jObject["Name"].Value<string>(),
                    Price = jObject["Price"].Value<decimal>()
                };

                var parameters = new Dictionary<long, string>();

                var parametersProperties = jObject.Properties().Where(x => x.Name[0] == 'i');

                foreach (var parameterProperty in parametersProperties)
                {
                    var parameterId = Convert.ToInt64(parameterProperty.Name.Substring(1));
                    var value = parameterProperty.Value.ToString();
                    parameters.Add(parameterId, value);
                }

                vm.Parameters = parameters;

                vms.Add(vm);
            }

            return vms;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(IEnumerable<ItemsViewModel>));
        }
    }
}