using Infra.DataContexts;
using Shared.Interfaces;

namespace Infra.Repositories
{
    public class UnitOfWorkRepositorio : IUnitOfWorkRepositorio
    {
        private readonly DataContext _context;

        public UnitOfWorkRepositorio(DataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
            //_context.Dispose();

        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
