using Netby.Domain;

namespace Netby.Application.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Formulario> FormularioRepository { get; }

        IRepository<Campo> CampoRepository { get; }
        Task<int> SaveChangesAsync();
    }
}