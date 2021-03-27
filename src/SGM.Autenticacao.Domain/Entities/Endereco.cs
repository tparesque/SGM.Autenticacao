using SGM.Autenticacao.Domain.Dto;

namespace SGM.Autenticacao.Domain.Entities
{
    public class Endereco
    {
        protected Endereco() { }
        public Endereco(EnderecoDto enderecoDto)
        {
            Logradouro = enderecoDto.Logradouro;
            CEP = enderecoDto.CEP;
            Numero = enderecoDto.Numero;
            Bairro = enderecoDto.Bairro;
            Complemento = enderecoDto.Complemento;
            MunicipioId = enderecoDto.MunicipioId;
        }

        public int EnderecoId { get; set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public int MunicipioId { get; set; }
        public virtual Municipio Municipio { get; set; }

        internal void AtualizarEndereco(EnderecoDto enderecoDto)
        {
            Logradouro = enderecoDto.Logradouro;
            CEP = enderecoDto.CEP;
            Numero = enderecoDto.Numero;
            Bairro = enderecoDto.Bairro;
            Complemento = enderecoDto.Complemento;
            MunicipioId = enderecoDto.MunicipioId;
        }
    }
}
