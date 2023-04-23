namespace OficinaWebMVC.Database.Entities;

public abstract class Veiculo:Entidade
{
    
    public string Placa { get; set; }
    public int Ano { get; set; }
    public string  CodChassi { get; set; }
   
    public Cliente Cliente { get; set; }


}
