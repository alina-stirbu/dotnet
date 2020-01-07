namespace InterfaceSegregationIdentityAfter.Contracts
{
    using System.Collections.Generic;
    public interface IUserActions: IAccount
    {
        IEnumerable<IUser> GetAllUsersOnline();

        IEnumerable<IUser> GetAllUsers();

        IUser GetUserByName(string name);
    }
}
