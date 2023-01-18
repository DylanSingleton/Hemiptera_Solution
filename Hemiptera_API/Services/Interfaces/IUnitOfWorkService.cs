namespace Hemiptera_API.Services.Interfaces
{
    public interface IUnitOfWorkService : IDisposable
    {
        IProjectService Project { get;}
        void Save();
    }
}
