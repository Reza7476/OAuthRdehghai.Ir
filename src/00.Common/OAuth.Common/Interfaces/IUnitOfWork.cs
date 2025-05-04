namespace OAuth.Common.Interfaces;

public interface IUnitOfWork : IScope
{
    Task Begin();

    Task Commit();

    Task Rollback();

    Task CommitPartial();

    Task Complete();

}
