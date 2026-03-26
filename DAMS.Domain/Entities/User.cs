namespace DAMS.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

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
