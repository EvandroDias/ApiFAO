using Domain.HorarioRotinaContent.Commands.Outputs;
using Domain.HorarioRotinaContent.Entities;
using Domain.HorarioRotinaContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class HorarioRotinaRepositorio : IHorarioRotinaRepositorio
    {
        private readonly DataContext _context;

        public HorarioRotinaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public HorarioRotina Salvar(HorarioRotina obj)
        {
            var retorno = _context.HorarioRotinas.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(HorarioRotina obj)
        {
            _context.Entry<HorarioRotina>(obj).State = EntityState.Modified;
        }

        public HorarioRotina Existe(Guid id)
        {
            return _context.HorarioRotinas.Where(s => s.Id == id).FirstOrDefault();
        }


        public HorarioRotina Existe(string aula,Guid rotinaId,Guid diaSemanaId)
        {
            return _context.HorarioRotinas.Where(s => s.Aula == aula && s.RotinaId == rotinaId && s.DiaSemanaId == diaSemanaId).FirstOrDefault();
        }

        public DetalheHorarioRotinaResults Detalhes(Guid horarioRotinaId,Guid usuarioId)
        {
            var retorno = (from s in _context.HorarioRotinas
                           join r in _context.Rotinas on s.RotinaId equals r.Id
                           join f in _context.Funcionarios on r.FuncionarioId equals f.Id
                           join u in _context.Usuarios on f.UsuarioId equals u.Id
                           where s.Id == horarioRotinaId && r.Status == true && u.Id == usuarioId
                           select new DetalheHorarioRotinaResults()
                           {
                              HorarioRotinaId = s.Id,
                              Aula = s.Aula,
                              Conteudo = s.Conteudo,
                              DataCadastro = s.DataCadastro,
                              DiaSemanaId = s.DiaSemanaId,
                              RotinaId = s.RotinaId,
                              Status = s.Status

                           }).FirstOrDefault();

            return retorno;
        }

        public DetalheHorarioRotinaResults Detalhes(Guid horarioRotinaId)
        {
            var retorno = (from s in _context.HorarioRotinas
                           join r in _context.Rotinas on s.RotinaId equals r.Id
                           join f in _context.Funcionarios on r.FuncionarioId equals f.Id
                           where s.Id == horarioRotinaId && r.Status == true
                           select new DetalheHorarioRotinaResults()
                           {
                               HorarioRotinaId = s.Id,
                               Aula = s.Aula,
                               Conteudo = s.Conteudo,
                               DataCadastro = s.DataCadastro,
                               DiaSemanaId = s.DiaSemanaId,
                               RotinaId = s.RotinaId,
                               Status = s.Status

                           }).FirstOrDefault();

            return retorno;
        }

        public IList<ListarHorarioRotinaResults> ListarMeusHorarios(Guid rotinaId, Guid diaSemanaId)
        {
            var retorno = (from s in _context.HorarioRotinas
                           join r in _context.Rotinas on s.RotinaId equals r.Id
                           join d in _context.DiaSemanas on s.DiaSemanaId equals d.Id
                           where s.RotinaId == rotinaId && d.Id == diaSemanaId && r.Status == true
                           select new ListarHorarioRotinaResults()
                           {
                               HorarioRotinaId = s.Id,
                               Conteudo = s.Conteudo,
                               DataCadastro = s.DataCadastro,
                               DiaSemanaId = s.DiaSemanaId,
                               RotinaId = s.RotinaId,
                               Status = s.Status,
                               Aula = s.Aula,
                               NomeSemana = d.Nome
                           }).OrderBy(s => s.Aula).ToList();

            return retorno;
        }

        public IList<ListarHorarioRotinaResults> ListarTodos()
        {
            var retorno = (from s in _context.HorarioRotinas
                           select new ListarHorarioRotinaResults()
                           {
                               Conteudo = s.Conteudo,
                               DataCadastro = s.DataCadastro,
                               DiaSemanaId = s.DiaSemanaId,
                               RotinaId = s.RotinaId,
                               Status = s.Status
                           }).OrderByDescending(s => s.DataCadastro).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
