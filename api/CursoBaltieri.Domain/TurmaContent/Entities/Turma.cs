using Domain.AnoContent.Entities;
using Domain.DepartamentoContent.Entities;
using Domain.FuncionarioContent.Entities;
using Domain.SerieContent.Entities;
using Domain.UserContent.Entities;
using Shared.Entities;
using System;

namespace Domain.TurmaContent.Entities
{
    public class Turma : Entity
    {
        

        protected Turma()
        {

        }

        public Turma(string escolaId,string ensino, string usuarioId, string serieId, string departamentoId, string anoId, string periodo, string coordenador, string diretor, string funcionarioId, int qtdAulas1Bimestre, int qtdAulas2Bimestre, int qtdAulas3Bimestre, int qtdAulas4Bimestre)
        {
            EscolaId = Guid.Parse(escolaId);
            UsuarioId = Guid.Parse(usuarioId);
            Ensino = ensino;
            SerieId = Guid.Parse(serieId);
            DepartamentoId = Guid.Parse(departamentoId);
            AnoId = Guid.Parse(anoId);
            Periodo = periodo;
            Coordenador = coordenador;
            Diretor = diretor;
            FuncionarioId = Guid.Parse(funcionarioId);
            QtdAulas1Bimestre = qtdAulas1Bimestre;
            QtdAulas2Bimestre = qtdAulas2Bimestre;
            QtdAulas3Bimestre = qtdAulas3Bimestre;
            QtdAulas4Bimestre = qtdAulas4Bimestre;
            Status = true;
        }

        public void Alterar(string ensino, string serieId, string departamentoId, string anoId, string periodo, string coordenador, string diretor, string funcionarioId, int qtdAulas1Bimestre, int qtdAulas2Bimestre, int qtdAulas3Bimestre, int qtdAulas4Bimestre)
        {
            
            Ensino = ensino;
            SerieId = Guid.Parse(serieId);
            DepartamentoId = Guid.Parse(departamentoId);
            AnoId = Guid.Parse(anoId);
            Periodo = periodo;
            Coordenador = coordenador;
            Diretor = diretor;
            FuncionarioId = Guid.Parse(funcionarioId);
            QtdAulas1Bimestre = qtdAulas1Bimestre;
            QtdAulas2Bimestre = qtdAulas2Bimestre;
            QtdAulas3Bimestre = qtdAulas3Bimestre;
            QtdAulas4Bimestre = qtdAulas4Bimestre;
            
        }

        public void AtivarDesativar()
        {
            Status = !Status;
        }


        public string Ensino {get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid SerieId { get; private set; }
        public Guid DepartamentoId { get; private set; }
        public Guid AnoId { get; private set; }
        public Guid EscolaId { get; private set; }
        public string Periodo { get; private set; }
        public string Coordenador { get; private set; }
        public string Diretor { get; private set; }
        public Guid FuncionarioId { get; private set; }
        public int QtdAulas1Bimestre { get; private set; }
        public int QtdAulas2Bimestre { get; private set; }
        public int QtdAulas3Bimestre { get; private set; }
        public int QtdAulas4Bimestre { get; private set; }
        public Boolean Status { get; private set; }


        public Usuario Usuario { get; private set; }
        public Serie Serie { get; private set; }
        public Funcionario Funcionario { get; private set; }
        public Departamento Departamento { get; private set; }
        public Ano Ano { get; private set; }

        

    }
}
