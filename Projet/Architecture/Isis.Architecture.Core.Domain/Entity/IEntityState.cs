namespace Isis.Architecture.Core.Domain.Entity
{
    public interface IEntityState : IEntityBase, IState
    {
        void Delete();

        void Create();
    }
}
