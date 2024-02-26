namespace GestionCuentasCorrientesAgustinMartinez.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public float Importe { get; set; }
        public string Descripcion { get; set; }
        public int IdCliente { get; set; }
    }
}
