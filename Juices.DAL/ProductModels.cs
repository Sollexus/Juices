using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Juices.DAL {
    public class Product {
        [Key]
        public int Id { get; set;}
		[Required, DisplayName("Название")]
		public string Name { get; set; }
		[DisplayName("Вещества")]
        public virtual ICollection<Content> Contents { get; set; }
		[DisplayName("Технологии")]
        public ICollection<Technology> Technologies { get; set; }
		[Required, DisplayName("Описание")]
		public string Description { get; set; }

	    public static Product GetDefault() {
		    return new Product {Id = 1, Name = "Название", Description = "Описание"};
	    }
    }

	[DisplayName("Содержание")]
    public class Content {
		[Key]
		public int Id { get; set; }

		[Required]
        public Chemical Chemical { get; set; }

        public int Order { get; set; }
		
		[DisplayName("Не указан")]
        public bool IsNotSpecified { get; set; }
    }

	[DisplayName("Вещество")]
    public class Chemical {
        [Key]
        public int Id { get; set; }

		[Required, DisplayName("Название")]
        public string Name { get; set; }

		[Range(0, 100), DisplayName("Рейтинг")]
        public int Rating { get; set; }

		public static Chemical GetDefault() {
			return new Chemical {Id = -1, Name = "Название", Rating = 10};
		}
    }

    public class Technology {
		[Key]
        public int Id { get; set; }

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