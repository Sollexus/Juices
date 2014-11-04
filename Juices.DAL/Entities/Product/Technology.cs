using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Juices.DAL.Entities.Product
{
	public class Technology : Entity {
		[Required, DisplayName("Название")]
		public string Name { get; set; }

		[Range(0, 100), DisplayName("Рейтинг")]
		public int Rating { get; set; }

		[Required, DisplayName("Описание")]
		public string Description { get; set; }

		public static Technology GetDefault() {
			return new Technology {Id = -1, Name = "Название", Rating = 10, Description = "Описание"};
		}
	}
}