using System.Data.Entity.Validation;
using System.Text;
using Juices.DAL;
using Juices.DAL.Entities.Product;


namespace JuicesMvc.Migrations {
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<JuicyContext> {
		public Configuration() {
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
			ContextKey = "JuicesMvc.Models.JuicyContext";
		}

		protected override void Seed(JuicyContext context) {
			context.Products.AddOrUpdate(
				p => p.Name,
				new Product {
					Name = "TestProduct", Contents = new[] {
						new Content { Chemical = new Chemical { Name = "Test Chemical", Rating = 66}}
					},
					Description = "Test description"
				}
			);
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//

			SaveChanges(context);
		}

		/// <summary>
		/// Wrapper for SaveChanges adding the Validation Messages to the generated exception
		/// </summary>
		/// <param name="context">The context.</param>
		private void SaveChanges(DbContext context) {
			try {
				context.SaveChanges();
			} catch (DbEntityValidationException ex) {
				var sb = new StringBuilder();

				foreach (var failure in ex.EntityValidationErrors) {
					sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
					foreach (var error in failure.ValidationErrors) {
						sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
						sb.AppendLine();
					}
				}

				throw new DbEntityValidationException(
					"Entity Validation Failed - errors follow:\n" +
					sb, ex
				); // Add the original exception as the innerException
			}
		}
	}
}
