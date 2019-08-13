using Domain.FuncionarioContent.Commands.Outputs;
using Domain.FuncionarioContent.Entities;
using Domain.FuncionarioContent.Repositories;
using Infra.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly DataContext _context;

        public FuncionarioRepositorio(DataContext context)
        {
            this._context = context;
        }
        public Funcionario Salvar(Funcionario obj)
        {
            var retorno = _context.Funcionarios.Add(obj);

            return retorno.Entity;
        }
        public Funcionario Alterar(Funcionario obj)
        {
           var retorno = _context.Entry<Funcionario>(obj).State = EntityState.Modified;

            return obj;
        }

        public Funcionario Existe(Guid id)
        {
            return _context.Funcionarios.Where(s => s.Id == id).FirstOrDefault();
        }
        public Funcionario Existe(string cpf)
        {
            return _context.Funcionarios.Where(s => s.Cpf == cpf).FirstOrDefault();
        }

        public Funcionario BuscarPorIdUsuario(Guid id)
        {
            return _context.Funcionarios.Where(s => s.UsuarioId == id).FirstOrDefault();
        }

        public IList<ListarFuncionarioCmbResults> ListarCordenador(string nome)
        {
            var retorno = (from c in _context.Funcionarios
                           join t in _context.Funcaos on c.FuncaoId equals t.Id
                           where t.Nome == nome
                           select new ListarFuncionarioCmbResults()
                           {
                               Id = c.Id,
                               Nome = c.Nome +" "+c.SobreNome,
                              
                           }
                           ).OrderBy(c => c.Nome).ToList();

            return retorno;
        }

        public DetalheFuncionarioResults Detalhes(Guid id)
        {
            var retorno = (from s in _context.Funcionarios
                           join d in _context.DadoPessoals on s.DadoPessoalId equals d.Id
                           join u in _context.Usuarios on s.UsuarioId equals u.Id
                           where s.Id == id
                           select new DetalheFuncionarioResults()
                           {
                               Nome = s.Nome,
                               SobreNome = s.SobreNome,
                               Id = s.Id,
                               Bairro = d.Bairro,
                               Cep = d.Cep,
                               Rua = d.Rua,
                               Numero = d.Numero,
                               Celular = s.Celular,
                               Uf = d.Uf,
                               Cidade = d.Cidade,
                               Complemento = d.Complemento,
                               Cpf = s.Cpf,
                               DadoPessoalId = d.Id,
                               DataNascimento = s.DataNascimento,
                               Email = s.Email,
                               Foto = s.Foto,
                               FuncaoId = s.FuncaoId,
                               Nacionalidade = s.Nacionalidade,
                               Natural = s.Natural,
                               Rg = s.Rg,
                               Sexo = s.Sexo,
                               Status = s.Status,
                               TelefoneFixo = s.TelefoneFixo,
                               TipoUsuario = u.TipoUsuario,
                               
                           }).FirstOrDefault();

            return retorno;
        }

        public IList<ListarFuncionarioResults> Pesquisar(string nome)
        {
            var retorno = (from s in _context.Funcionarios
                           join d in _context.DadoPessoals on s.DadoPessoalId equals d.Id
                           join u in _context.Usuarios on s.UsuarioId equals u.Id
                           where s.Status == true && s.Nome.Contains(nome)
                           select new ListarFuncionarioResults()
                           {
                               Nome = s.Nome,
                               SobreNome = s.SobreNome,
                               Id = s.Id,
                               Bairro = d.Bairro,
                               Cep = d.Cep,
                               Rua = d.Rua,
                               Numero = d.Numero,
                               Celular = s.Celular,
                               Uf = d.Uf,
                               Cidade = d.Cidade,
                               Complemento = d.Complemento,
                               Cpf = s.Cpf,
                               DadoPessoalId = d.Id,
                               DataNascimento = s.DataNascimento,
                               Email = s.Email,
                               Foto = s.Foto,
                               FuncaoId = s.FuncaoId,
                               Nacionalidade = s.Nacionalidade,
                               Natural = s.Natural,
                               Rg = s.Rg,
                               Sexo = s.Sexo,
                               Status = s.Status,
                               TelefoneFixo = s.TelefoneFixo,
                               TipoUsuario = u.TipoUsuario
                           }).OrderBy(s => s.Nome).ToList();

            return retorno;
        }

        public IList<ListarFuncionarioResults> ListarTodos(Boolean status,int skip,int take)
        {
            var retorno = (from s in _context.Funcionarios
                           join d in _context.DadoPessoals on s.DadoPessoalId equals d.Id
                           join u in _context.Usuarios on s.UsuarioId equals u.Id
                           where s.Status == status
                           select new ListarFuncionarioResults()
                           {
                               Nome = s.Nome,
                               SobreNome = s.SobreNome,
                               Id = s.Id,
                               Bairro = d.Bairro,
                               Cep = d.Cep,
                               Rua = d.Rua,
                               Numero = d.Numero,
                               Celular = s.Celular,
                               Uf = d.Uf,
                               Cidade = d.Cidade,
                               Complemento = d.Complemento,
                               Cpf = s.Cpf,
                               DadoPessoalId = d.Id,
                               DataNascimento = s.DataNascimento,
                               Email = s.Email,
                               Foto = s.Foto,
                               FuncaoId = s.FuncaoId,
                               Nacionalidade = s.Nacionalidade,
                               Natural = s.Natural,
                               Rg = s.Rg,
                               Sexo = s.Sexo,
                               Status = s.Status,
                               TelefoneFixo = s.TelefoneFixo,
                               TipoUsuario = u.TipoUsuario
                           }).OrderBy(s => s.Nome).Skip(skip).Take(take).ToList();

            return retorno;
        }

        public IList<ListarFuncionarioCmbResults> ListarCmb()
        {
            var retorno = (from s in _context.Funcionarios
                           where s.Status == true
                           select new ListarFuncionarioCmbResults()
                           {
                               Nome = s.Nome,
                               SobreNome = s.SobreNome,
                               Id = s.Id,
                              
                           }).OrderByDescending(s => s.Nome).ToList();

            return retorno;
        }

       

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
