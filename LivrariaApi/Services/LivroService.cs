using LivrariaApi.Data;
using LivrariaCore.Models;
using LivrariaCore.DTO_s;



namespace LivrariaApi.Services
{
    public class LivroService : ILivroService
    {

        private readonly LivrariaContext _context;

        public LivroService(LivrariaContext context)
        {
            _context = context;
        }

        public Livro? ObterLivroPeloId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ERRO: ID DO LIVRO NÃO PODE SER MENOR OU IGUAL A ZERO");
            }

            return _context.Livros.FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<LivroDigital> ObterLivrosDigitais()
        {
            return _context.Livros.OfType<LivroDigital>().ToList();
        }

        public IEnumerable<LivroFisico> ObterLivrosFisicos()
        {
            return _context.Livros.OfType<LivroFisico>().ToList();
        }

        public IEnumerable<Livro> ObterTodosLivros()
        {
            return _context.Livros.ToList();
        }

        public void AdicionarLivroDigital(LivroDigitalDTO digitalDto)
        {
            if (digitalDto == null)
            {
                throw new ArgumentNullException(nameof(digitalDto), "ERRO: LIVRO DIGITAL NÃO PODE SER NULO");
            }

            var livroDigital = new LivroDigital(digitalDto.ID, digitalDto.Nome, digitalDto.Preco, digitalDto.Autor, digitalDto.Formato, digitalDto.Quantidade);

            _context.Add(livroDigital);
            _context.SaveChanges();
        }

        public void AdicionarLivroFisico(LivroFisicoDTO fisicoDto)
        {

            if (fisicoDto == null)
            {
                throw new ArgumentNullException(nameof(fisicoDto), "ERRO: LIVRO FISICO NÃO PODE SER NULO");
            }

            var livroFisico = new LivroFisico(fisicoDto.ID, fisicoDto.Nome, fisicoDto.Preco, fisicoDto.Autor, fisicoDto.TipoCapa, fisicoDto.Quantidade);
            _context.Add(livroFisico);
            _context.SaveChanges();

        }

        public void AtualizarLivroDigital(int id, LivroDigitalDTO atualizarDigital)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                throw new ArgumentException(nameof(id), "ERRO: LIVRO COM O ID ESPECIFICADO NÃO FOI ENCONTRADO");
            }

            if (livro is LivroDigital digital)
            {
                digital.Nome = atualizarDigital.Nome;
                digital.Autor = atualizarDigital.Autor;
                digital.Preco = atualizarDigital.Preco;
                digital.Formato = atualizarDigital.Formato;
                digital.Quantidade = atualizarDigital.Quantidade;
                _context.Update(digital);
            }

            _context.SaveChanges();
        }

        public void AtualizarLivroFisico(int id, LivroFisicoDTO atualizarFisico)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.Id == id);
            if (livro == null)
            {
                throw new ArgumentException(nameof(id), "ERRO: LIVRO COM O ID ESPECIFICADO NÃO FOI ENCONTRADO");
            }

            if (livro is LivroFisico fisico)
            {
                fisico.Nome = atualizarFisico.Nome;
                fisico.Autor = atualizarFisico.Autor;
                fisico.Preco = atualizarFisico.Preco;
                fisico.TipoCapa = atualizarFisico.TipoCapa;
                fisico.Quantidade = atualizarFisico.Quantidade;
                _context.Update(fisico);
            }

            _context.SaveChanges();
        }


    

         public void ExcluirLivro(int id)
         {
            var livro = _context.Livros.FirstOrDefault(l => l.Id == id);
            if (livro == null)
            {
                throw new ArgumentException(nameof(id), "ERRO: LIVRO COM O ID ESPECIFICADO NÃO FOI ENCONTRADO");
            }
            _context.Remove(livro);
            _context.SaveChanges();
         }
    }
}

         
 