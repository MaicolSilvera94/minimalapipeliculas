using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MinimalAPIPeliculas.DTOs;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Repositorios;

namespace MinimalAPIPeliculas.Endpoints
{
    public static class GenerosEndpoints
    {
        public static RouteGroupBuilder MapGeneros(this RouteGroupBuilder group)
        {
            group.MapGet("/", ObtenerGenero).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("generos-get"));

            group.MapGet("/{id:int}", ObtenerGeneroPorId);

            group.MapPost("/", CrearGenero);

            group.MapPut("/{id:int}", ActualizarGenero);

            group.MapDelete("/{id:int}", EliminarGenero);
            return group;
        }

        static async Task<Ok<List<GeneroDTOlectura>>> ObtenerGenero(IRepositorioGeneros repositorioGeneros, IMapper mapper)
        {
            var generos = await repositorioGeneros.ObtenerTodos();
            var generoDTOlectura = mapper.Map<List<GeneroDTOlectura>>(generos);
            return TypedResults.Ok(generoDTOlectura);
        }

        static async Task<Results<Ok<GeneroDTOlectura>, NotFound>> ObtenerGeneroPorId(int id, 
            IRepositorioGeneros repositorioGeneros, IMapper mapper)
        {
            var genero = await repositorioGeneros.ObtenerPorId(id);

            if (genero is null)
            {
                return TypedResults.NotFound();
            }
            var generoDTOlectura = mapper.Map<GeneroDTOlectura>(genero);

            return TypedResults.Ok(generoDTOlectura);
        }

        static async Task<Created<GeneroDTOlectura>> CrearGenero(CrearGeneroDTO crearGeneroDTO, 
            IRepositorioGeneros repositorioGeneros,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {

            var genero = mapper.Map<Genero>(crearGeneroDTO);
            var id = await repositorioGeneros.CrearGenero(genero);
            await outputCacheStore.EvictByTagAsync("generos-get", default);
            var generoDTOlectura = mapper.Map<GeneroDTOlectura>(genero);
            return TypedResults.Created($"/generos/{id}", generoDTOlectura);
        }

        static async Task<Results<NoContent, NotFound>> ActualizarGenero(int id, CrearGeneroDTO crearGeneroDTO,
            IRepositorioGeneros repositorioGeneros, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repositorioGeneros.Existe(id);

            if (!existe)
            {
                return TypedResults.NotFound();
            }

            var genero = mapper.Map<Genero>(crearGeneroDTO);
            genero.Id = id;

            await repositorioGeneros.Actualizar(genero);
            await outputCacheStore.EvictByTagAsync("generos-get", default);
            return TypedResults.NoContent();

        }

        static async Task<Results<NoContent, NotFound>> EliminarGenero(int id, IRepositorioGeneros repositorioGeneros,
            IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorioGeneros.Existe(id);

            if (!existe)
            {
                return TypedResults.NotFound();
            }

            await repositorioGeneros.Borrar(id);
            await outputCacheStore.EvictByTagAsync("generos-get", default);
            return TypedResults.NoContent();
        }

    }
}
