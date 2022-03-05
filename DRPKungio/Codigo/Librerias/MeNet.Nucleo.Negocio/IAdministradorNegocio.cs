using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace MeNet.Nucleo.Negocio
{
    interface IAdministradorNegocioEntidad <T> where T : class, new()
    {
        T Instanciar();

        T Agregar(T elemento);

        void Actualizar(T elemento);

        void Eliminar(T elemento);

        T Obtener(Expression<Func<T, bool>> expresion);

        IEnumerable<T> Consultar(Expression<Func<T, bool>> expresion);

        List<T> ObtenerLista();




    }
}
