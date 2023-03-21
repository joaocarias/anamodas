using Joao.Ana.Modas.Dominio.Entidades;
using Joao.Ana.Modas.Infra.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Joao.Ana.Modas.Infra.Contexts
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Produto> Produtos { get; set; }     
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set;}

        public DbSet<Cor> Cores { get; set; }
        public DbSet<Tamanho> Tamanhos { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
