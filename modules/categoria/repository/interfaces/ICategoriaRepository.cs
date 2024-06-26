﻿using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.common.repository.interfaces;

namespace api_catalogo_curso.modules.categoria.repository.interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    IEnumerable<Categoria> GetAllInclude(int skip = 0, int take = 10);
}