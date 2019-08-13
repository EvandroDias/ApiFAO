using Domain.HorarioRotinaContent.Commands.Outputs;
using Domain.RotinaContent.Commands.Outputs;
using Domain.RotinaContent.Entities;
using Domain.RotinaContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using Shared.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class RotinaRepositorio : IRotinaRepositorio
    {
        private readonly DataContext _context;

        public RotinaRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Rotina Salvar(Rotina obj)
        {
            var retorno = _context.Rotinas.Add(obj);

            return retorno.Entity;
        }
        public void Alterar(Rotina obj)
        {
            _context.Entry<Rotina>(obj).State = EntityState.Modified;
        }

        public Rotina Existe(Guid id)
        {
            return _context.Rotinas.Where(r => r.Id == id).FirstOrDefault();
        }

        public Rotina Existe(Guid id, bool status)
        {
            return _context.Rotinas.Where(r => r.Id == id && r.Status == status).FirstOrDefault();
        }

        public Rotina Existe(Guid id, Guid usuarioId)
        {
            var retorno = (from r in _context.Rotinas
                           join f in _context.Funcionarios on r.FuncionarioId equals f.Id
                           join u in _context.Usuarios on f.UsuarioId equals u.Id
                           where r.Id == id && u.Id == usuarioId
                           select r
                           ).FirstOrDefault();

            return retorno;
        }

        public Rotina JaTemRotina(Guid funcionarioId,DateTime de)
        {
            var dataAtual = DataBrasilia.HorarioBrasilia();

            var retorno = (from r in _context.Rotinas
                           join f in _context.Funcionarios on r.FuncionarioId equals f.Id
                           join u in _context.Usuarios on f.UsuarioId equals u.Id
                           where r.Status == true && de >= r.Ate && u.Id == funcionarioId
                           select r
                           ).FirstOrDefault();

            return retorno;


            //return _context.Rotinas.Where(r => r.Status == true && r.Ate <= dataAtual && r.FuncionarioId == funcionarioId ).FirstOrDefault();
        }

        public DetalheRotinaResults Detalhes(Guid id)
        {
            var retorno = (from r in _context.Rotinas
                               join f in _context.Funcionarios on r.FuncionarioId equals f.Id
                           where r.Id == id
                           select new DetalheRotinaResults()
                           {
                               Ate = r.Ate.ToString("dd/MM/yyyy"),
                               De = r.De.ToString("dd/MM/yyyy"),
                               DataCadastro = r.DataCadastro,
                               FuncionarioId = r.FuncionarioId,
                               ImgCabecalho = r.ImgCabecalho,
                               NomeFuncionario = f.Nome+" "+f.SobreNome,
                               RotinaId = r.Id,
                               SerieId = r.SerieId,
                               Visualizada = r.Visualizada,
                               Status = r.Status
                           }).FirstOrDefault();

            return retorno;
        }

        public ImprimirRotinaResults Imprimir(Guid id)
        {
            var imprimir = new ImprimirRotinaResults();

            var retorno = (from r in _context.Rotinas
                           join f in _context.Funcionarios on r.FuncionarioId equals f.Id
                           join s in _context.Series on r.SerieId equals s.Id
                           where r.Id == id && r.Status == true
                           select new ListarRotinaResults()
                           {
                               RotinaId = r.Id,
                               De = r.De,
                               Ate = r.Ate,
                               DataCadastro = r.DataCadastro,
                               FuncionarioId = r.FuncionarioId,
                               ImgCabecalho = r.ImgCabecalho,
                               SerieId = r.SerieId,
                               Status = r.Status,
                               Visualizada = r.Visualizada,
                               NomeFuncionario = f.Nome + " " + f.SobreNome,
                               NomeSerie = s.Nome
                           }).FirstOrDefault();

            var horario = (from s in _context.HorarioRotinas
                           join r in _context.Rotinas on s.RotinaId equals r.Id
                           join d in _context.DiaSemanas on s.DiaSemanaId equals d.Id
                           where s.RotinaId == id && r.Status == true
                           select new ListarHorarioRotinaResults()
                           {
                               Conteudo = s.Conteudo,
                               DataCadastro = s.DataCadastro,
                               DiaSemanaId = s.DiaSemanaId,
                               RotinaId = s.RotinaId,
                               Status = s.Status,
                               Aula = s.Aula,
                               NomeSemana = d.Nome,
                               Order = d.Order
                           }).OrderBy(s => s.Aula ).ToList();

            imprimir.ListarRotinaResults = retorno;
            imprimir.ListarHorarioRotinaResults = horario;

            return imprimir;
        }

        public IList<ListarRotinaResults> ListarTodos(int skip, int take)
        {
            var dataAtual = DataBrasilia.HorarioBrasilia();

            var retorno = (from r in _context.Rotinas
                           join f in _context.Funcionarios on r.FuncionarioId equals f.Id
                           join s in _context.Series on r.SerieId equals s.Id
                           where r.Status == true 
                           select new ListarRotinaResults()
                           {
                               RotinaId = r.Id,
                               De = r.De,
                               Ate = r.Ate,
                               DataCadastro = r.DataCadastro,
                               FuncionarioId = r.FuncionarioId,
                               ImgCabecalho = r.ImgCabecalho,
                               SerieId = r.SerieId,
                               Status = r.Status,
                               Visualizada = r.Visualizada,
                               NomeFuncionario = f.Nome+" "+f.SobreNome,
                               NomeSerie = s.Nome
                           }).OrderByDescending(r => r.DataCadastro).Take(take).Skip(skip).ToList();

            return retorno;
        }

        public IList<ListarRotinaResults> ListarMinhasRotinas(int skip, int take, Guid funcionarioId)
        {
            var retorno = (from r in _context.Rotinas
                           join s in _context.Series on r.SerieId equals s.Id
                           join f in _context.Funcionarios on r.FuncionarioId equals f.Id
                           join u in _context.Usuarios on f.UsuarioId equals u.Id
                           where u.Id == funcionarioId && r.Status == true
                           select new ListarRotinaResults()
                           {
                               RotinaId = r.Id,
                               De = r.De,
                               Ate = r.Ate,
                               DataCadastro = r.DataCadastro,
                               FuncionarioId = r.FuncionarioId,
                               ImgCabecalho = r.ImgCabecalho,
                               SerieId = r.SerieId,
                               Status = r.Status,
                               Visualizada = r.Visualizada,
                               NomeFuncionario = f.Nome+" "+f.SobreNome,
                               NomeSerie = s.Nome
                           }).OrderByDescending(r => r.DataCadastro).Take(take).Skip(skip).ToList();

            return retorno;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
