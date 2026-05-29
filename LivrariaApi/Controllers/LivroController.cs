using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LivrariaCore.Models;
using LivrariaCore.DTO_s;
using LivrariaApi.Services;

namespace LivrariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        // O Controller não conhece mais o banco — só o Service
        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        //Retorna a lista completa de livros cadastrados no navegador.
        [HttpGet]
        public ActionResult<IEnumerable<Livro>> Get()
        {
            return Ok(_livroService.ObterTodosLivros());
        }


        //Percorre a lista passando o ID do livro como parâmetro na URL, e retorna o livro correspondente ou um erro 404 caso não seja encontrado.
        [HttpGet("{id}")]
        public ActionResult<Livro> Get(int id)
        {
            //Busca o livro na lista usando o método, que retorna o primeiro livro que satisfaz a condição
            var livro = _livroService.ObterLivroPeloId(id);

            if (livro == null) { 
            
                return NotFound();
            }
            
            return Ok(livro);
        }

        [HttpGet("consultadigital")]
        public ActionResult<Livro> GetLivrosDigitais()
        {
            //Percorre a lista e analisa se o tipo é digital, caso seja verdadeiro, é armazenado em uma nova lista...
            var livrosDigitais = _livroService.ObterLivrosDigitais();
            return Ok(livrosDigitais);
        }

        [HttpGet("consultafisicos")]
        public ActionResult<Livro> GetLivrosFisicos()
        {
            //Percorre a lista e analisa se o tipo é digital, caso seja verdadeiro, é armazenado em uma nova lista...
            var livrosFisicos = _livroService.ObterLivrosFisicos();
            return Ok(livrosFisicos);
        }


        //Lógica para inserir livros do tipo físico por meio do POST, usado POSTMAN
        //Recebe um objeto do tipo LivroFisicoDTO, classe essa para transferência de dados, analisando se é nulo ou não
        //Caso não seja nulo, é criado um objeto do tipo livrofisico e é validado se todos os dados entregues pelo DTO estão corretos, onde caso esteja, será adicionado no banco, retornando o status 201 Created, mostrando a localização do respectivo livro
        [HttpPost("fisicos")]
        public ActionResult Post([FromBody] LivroFisicoDTO fisicoDTO)
        {
            if (fisicoDTO == null)
            {
                return BadRequest();
            }

            _livroService.AdicionarLivroFisico(fisicoDTO);

            return CreatedAtAction(nameof(Get), new { id = fisicoDTO.ID }, fisicoDTO);

        }


        [HttpPost("digitais")]
        public ActionResult Post([FromBody] LivroDigitalDTO digitalDto)
        {
            if (digitalDto == null)
            {
                return BadRequest();
            }

            _livroService.AdicionarLivroDigital(digitalDto);
            
            return CreatedAtAction(nameof(Get), new { id = digitalDto.ID }, digitalDto);
        }

        [HttpPut("fisicos/{id}")]
        public ActionResult Put(int id, [FromBody] LivroFisicoDTO fisicoDto)
        {
            if (fisicoDto == null)
            {
                return BadRequest();
            }

            _livroService.AtualizarLivroFisico(id, fisicoDto);
            return NoContent();
        }


        [HttpPut ("digitais/{id}")]
        public ActionResult Put(int id, [FromBody] LivroDigitalDTO digitalDto)
        {
            if (digitalDto == null)
            {
                return BadRequest();
            }

            _livroService.AtualizarLivroDigital(id, digitalDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Livro> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            _livroService.ExcluirLivro(id);
            return NoContent();
        }

    } 
}
