using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Juices.DAL.Entities.Product
{
	[DisplayName("Содержание")]
	public class Content : Entity {
		[Required]
		public virtual Chemical Chemical { get; set; }

		[Required]
		public virtual Product Product { get; set; }
//TODO: there is a way to deal with circular referenced in JSON.NET

		public int Order { get; set; }

		[DisplayName("Не указан")]
		public bool IsNotSpecified { get; set; }
	}
}