using OficinaWebMVC.Enums;

namespace OficinaWebMVC.Models
{
    public class ServicoModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public decimal Preco { get; set; }
        public TipoServico TipoServico { get; set; }
    }
}
