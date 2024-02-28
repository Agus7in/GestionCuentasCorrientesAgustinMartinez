namespace GestionCuentasCorrientesAgustinMartinez.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public float Saldo { get; set; }
        public int Estado { get; set; }

        public string EstadoDescripcion
        {
            get
            {
                return Estado == 1 ? "Activo" : "Dado de Baja";
            }
        }
        
        public string NombreCompleto
        {
            get
            {
                return this.Nombre + " " + this.Apellido;
            }
        }
    }
}
