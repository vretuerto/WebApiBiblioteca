using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.Entidades;
using WebApiBiblioteca.Entidades.Seed;

namespace WebApiBiblioteca
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Editorial>().HasQueryFilter(x => !x.Eliminado);

            modelBuilder.Entity<ConsultaKeyLess>().HasNoKey().
               ToSqlQuery("SELECT Editoriales.EditorialId,Editoriales.Nombre as NombreEditorial, LibroId, Libros.Nombre as NombreLibro, paginas FROM Editoriales INNER JOIN Libros ON Editoriales.EditorialId=Libros.EditorialID").ToView(null); // 4.10

            SeedData.Seed(modelBuilder);
        }

        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<ConsultaKeyLess> ConsultaKeyLess { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}
