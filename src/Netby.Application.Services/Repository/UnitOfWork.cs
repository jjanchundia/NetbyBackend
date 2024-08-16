using Netby.Application.Services.Interfaces;
using Netby.Domain;
using Netby.Infraestucture.Persistence;

namespace Netby.Application.Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Formulario> _formularioRepository;
        private IRepository<Campo> _campoRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Formulario> FormularioRepository
        {
            get { return _formularioRepository ??= new Repository<Formulario>(_context); }
        }

        public IRepository<Campo> CampoRepository
        {
            get { return _campoRepository ??= new Repository<Campo>(_context); }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}