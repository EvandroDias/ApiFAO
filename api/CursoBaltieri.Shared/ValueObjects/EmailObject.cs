using Flunt.Validations;

namespace Shared.ValueObjects
{
    public class EmailObjectShared : ValueObject
    {
        public EmailObjectShared(string address)
        {
            Address = address;

            this.Validation(address);
        }

        public string Address { get; private set; }

        private void Validation(string address)
        {
            AddNotifications(new Contract()
                .IsEmail(this.Address, "Email", "O E-mail é inválido")
                .IsNotNullOrEmpty(address, "Email", "O E-mail é obrigatório")
                );
        }

        public override string ToString()
        {
            return this.Address;
        }
    }
}
