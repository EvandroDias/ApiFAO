namespace Shared.Comands
{
    public interface IComandHandler<T> where T : IComand
    {
        IComandResult Handle(T comand);
    }
}
