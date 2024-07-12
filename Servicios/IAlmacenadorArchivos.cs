namespace MinimalAPIPeliculas.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task Borrar(string? ruta, string contenedor);
        Task<string> Almacenar(string contenedor, IFormFile archivo);
        //async Task<string> Editar(string ruta, string contenedor, IFormFile archivo)
        //{
        //    await Borrar(ruta, contenedor);
        //    return await Almacenar(contenedor, archivo);
        //}
        public async Task<string> Editar(string ruta, string contenedor, IFormFile archivo)
        {
            await Borrar(ruta, contenedor); // Borra el archivo existente
            return await Almacenar(contenedor, archivo); // Almacena el nuevo archivo y devuelve la nueva URL
        }
    }
}
