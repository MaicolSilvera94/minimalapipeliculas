namespace MinimalAPIPeliculas.DTOs
{
    public class ActorDTOlectura
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; } = null!;
    }
}
