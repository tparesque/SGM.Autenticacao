using SGM.Autenticacao.Domain.Entities;
using SGM.Autenticacao.Repository.EntityConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SGM.Autenticacao.Repository.Context
{
    public class SGMDbContext : IdentityDbContext<Usuario>
    {       
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }

        public SGMDbContext(DbContextOptions<SGMDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        
            modelBuilder.ApplyConfiguration(new EnderecoConfig());
            modelBuilder.ApplyConfiguration(new EstadoConfig());
            modelBuilder.ApplyConfiguration(new MunicipioConfig());  
            modelBuilder.ApplyConfiguration(new SituacaoConfig());            
            modelBuilder.ApplyConfiguration(new UsuarioConfig());           

            IgnoreDefaultColumnsAspNetUsers(modelBuilder);
        }

        private static void IgnoreDefaultColumnsAspNetUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Ignore(i => i.NormalizedEmail)
                .Ignore(i => i.EmailConfirmed)
                .Ignore(i => i.PhoneNumber)
                .Ignore(i => i.PhoneNumberConfirmed)
                .Ignore(i => i.TwoFactorEnabled)
                .Ignore(i => i.LockoutEnabled)
                .Ignore(i => i.AccessFailedCount)
                .Ignore(i => i.LockoutEnd);            
        }       
    }
}
