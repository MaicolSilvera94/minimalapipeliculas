﻿using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Repositorios
{
    public interface IRepositorioActores
    {
        Task Actualizar(Actor actor);
        Task Borrar(int id);
        Task<int> CrearActor(Actor actor);
        Task<bool> Existe(int id);
        Task<Actor?> ObtenerPorId(int id);
        Task<List<Actor>> ObtenerTodos();
    }
}