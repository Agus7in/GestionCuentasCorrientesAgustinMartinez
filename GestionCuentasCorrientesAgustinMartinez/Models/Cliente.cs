﻿namespace GestionCuentasCorrientesAgustinMartinez.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public float Saldo { get; set; }
        public int Estado { get; set; }
    }
}
