using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LivrariaCore.Models;

namespace LivrariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private static readonly List<Livro> _livros = new List<Livro>()
        {
            new LivroFisico(1, "O Senhor dos Anéis", 50.00, "J.R.R. Tolkien", "Capa Dura", 10),
            new LivroDigital(2, "Harry Potter e a Pedra Filosofal", 30.00, "J.K. Rowling", "PDF", 20),
            new LivroFisico(3, "O Código Da Vinci", 40.00, "Dan Brown", "Capa Comum", 15),
        };

        //Retorna a lista completa de livros cadastrados no navegador.
        [HttpGet]
        public ActionResult<IEnumerable<Livro>> Get()
        {
            return Ok(_livros);
        }


        //Percorre a lista passando o ID do livro como parâmetro na URL, e retorna o livro correspondente ou um erro 404 caso não seja encontrado.
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Livro>> Get(int id)
        {
            //Busca o livro na lista usando o método, que retorna o primeiro livro que satisfaz a condição
            var livro = _livros.FirstOrDefault(l => l.Id == id);

            if(livro == null) { 
            
                return NotFound();
            }
            
            return Ok(livro);
        }

        [HttpGet("digitais")]
        public ActionResult<IEnumerable<Livro>> GetLivrosDigitais()
        {
            //Percorre a lista e analisa se o tipo é digital, caso seja verdadeiro, é armazenado em uma nova lista...
            var livrosDigitais = _livros.OfType<LivroDigital>();
            return Ok(livrosDigitais);
        }


    } 
}
