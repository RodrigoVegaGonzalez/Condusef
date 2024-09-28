//using Condusef.Seguridad;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace Condusef.Models
//{
//	public class ConnectionContext : IdentityDbContext<ApplicationUser>
//	{
//		public ConnectionContext(DbContextOptions<ConnectionContext> options)
//			: base(options)
//		{
//		}

//		protected override void OnModelCreating(ModelBuilder builder)
//		{
//			base.OnModelCreating(builder);

//			// Personaliza el nombre de la tabla de usuarios
//			builder.Entity<ApplicationUser>().ToTable("usuarios");
//		}
//	}
//}
