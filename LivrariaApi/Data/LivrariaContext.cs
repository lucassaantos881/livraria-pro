
using Microsoft.EntityFrameworkCore;
using LivrariaCore.Models;

namespace LivrariaApi.Data
{
    public class LivrariaContext : DbContext
    {

        //DbContextOptions carrega as configurações, qual banco usar, etc.
        //Essas configurações vem do Program.cs, onde configuramos o serviço do banco de dados
        public LivrariaContext(DbContextOptions<LivrariaContext> options) 
            : base(options)
        {

        }

        // DbSet<T> representa uma tabela no banco
        // Livros → tabela "Livros" com todas as colunas mapeadas automaticamente
        public DbSet<Livro> Livros { get; set; }
        public DbSet<LivroFisico> LivrosFisicos { get; set; }
        public DbSet<LivroDigital> LivrosDigitais { get; set; }
        
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura o mapeamento para a hierarquia de classes Livro, LivroFisico e LivroDigital
            modelBuilder.Entity<Livro>()
                .HasDiscriminator<string>("TipoLivro")
                .HasValue<LivroFisico>("Fisico")
                .HasValue<LivroDigital>("Digital");

            //Configura para que o enum StatusPedido seja armazenado como string no banco ao invés de valor numérico
            modelBuilder.Entity<Pedido>()
                .Property(p => p.StatusPedido)
                .HasConversion<string>();
            
        }

        
    }
}
