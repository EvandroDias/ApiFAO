using Flunt.Notifications;
using Flunt.Validations;
using Shared.Comands;

namespace Domain.TurmaContent.Commands.Inputs
{
    public class SalvarTurmaCommands : Notifiable, IComand
    {
        public string Ensino { get;  set; }
        public string UsuarioId { get;  set; }
        public string SerieId { get;  set; }
        public string DepartamentoId { get;  set; }
        public string AnoId { get;  set; }
        public string EscolaId { get;  set; }
        public string Periodo { get;  set; }
        public string NomeCoordenador { get;  set; }
        public string NomeDiretor { get;  set; }
        public string FuncionarioId { get;  set; }
        public int QtdAulas1Bimestre { get;  set; }
        public int QtdAulas2Bimestre { get;  set; }
        public int QtdAulas3Bimestre { get;  set; }
        public int QtdAulas4Bimestre { get;  set; }
       
        public bool IsValid()
        {

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(UsuarioId, "Usuario", "O usuário é obrigatório!")
                .IsNotNullOrEmpty(Ensino, "Ensino", "O ensino é obrigatório!")
                .IsNotNullOrEmpty(SerieId, "SerieId", "A série é obrigatória!")
                .IsNotNullOrEmpty(DepartamentoId, "DepartamentoId", "A sala é obrigatória!")
                .IsNotNullOrEmpty(AnoId, "AnoId", "O ano é obrigatório!")
                .IsNotNullOrEmpty(EscolaId, "EscolaId", "A escola é obrigatória!")
                .IsNotNullOrEmpty(Periodo, "Periodo", "O período é obrigatório!")
                .IsNotNullOrEmpty(NomeCoordenador, "Coordenador", "O Coordenador(a) é obrigatório!")
                .IsNotNullOrEmpty(NomeDiretor, "Diretor", "O diretor(a) é obrigatório!")
                .IsNotNullOrEmpty(FuncionarioId, "Professor", "O professor(a) é obrigatório!")
                .IsGreaterThan(QtdAulas1Bimestre,0, "QtdAulas1Bimestre", "A QtdAulas1Bimestre é obrigatória!")
                .IsGreaterThan(QtdAulas2Bimestre, 0, "QtdAulas2Bimestre", "A QtdAulas2Bimestre é obrigatória!")
                .IsGreaterThan(QtdAulas3Bimestre, 0, "QtdAulas3Bimestre", "A QtdAulas3Bimestre é obrigatória!")
                .IsGreaterThan(QtdAulas4Bimestre, 0, "QtdAulas4Bimestre", "A QtdAulas4Bimestre é obrigatória!")
            );

            return Valid;
        }

        public void SetarUsuarioId(string id)
        {
            this.UsuarioId = id;
        }
    }
}
