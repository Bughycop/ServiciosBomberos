namespace ServiciosBomberos.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IRepository
    {
        void AddTipo(Tipo Tipo);

        Tipo GetTipo(int id);

        IEnumerable<Tipo> GetTipos();

        void RemoveTipo(Tipo Tipo);

        Task<bool> SaveAllAsync();

        bool TipoExists(int id);

        void UpdateTipo(Tipo Tipo);
    }
}