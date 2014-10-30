using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JuicesMvc.Dtos.Products
{
    public class EditProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<int> Contents { get; set; }
        public string Description { get; set; }
    }
}