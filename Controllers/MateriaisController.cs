using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkPaginateCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Servico;

namespace webapi.Controllers
{
    [ApiController]
    [Route("materiais/api")]
    public class MateriaisController : Controller
    {
        private readonly DbContexto _context;

        private const int QUANTIDADE_POR_PAGINA = 3;

        public MateriaisController(DbContexto context)
        {
            _context = context;
        }

        // GET: Materiais
        [HttpGet("listar-materiais")]
        public async Task<IActionResult> Index(int page = 1)
        {
            return StatusCode(200, await _context.Materiais.OrderBy(x => x.Id).PaginateAsync(page, QUANTIDADE_POR_PAGINA));
        }

        // GET: Materiais/Details/5
        [HttpGet("detalhes-material/{id:int?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materiais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return Ok(material);
        }

        // POST: Materiais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("cadastrar-material")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Material material)
        {
            _context.Add(material);
            await _context.SaveChangesAsync();
            return Ok(material);
        }

        // POST: Materiais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("atualizar-material/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Material material)
        {
            if (id != material.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(material);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(material.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return Ok(material);
        }

        // POST: Materiais/Delete/5
        [HttpDelete("remover-material/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var material = await _context.Materiais.FindAsync(id);
            
            if(material == null)
                return NotFound("material não encontrado");

            _context.Materiais.Remove(material);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool MaterialExists(int id)
        {
            return _context.Materiais.Any(e => e.Id == id);
        }
    }
}
