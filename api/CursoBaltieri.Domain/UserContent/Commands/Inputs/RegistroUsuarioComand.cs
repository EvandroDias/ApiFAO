using Shared.Comands;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.UserContent.Commands.Inputs
{
    public class RegistroUsuarioComand : Notifiable, IComand
    {
        public string Nome { get;  set; }
        public string SobreNome { get; set; }
        public string Email { get;  set; }
        public string Senha { get;  set; }
       

        public bool IsValid()
        {
            
            AddNotifications(new Contract()
                //.IsNotNull(this,"","Preencha todos os campos obrigatórios")
                .IsEmail(Email,"Email","O E-mail é inválido!")
                .IsEmailOrEmpty(Email,"Email","O campo E-mail é obrigatório!")
                .IsNotNullOrEmpty(Nome, "Nome", "O campo nome é obrigatório!")
                .IsNotNullOrEmpty(SobreNome, "SobreNome", "O campo sobreNome é obrigatório!")
                .IsNotNullOrEmpty(Senha, "Senha", "O campo senha é obrigatório!")
                .HasMinLen(Senha, 4, "Senha", "A  senha deve conter no minimo 4 caracter!")
                .HasMinLen(Nome,3,"Nome","O nome deve conter no minimo 3 caracter!")
                .HasMaxLen(Nome, 200, "Nome", "O nome deve conter no máximo 200 caracter!")
                .HasMinLen(SobreNome, 3, "SobreNome", "O SobreNome deve conter no minimo 3 caracter!")
                .HasMaxLen(SobreNome, 200, "SobreNome", "O SobreNome deve conter no máximo 200 caracter!")
            );

            return Valid;
        }
    }
}
