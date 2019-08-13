using System;

namespace Domain.UserContent.Queries
{
    public class UserQueryResult
    {
        public Guid Id { get; set; }
        public string Login { get;  set; }
        public string Address { get;  set; }
        public string Senha { get;  set; }
        public DateTime DateRegister { get;  set; }
        public string TypeUser { get; set; }
    }
}
