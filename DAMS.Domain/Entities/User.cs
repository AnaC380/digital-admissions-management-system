namespace DAMS.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }

        protected User() { }

        public User(string name, string email, string role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Role = role;
        }
    }
}
