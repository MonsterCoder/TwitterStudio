namespace TwitterStudio.Domain
{
    public interface ICmdHandler
    {
        bool Send(string msg);
    }
}