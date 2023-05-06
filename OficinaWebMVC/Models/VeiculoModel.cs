using OficinaWebMVC.Database.Entities;
using OficinaWebMVC.Enums;

namespace OficinaWebMVC.Models
{
    public class VeiculoModel
    {

        public string Placa { get; set; }
        public int Ano { get; set; }
        public string CodChassi { get; set; }

        public Cliente Cliente { get; set; }

        public ModeloMoto ModeloMoto { get; set; }
        public ModeloCarro ModeloCarro { get; set; }

    }
}
