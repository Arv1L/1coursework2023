namespace TourAgency
{
    public class Administrator : User
    {
        public Administrator(string email, string name, string password) : base(email, name, password)
        {
            Status = UserStatus.Administrator;
        }
    }
}
