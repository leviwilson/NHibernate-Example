namespace NH.Data
{
    public interface UnitOfWorkFactory
    {
        UnitOfWork StartUnitOfWork<T>() where T : SessionConfiguration;
    }
}