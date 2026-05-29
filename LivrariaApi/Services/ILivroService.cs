using LivrariaCore.Models;
using LivrariaCore.DTO_s;

namespace LivrariaApi.Services
{
    public interface ILivroService
    {

        IEnumerable<Livro> ObterTodosLivros();

        Livro? ObterLivroPeloId(int id);

        IEnumerable<LivroDigital> ObterLivrosDigitais();

        IEnumerable<LivroFisico> ObterLivrosFisicos();

        void AdicionarLivroFisico(LivroFisicoDTO livroFisico);

        void AdicionarLivroDigital(LivroDigitalDTO livroDigital);

        void AtualizarLivroFisico(int id, LivroFisicoDTO atualizarFisico);

        void AtualizarLivroDigital(int id, LivroDigitalDTO atualizarDigital);

        void ExcluirLivro(int id);
    }
}
