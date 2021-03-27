using SGM.Autenticacao.Domain.Dto;
using Microsoft.AspNetCore.Identity;
using System;

namespace SGM.Autenticacao.Domain.Entities
{
    public class Usuario : IdentityUser
    {
        protected Usuario() { }
        public Usuario(UsuarioDto usuarioDto)
        {
            Nome = usuarioDto.Nome;
            UserName = usuarioDto.Email;
            Email = usuarioDto.Email;
            Celular = usuarioDto.Celular;
            DataCadastro = DateTime.Now;
            Endereco = usuarioDto.Endereco != null ? new Endereco(usuarioDto.Endereco) : null;
        }

        public override string Id { get => base.Id; set => base.Id = value; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }

        public void AtualizarUsuario(UsuarioDto usuarioDto)
        {
            Nome = usuarioDto.Nome;
            Telefone = usuarioDto.Telefone;
            Celular = usuarioDto.Celular;

            if (Endereco != null)
                Endereco.AtualizarEndereco(usuarioDto.Endereco);
            else
                Endereco = new Endereco(usuarioDto.Endereco);
        }       
    }
}
