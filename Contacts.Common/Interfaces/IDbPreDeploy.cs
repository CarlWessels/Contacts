namespace Contacts.Common.Interfaces
{
    public interface IDbPreDeploy
    {
        void Execute(IDatabaseConnection connection);

    }
}
