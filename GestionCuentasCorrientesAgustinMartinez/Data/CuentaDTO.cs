using GestionCuentasCorrientesAgustinMartinez.Models;

namespace GestionCuentasCorrientesAgustinMartinez.Data
{
    public class CuentaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public float Importe { get; set; }
        public string Descripcion { get; set; }
        public List<Cliente> Clientes { get; set; }
    }
}
