namespace Outreach.Reporting.Business.Interfaces
{
    public interface IAuthProcessor
    {
        bool AuthenticateUser(int associateID, string email);
        string GetUserRoleById(int id);
        bool CheckPocById(int userId, string email);
    }
}
