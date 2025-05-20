using System;
using System.Collections.Generic;

namespace MyInfoDAL.DataModel
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPrice { get; set; }
    }
}
