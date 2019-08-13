using Domain.AlunoConselhoContent.Entities;
using Domain.AnoContent.Entities;
using Domain.BimestreContent.Entities;
using Domain.SerieContent.Entities;
using Domain.UserContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;
using System.Collections.Generic;

namespace Domain.ConselhoContent.Entities
{
    public class Conselho : Entity
    {
       

        protected Conselho()
        {

        }

        public Conselho(DateTime dataConselho, Guid usuarioId,Guid serieId,Guid bimestreId,Guid funcionarioId,string nomeCoordenador,string nomeDiretor,Guid anoId)
        {
           
            DataConselho = dataConselho;
            UsuarioId = usuarioId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
            Status = true;
            SerieId = serieId;
            BimestreId = bimestreId;
            FuncionarioId = funcionarioId;
            NomeCoordenador = nomeCoordenador;
            NomeDiretor = nomeDiretor;
            AnoId = anoId;
        }

        public void Alterar(DateTime dataConselho,Guid serieId, Guid bimestreId, Guid funcionarioId,string nomeCoordenador,string nomeDiretor,Guid anoId)
        {
            
            DataConselho = dataConselho;
            SerieId = serieId;
            FuncionarioId = funcionarioId;
            BimestreId = bimestreId;
            NomeCoordenador = nomeCoordenador;
            AnoId = anoId;
            NomeDiretor = nomeDiretor;
        }


        public void AtivarDesativar()
        {
            Status = !Status;
        }

       
       
       
        public DateTime DataConselho { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid BimestreId { get; private set; }
        public Guid SerieId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Boolean Status { get; private set; }

        public string NomeCoordenador { get; private set; }
        public string NomeDiretor { get; private set; }

        public Guid FuncionarioId { get; private set; }

        public Guid AnoId { get; private set; }


        public Usuario Usuario { get; private set; }
              
        public Serie Serie { get; private set; }

        public Bimestre Bimestre { get; private set; }

        public Ano Ano { get; private set; }

        public IEnumerable<AlunoConselho> AlunoConselho { get; private set; }

    }
}
