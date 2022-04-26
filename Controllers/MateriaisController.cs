using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EntityFrameworkPaginateCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Servico;
using webapi_materiais.Servico;

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
                return StatusCode(404, new { Mensagem = "Material não foi encontrado!"});
            }

            var material = await _context.Materiais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return StatusCode(404, new { Mensagem = "Material não foi encontrado!"});
            }

            return StatusCode(200, material);
        }

        // POST: Materiais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("cadastrar-material")]
        public async Task<IActionResult> Create(Material material)
        {
            if(ModelState.IsValid)
            {
                if(!(await AlunoServico.ValidarUsuario(material.AlunoId)))
                    return StatusCode(400, new { Mensagem = $"Usuário de ID {material.AlunoId} não é válido ou não existe!"});
                
                _context.Add(material);
                await _context.SaveChangesAsync();
                return StatusCode(201, material);
            }

            return StatusCode(400, new { Mensagem = "O material passado é inválido!" });
        }

        // POST: Materiais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("atualizar-material/{id:int}")]
        public async Task<IActionResult> Edit(int id, Material material)
        {
            if(!(await AlunoServico.ValidarUsuario(material.AlunoId)))
                    return StatusCode(400, new { Mensagem = $"Usuário de ID {material.AlunoId} não é válido ou não existe!"});

            try
            {
                material.Id = id;
                _context.Update(material);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(material.Id))
                {
                    return StatusCode(404, new { Mensagem = "Material para atualizar não foi encontrado!"});
                }
                else
                {
                    throw;
                }
            }
            
            return StatusCode(200, material);
        }

        // POST: Materiais/Delete/5
        [HttpDelete("remover-material/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var material = await _context.Materiais.FindAsync(id);
            
            if(material == null)
                return StatusCode(404, new { Mensagem = "Material não encontrado!"});

            _context.Materiais.Remove(material);
            await _context.SaveChangesAsync();
            return StatusCode(402);
        }

        private bool MaterialExists(int id)
        {
            return _context.Materiais.Any(e => e.Id == id);
        }
    }
}
