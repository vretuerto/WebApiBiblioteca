using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBiblioteca.DTOs;
using WebApiBiblioteca.Entidades;

namespace WebApiBiblioteca.Controllers.v2
{
    [ApiController]
    [Authorize]
    [Route("api/v2/editoriales")]
    public class EditorialController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EditorialController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Editorial>> GetEditoriales()
        {
            var editoriales = await context.Editoriales.ToListAsync();
            foreach (var editorial in editoriales)
            {
                editorial.Nombre = editorial.Nombre.ToUpper();
            }

            return editoriales;
        }

    }


}

