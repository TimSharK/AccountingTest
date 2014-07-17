using System.Collections.Generic;

namespace Accounting.ViewModels
{
    public class ItemsViewModel
    {
        public long Id { get; set; }
        public long Number { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Dictionary<long, string> Parameters { get; set; }
    }
}