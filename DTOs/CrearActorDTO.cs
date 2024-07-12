namespace MinimalAPIPeliculas.DTOs
{
    public class CrearActorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public IFormFile? Foto { get; set; }

    }
}
