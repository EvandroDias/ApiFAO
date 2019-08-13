using Domain.UserContent.Entities;
using Domain.UserContent.Queries;
using System;
using System.Collections.Generic;

namespace Domain.UserContent.Repositories
{
    public interface IUsuarioRepositorio : IDisposable
    {
        bool ExisteCpf(string login);
        bool ExisteEmail(string email);
        Usuario Existe(Guid usuarioId);
       
        Usuario ExisteAtivoInativo(Guid usuarioId);
        Usuario VerificarCodigoRecuperarSenha(string codigo);
        Usuario BuscarPorEmail(string email);
        Usuario BuscarPorTipoUsaurio(string tipo);
        int TotalUsuario(string tipo, int dias);
        Usuario Autenticar(string email,string senha);
        Usuario Salvar(Usuario user);
        bool Excluir(Guid id);
        void Alterar(Usuario user);
        void TrocarSenha(Usuario obj);
        IEnumerable<ListarUsuarioQueryResultado> ListarTodos(int skip, int take);
        IList<ListarUsuarioQueryResultado> ListarTodos();
        //IList<ListarUsuarioQueryResultado> ListarPorFiltro(string filtro);
        ListarUsuarioQueryResultado BuscarPorId(Guid id);
       
    }
}
