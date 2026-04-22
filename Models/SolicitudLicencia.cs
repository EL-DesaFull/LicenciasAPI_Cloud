namespace LicenciasAPI.Models
{
    public class SolicitudLicencia
    {
        public int Id { get; set; } // PK para SQL SERVER
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public int Edad { get; set; }
        public int Grado { get; set; }
        public double NotaTeorica { get; set; }
        public double NotaPractica { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Aprobado { get; set; }
    }
}
