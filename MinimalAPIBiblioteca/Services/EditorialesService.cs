using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPIBiblioteca.Entidades;

namespace MinimalAPIBiblioteca.Services
{
 
    public class EditorialesService
    {
        private readonly BibliotecaContext context;

        public EditorialesService(BibliotecaContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<IEnumerable<Editoriale>>> GetEditoriales()
        {
            var familias = await context.Editoriales.ToListAsync();
            return familias;
        }

        public async Task<ActionResult<Editoriale>> GetEditoriales(int id)
        {
            var editorial = await context.Editoriales.FindAsync(id);
            return editorial;
        }

        public async Task<ActionResult<Editoriale>> PostEditoriales(string nombre)
        {
            var editorial = new Editoriale
            {
                Nombre = nombre
            };
            await context.Editoriales.AddAsync(editorial);
            await context.SaveChangesAsync();
            return editorial;
        }

        public async Task<ActionResult<Editoriale>> PutEditoriales(int id, string nombre)
        {
            var editorial = await context.Editoriales.FindAsync(id);
            editorial.Nombre = nombre;
            await context.SaveChangesAsync();
            return editorial;
        }

        public async Task<ActionResult<Editoriale>> DeleteEditoriales(int id)
        {
            var editorial = await context.Editoriales.FindAsync(id);
            context.Remove(editorial);
            await context.SaveChangesAsync();
            return editorial;
        }
    }
}
