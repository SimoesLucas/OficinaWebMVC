using OficinaWebMVC.Database.Entities;
using OficinaWebMVC.Enums;

namespace OficinaWebMVC.Models
{
    public class OrcamentoModel
    {
        public Veiculo Veiculo { get; set; }
        public decimal ValorTotal { get; set; }
        public Cliente Cliente { get; set; }
        public string Responsavel { get; set; }
        public string CpfResponsavel { get; set; }
        public StatusOrcamento StatusOrcamento { get;  set; }
        
    }
}
