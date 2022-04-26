  using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using webapi;
using webapi.Servico;

namespace webapi_materiais.Servico
{
    public class AlunoServico
    {
        private static DbContexto _context;
        public AlunoServico(DbContexto context)
        {
            _context = context;
        }

        public static async Task<bool> ValidarUsuario(int id)
        {
            using var client = new HttpClient();
            
            client.DefaultRequestHeaders.Add("cookie", "some_cookie");

            using var response = await client.GetAsync($"{Program.AlunoApi}/detalhes-aluno/{id}");
            
            return response.IsSuccessStatusCode;
        }
    }
}