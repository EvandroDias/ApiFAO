using System.Collections.Generic;

namespace Domain.Services
{
    public interface IEnviarEmailServices
    {
        bool EnviarCodigo(string remetente, List<string> destinatario, string responderPara, string copiaOculta, bool html, string assunto, string conteudo, string smtp);
    }
}
