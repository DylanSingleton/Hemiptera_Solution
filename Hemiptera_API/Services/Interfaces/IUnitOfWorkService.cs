namespace Hemiptera_API.Services.Interfaces
{
    public interface IUnitOfWorkService : IDisposable
    {
        IProjectService ProjectService { get;}
        void Save();
    }
}
