using Api.Util;
using Domain.AlunoContent.Repositories;
using Domain.AlunoTurmaContent.Commands.Inputs;
using Domain.AlunoTurmaContent.Repositories;
using Domain.AnoContent.Repositories;
using Domain.DepartamentoContent.Repositories;
using Domain.DisciplinaTurmaContent.Repositories;
using Domain.EscolaContent.Repositories;
using Domain.FuncionarioContent.Handlers;
using Domain.FuncionarioContent.Repositories;
using Domain.ItemAlunoTurmaContent.Handlers;
using Domain.SerieContent.Repositories;
using Domain.TurmaContent.Commands.Inputs;
using Domain.TurmaContent.Handlers;
using Domain.TurmaContent.Repositories;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Comands;
using Shared.Interfaces;
using System;
using System.Data;
using System.IO;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Consumes("aplication/json","aplication/json-patch+json","multipart/form-data")]
    [Route("api/ImportarExcelTurma")]
    public class ImportarExcelTurmaController : TransacaoBase
    {
        private readonly IDepartamentoRepositorio _departamentoRepositorio;
        private readonly ISerieRepositorio _serieRepositorio;
        private readonly IAnoRepositorio _anoRepositorio;
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IEscolaRepositorio _escolaRepositorio;
        private readonly ITurmaRepositorio _turmaRepositorio;
        private readonly IItemAlunoTurmaRepositorio _itemAlunoTurmaRepositorio;
        private readonly IItemDisciplinaTurmaRepositorio _itemDisciplinaTurmaRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly TurmaHandler _turmaHandler;
        private readonly FuncionarioHandler _funcionarioHandler;
        private readonly ItemAlunoTurmaHandler _itemAlunoTurmaHandler;

        public ImportarExcelTurmaController(ItemAlunoTurmaHandler _itemAlunoTurmaHandler,IItemDisciplinaTurmaRepositorio itemDisciplinaTurmaRepositorio,IItemAlunoTurmaRepositorio itemAlunoTurmaRepositorio,IAlunoRepositorio alunoRepositorio,ITurmaRepositorio turmaRepositorio,IEscolaRepositorio escolaRepositorio,IFuncionarioRepositorio funcionarioRepositorio,IAnoRepositorio anoRepositorio,ISerieRepositorio serieRepositorio, IDepartamentoRepositorio departamentoRepositorio, TurmaHandler turmaHandler, IUnitOfWorkRepositorio unitOfWork) : base(unitOfWork)
        {
            this._departamentoRepositorio = departamentoRepositorio;
            this._serieRepositorio = serieRepositorio;
            this._anoRepositorio = anoRepositorio;
            this._funcionarioRepositorio = funcionarioRepositorio;
            this._escolaRepositorio = escolaRepositorio;
            this._turmaHandler = turmaHandler;
            this._turmaRepositorio = turmaRepositorio;
            this._alunoRepositorio = alunoRepositorio;
            this._itemAlunoTurmaRepositorio = itemAlunoTurmaRepositorio;
            this._itemDisciplinaTurmaRepositorio = itemDisciplinaTurmaRepositorio;
            this._itemAlunoTurmaHandler = _itemAlunoTurmaHandler;
            
        }

        [HttpPost, DisableRequestSizeLimit]
        [Authorize("Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("v1/turma")]
        public IComandResult Turma(IFormFile upload)
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
                                           

                        var salvar = new SalvarTurmaCommands();
                                    
                        salvar.Ensino = drCurrent["ENSINO"].ToString();
                        salvar.Periodo = drCurrent["PERIODO"].ToString();
                        salvar.NomeCoordenador = drCurrent["COORDENADOR"].ToString();
                        salvar.NomeDiretor = drCurrent["DIRETOR"].ToString();
                        salvar.QtdAulas1Bimestre = Convert.ToInt32(drCurrent["QTDAULAS1BIMESTRE"].ToString());
                        salvar.QtdAulas2Bimestre = Convert.ToInt32(drCurrent["QTDAULAS2BIMESTRE"].ToString());
                        salvar.QtdAulas3Bimestre = Convert.ToInt32(drCurrent["QTDAULAS3BIMESTRE"].ToString());
                        salvar.QtdAulas4Bimestre = Convert.ToInt32(drCurrent["QTDAULAS4BIMESTRE"].ToString());

                       var serieId = _serieRepositorio.Existe(drCurrent["SERIE"].ToString());
                       var anoId = _anoRepositorio.Existe(drCurrent["ANO"].ToString());
                       var funcionarioId = _funcionarioRepositorio.Existe(drCurrent["PROFESSOR"].ToString());
                       var departamentoId = _departamentoRepositorio.Existe(drCurrent["DEPARTAMENTO"].ToString());
                       var escolaId = _escolaRepositorio.Existe(drCurrent["ESCOLA"].ToString());

                    if (serieId == null)
                        {
                        return new ComandResult(false, "SerieId não encontrado!!"+ drCurrent["SERIE"].ToString(), new { });
                          }
                        if(anoId == null)
                        {
                        return new ComandResult(false, "anoId não encontrado!!"+ drCurrent["ANO"].ToString(), new { });
                        }
                        if(funcionarioId == null)
                        {
                        return new ComandResult(false, "funcionarioId não encontrado!!"+ drCurrent["PROFESSOR"].ToString(), new { });
                        }
                        if(departamentoId == null)
                        {
                        return new ComandResult(false, "departamentoId não encontrado!!"+ drCurrent["DEPARTAMENTO"].ToString(), new { });
                        }
                        if (escolaId == null)
                        {
                            return new ComandResult(false, "escolaId não encontrado!!" + drCurrent["ESCOLA"].ToString(), new { });
                        }

                        salvar.SerieId = serieId.Id.ToString();
                        salvar.AnoId = anoId.Id.ToString();
                        salvar.FuncionarioId = funcionarioId.Id.ToString();
                        salvar.EscolaId = escolaId.Id.ToString();
                        salvar.DepartamentoId = departamentoId.Id.ToString();
                        var user = Guid.Parse(this.User.Identity.Name);

                        if(user == null)
                        {
                        return new ComandResult(false, "Usuario não encontrado!!", new { });
                        }
                         salvar.SetarUsuarioId(user.ToString());
                    //var user = Guid.Parse("781503e9-272b-4251-8fdf-77aca5f2d57a");


                       _turmaHandler.Handle(salvar);
                        
                                         
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
        [Route("v1/aluno")]
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
                    var salvar = new SalvarItemAlunoTurmaCommands();


                    var alunoId = _alunoRepositorio.AlunoJaExiste(drCurrent["RM"].ToString());
                    var serieId = _serieRepositorio.Existe(drCurrent["SERIE"].ToString());
                    var numero = drCurrent["NUMERO"].ToString();

                    if (alunoId == null)
                    {
                        return new ComandResult(false, "alunoId não encontrado!!" + drCurrent["RM"].ToString(), new { });
                    }

                    if (serieId == null)
                    {
                        return new ComandResult(false, "serieId não encontrado!!" + drCurrent["SERIE"].ToString(), new { });
                    }
                    else
                    {
                        var turmaId = _turmaRepositorio.ExistePorSerie(serieId.Id);

                        if(turmaId == null)
                        {
                            return new ComandResult(false, "turmaId não encontrado!!" + drCurrent["SERIE"].ToString(), new { });
                        }

                        salvar.TurmaId = turmaId.Id.ToString();
                    }

                    if (numero == null)
                    {
                        return new ComandResult(false, "numero não encontrado!!" + drCurrent["NUMERO"].ToString(), new { });
                    }

                    salvar.AlunoId = alunoId.Id.ToString();
                    salvar.Numero = Convert.ToInt32(numero);

                    var existe = _itemAlunoTurmaRepositorio.Existe(Guid.Parse(salvar.AlunoId), Guid.Parse(salvar.TurmaId));
                                       
                    if(existe == null)
                    {
                        _itemAlunoTurmaHandler.Handle(salvar);

                        this.Commit(true);
                    }
                    
                }

                this.Dispose();

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