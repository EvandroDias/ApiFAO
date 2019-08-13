using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Util;
using Domain.AlunoContent.Commands.Inputs;
using Domain.AlunoContent.Entities;
using Domain.AlunoContent.Handlers;
using Domain.AlunoContent.Repositories;
using Domain.FuncionarioContent.Commands.Inputs;
using Domain.FuncionarioContent.Handlers;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Consumes("aplication/json","aplication/json-patch+json","multipart/form-data")]
    [Route("api/ImportarExcel")]
    public class ImportarExcelController : TransacaoBase
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly AlunoHandler _alunoHandler;
        private readonly FuncionarioHandler _funcionarioHandler;

        public ImportarExcelController(IAlunoRepositorio serieRepositorio, AlunoHandler alunoHandler, FuncionarioHandler funcionarioHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._alunoRepositorio = serieRepositorio;
            this._alunoHandler = alunoHandler;
            this._funcionarioHandler = funcionarioHandler;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/aluno")]
        public IComandResult Aluno(IFormFile upload)
        {
           

          

                if (upload != null && upload.Length > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = upload.OpenReadStream();

                    // We return the interface, so that
                    IExcelDataReader reader = null;


                    if (upload.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelDataReader.ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        
                        return new ComandResult(false,"Arquivo não suportado!!",new { });
                    }

                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    // reader.IsFirstRowAsColumnNames = true;

                    //DataSet result = reader.AsDataSet();
                    reader.Close();

                    var tblAuthors = result.Tables[0];



                    foreach (DataRow drCurrent in tblAuthors.Rows)
                    {

                        var salvar = new SalvarAlunoCommands();

                        var Nome = drCurrent["Nome"].ToString();
                        salvar.Ra = drCurrent["Ra"].ToString();
                        salvar.Rm = drCurrent["Rm"].ToString();
                        salvar.Nacionalidade = drCurrent["Nacionalidade"].ToString();
                        salvar.RacaCor = drCurrent["RacaCor"].ToString();
                        salvar.Sexo = drCurrent["Sexo"].ToString();
                        salvar.Uf = drCurrent["Uf"].ToString();
                        salvar.Cidade = drCurrent["Cidade"].ToString();
                        salvar.SobreNome = SobreNome(Nome);
                        salvar.Nome = PrimeiroNome(Nome);
                        salvar.Gemeos = "Não";
                        salvar.DataNascimento = Convert.ToDateTime(drCurrent["DataNascimento"].ToString());

                         var user = Guid.Parse(this.User.Identity.Name);
                    //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
                        salvar.SetarUsuarioId(user);

                    _alunoHandler.Handle(salvar);
                        
                                         
                    }

                this.Commit(true);

                return new ComandResult(true, "Adicionado com sucesso", new { });
                }
                else
                {
                    return new ComandResult(false, "Arquivo não encontrado!!", new { });
                }
            
            
        }

        [HttpPost, DisableRequestSizeLimit]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/funcionario")]
        public IComandResult Funcionario(IFormFile upload)
        {




            if (upload != null && upload.Length > 0)
            {
                // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                // to get started. This is how we avoid dependencies on ACE or Interop:
                Stream stream = upload.OpenReadStream();

                // We return the interface, so that
                IExcelDataReader reader = null;


                if (upload.FileName.EndsWith(".xls"))
                {
                    reader = ExcelDataReader.ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (upload.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {

                    return new ComandResult(false, "Arquivo não suportado!!", new { });
                }

                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                // reader.IsFirstRowAsColumnNames = true;

                //DataSet result = reader.AsDataSet();
                reader.Close();

                var tblAuthors = result.Tables[0];



                foreach (DataRow drCurrent in tblAuthors.Rows)
                {

                    var salvar = new SalvarFuncionarioCommands();

                    var Nome = drCurrent["Nome"].ToString();
                    salvar.FuncaoId = drCurrent["FuncaoId"].ToString();
                    salvar.Cpf = drCurrent["Cpf"].ToString();
                    salvar.Email = drCurrent["Email"].ToString();
                    salvar.TipoUsuario = drCurrent["TipoUsuario"].ToString();
                    salvar.Celular = drCurrent["Celular"].ToString();
                    salvar.Nacionalidade = "brasileira";
                    salvar.Sexo = drCurrent["Sexo"].ToString();
                    salvar.Uf = drCurrent["Uf"].ToString();
                    salvar.Cidade = drCurrent["Cidade"].ToString();
                    salvar.SobreNome = SobreNome(Nome);
                    salvar.Nome = PrimeiroNome(Nome);
                   
                    //salvar.DataNascimento = Convert.ToDateTime(drCurrent["DataNascimento"].ToString());

                    var user = Guid.Parse(this.User.Identity.Name);
                    //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");
                    salvar.SetarUsuarioId(user);

                    _funcionarioHandler.Handle(salvar);


                }

                this.Commit(true);

                return new ComandResult(true, "Adicionado com sucesso", new { });
            }
            else
            {
                return new ComandResult(false, "Arquivo não encontrado!!", new { });
            }


        }

        private string PrimeiroNome(string nome)
        {
            string primeiroNome = "";
            string[] arrayNome = nome.Split(' ');
            primeiroNome = arrayNome[0];
          

            return primeiroNome;
        }

        private string SobreNome(string nome)
        {
            string primeiroNome = "";
            string nomeMeio = "";
            string sobreNome = "";
            string resutado = "";
            string[] arrayNome = nome.Split(' ');
            primeiroNome = arrayNome[0];
            sobreNome = arrayNome[arrayNome.Length - 1];
            for (int i = 1; i < arrayNome.Length - 1; i++)
            {
                nomeMeio = nomeMeio += arrayNome[i] + " ";
            }

            resutado = nomeMeio + sobreNome;

            return resutado;
        }
    }
}