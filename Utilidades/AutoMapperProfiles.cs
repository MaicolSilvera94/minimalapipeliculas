﻿using AutoMapper;
using MinimalAPIPeliculas.DTOs;
using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<CrearGeneroDTO, Genero>();
            CreateMap<Genero, GeneroDTOlectura>();

            CreateMap<CrearActorDTO, Actor>()
                .ForMember(x => x.Foto, opciones => opciones.Ignore());
            CreateMap<Actor, ActorDTOlectura>();
        }
    }
}
