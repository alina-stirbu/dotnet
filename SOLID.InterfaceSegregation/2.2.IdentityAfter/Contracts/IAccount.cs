namespace InterfaceSegregationIdentityAfter.Contracts
{
    public interface IAccount
    {
        bool RequireUniqueEmail { get; set; }

        int MinRequiredPasswordLength { get; set; }

        int MaxRequiredPasswordLength { get; set; }

        void ChangePassword(string oldPass, string newPass);
    }
}
