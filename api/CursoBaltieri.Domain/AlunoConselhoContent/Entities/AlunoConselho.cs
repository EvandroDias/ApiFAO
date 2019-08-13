using Domain.AlunoContent.Entities;
using Domain.ConselhoContent.Entities;
using Domain.UserContent.Entities;
using Shared.Entities;
using Shared.Util;
using System;

namespace Domain.AlunoConselhoContent.Entities
{
    public class AlunoConselho : Entity
    {
        protected AlunoConselho()
        {

        }

        public AlunoConselho(Guid usuarioId, string descricao, Guid alunoId,Guid conselhoId)
        {
            UsuarioId = usuarioId;
            Descricao = descricao;
            AlunoId = alunoId;
            ConselhoId = conselhoId;
            DataCadastro = DataBrasilia.HorarioBrasilia();
        }

        public void AlterarAlunoConselho(string descricao, Guid alunoId,Guid conselhoId)
        {

            Descricao = descricao;
            AlunoId = alunoId;
            ConselhoId = conselhoId;

        }



        public Guid ConselhoId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string Descricao { get; private set; }
        public Guid  AlunoId{ get; private set; }
        public Aluno Aluno { get; private set; }
        public Usuario Usuario { get; private set; }
        public Conselho Conselho { get; private set; }

        public DateTime DataCadastro { get; private set; }


    }
}
