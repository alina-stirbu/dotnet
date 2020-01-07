namespace InterfaceSegregationIdentityAfter
{
    using InterfaceSegregationIdentityAfter.Contracts;

    public class AccountContoller
    {
        private readonly IController manager;

        public AccountContoller(IController manager)
        {
            this.manager = manager;
        }

        public void ChangePassword(string oldPass, string newPass)
        {
            this.manager.ChangePassword(oldPass, newPass);
        }
    }
}
