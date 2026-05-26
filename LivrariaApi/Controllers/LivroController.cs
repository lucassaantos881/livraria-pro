using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LivrariaCore.Models;
using LivrariaApi.Data;
using LivrariaCore.DTO_s;

namespace LivrariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivrariaContext _context;

        //Injeção de dependência do contexto do banco de dados, onde o controlador utiliza o contexto criado pelo Program.cs e assim poder utilizar o banco.
        //O construtor recebe o contexto e armazena em um campo privado para uso nos métodos do controlador
        public LivroController(LivrariaContext context)
        {
            _context = context;
        }

        //Retorna a lista completa de livros cadastrados no navegador.
        [HttpGet]
        public ActionResult<IEnumerable<Livro>> Get()
        {
            return Ok(_context.Livros.ToList());
        }


        //Percorre a lista passando o ID do livro como parâmetro na URL, e retorna o livro correspondente ou um erro 404 caso não seja encontrado.
        [HttpGet("{id}")]
        public ActionResult<Livro> Get(int id)
        {
            //Busca o livro na lista usando o método, que retorna o primeiro livro que satisfaz a condição
            var livro = _context.Livros.FirstOrDefault(l => l.Id == id);

            if(livro == null) { 
            
                return NotFound();
            }
            
            return Ok(livro);
        }

        [HttpGet("consultadigital")]
        public ActionResult<Livro> GetLivrosDigitais()
        {
            //Percorre a lista e analisa se o tipo é digital, caso seja verdadeiro, é armazenado em uma nova lista...
            var livrosDigitais = _context.Livros.OfType<LivroDigital>();
            return Ok(livrosDigitais);
        }


        //Lógica para inserir livros do tipo físico por meio do POST, usado POSTMAN
        //Recebe um objeto do tipo LivroFisicoDTO, classe essa para transferência de dados, analisando se é nulo ou não
        //Caso não seja nulo, é criado um objeto do tipo livrofisico e armazenado no banco de dados, retornando o status 201 Created, mostrando a localização do respectivo livro
        [HttpPost("fisicos")]
        public ActionResult Post([FromBody] LivroFisicoDTO fisicoDTO)
        {
            if (fisicoDTO == null)
            {
                return BadRequest();
            }

            LivroFisico livroFisico = new(fisicoDTO.ID, fisicoDTO.Nome, fisicoDTO.Preco, fisicoDTO.Autor, fisicoDTO.TipoCapa, fisicoDTO.Quantidade);

            _context.Livros.Add(livroFisico);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = livroFisico.Id }, livroFisico);

        }


        [HttpPost("digitais")]
        public ActionResult Post([FromBody] LivroDigitalDTO digitalDto)
        {
            if (digitalDto == null)
            {
                return BadRequest();
            }

            LivroDigital livroDigital = new(digitalDto.ID, digitalDto.Nome, digitalDto.Preco, digitalDto.Autor, digitalDto.Formato, digitalDto.Quantidade);

            _context.Livros.Add(livroDigital);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = livroDigital.Id }, livroDigital);
        }

    } 
}
