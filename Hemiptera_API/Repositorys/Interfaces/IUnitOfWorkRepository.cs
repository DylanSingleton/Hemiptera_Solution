namespace Hemiptera_API.Services.Interfaces
{
    public interface IUnitOfWorkRepository : IDisposable
    {
        IProjectRepository Project { get;}
        void Save();
    }
}
