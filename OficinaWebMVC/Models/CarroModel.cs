using OficinaWebMVC.Database.Entities;
using OficinaWebMVC.Enums;

namespace OficinaWebMVC.Models
{
    public class CarroModel
    {
      
        public string Placa { get; set; }
        public int Ano { get; set; }
        public string CodChassi { get; set; }

        public Guid IdCliente { get; set; }
     

          public ModeloCarro ModeloCarro { get; set; }

    }
}
