namespace Shared.Comands
{
    public interface IComandResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }
}
