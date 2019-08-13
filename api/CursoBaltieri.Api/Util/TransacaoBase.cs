using Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Util
{
    public class TransacaoBase : Controller
    {
        private IUnitOfWorkRepositorio _unitOfWork;

        public TransacaoBase(IUnitOfWorkRepositorio unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }

        public bool Commit(bool success)
        {
            if (success)
            {
                _unitOfWork.Commit();
                return true;
            }


            return false;
        }

    }
}
