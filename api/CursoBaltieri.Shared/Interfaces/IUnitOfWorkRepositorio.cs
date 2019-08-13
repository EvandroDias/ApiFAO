using System;

namespace Shared.Interfaces
{
    public interface IUnitOfWorkRepositorio : IDisposable
    {

        void Commit();

    }
}
