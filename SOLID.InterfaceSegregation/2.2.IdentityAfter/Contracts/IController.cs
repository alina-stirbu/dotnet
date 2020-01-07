namespace InterfaceSegregationIdentityAfter.Contracts
{
    using System.Collections.Generic;
    public interface IController : IAccount, IUserActions
    {
        void Register(string username, string password);

        void Login(string username, string password);
    }
}
