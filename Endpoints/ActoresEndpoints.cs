using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MinimalAPIPeliculas.DTOs;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Repositorios;
using MinimalAPIPeliculas.Servicios;

namespace MinimalAPIPeliculas.Endpoints
{
    public static class ActoresEndpoints
    {
        private static readonly string contenedor = "actores";
        public static RouteGroupBuilder MapActores(this RouteGroupBuilder group)
        {
            group.MapPost("/", CrearActor).DisableAntiforgery();
            return group;
        }

        static async Task<Created<ActorDTOlectura>> CrearActor([FromForm] CrearActorDTO crearActorDTO,
            IRepositorioActores repositorioActor, IOutputCacheStore outputCacheStore, IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {

            var actor = mapper.Map<Actor>(crearActorDTO);

            if (crearActorDTO is not null)
            {
                var url = await almacenadorArchivos.Almacenar(contenedor, crearActorDTO.Foto);
                actor.Foto = url;

            }

            var id = await repositorioActor.CrearActor(actor);
            await outputCacheStore.EvictByTagAsync("actores-get", default);
            var actorDTOlectura = mapper.Map<ActorDTOlectura>(actor);
            return TypedResults.Created($"/actores/{id}", actorDTOlectura);
        }
    }
}
