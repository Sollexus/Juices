using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace JuicesWebForms.Models {
    public class Product {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public DbSet<Content> Contents;
        public DbSet<Technology> Technologies;
    }

    public class Content {
        public Chemical Chemical { get; set; }
        public int Order { get; set; }
        public bool IsSpecified { get; set; }
        public string Description { get; set; }
    }

	[DisplayName("Вещество")]
    public class Chemical {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		
		[DisplayName("Название")]
        public string Name { get; set; }

		[Range(0, 100)]
		[DisplayName("Рейтинг")]
        public int Rating { get; set; }
    }

    public class Technology {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}