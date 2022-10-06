namespace Planday.Schedule
{
    public class ExternalEmployee
    {
        public ExternalEmployee(string name, string email)
        {
            Name = name;
            Email = email;
        }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

