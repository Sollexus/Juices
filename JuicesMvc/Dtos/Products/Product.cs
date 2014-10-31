using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JuicesMvc.Dtos.Products
{
    public class EditProductDto
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Нужно задать название")]
        public string Name { get; set; }
        public IEnumerable<int> Contents { get; set; }
		[Required(ErrorMessage = "Нужно задать описание")]
        public string Description { get; set; }
    }
}