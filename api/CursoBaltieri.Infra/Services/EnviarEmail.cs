using Domain.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infra.Services
{
    public  class EnviarEmailServices : IEnviarEmailServices
    {
        public  bool EnviarCodigo(string remetente, List<string> destinatario, string responderPara, string copiaOculta, bool html, string assunto, string conteudo, string smtp)
        {
            var enviou = this.Enviar(remetente, destinatario, responderPara, copiaOculta, html, assunto, conteudo, smtp, 587, true);

            return enviou;
        }
        private  bool Enviar(string remetente, List<string> destinatario, string responderPara, string copiaOculta, bool html, string assunto, string conteudo, string smtp, int porta, bool ssl)
        {
            MailMessage objEmail = new MailMessage();
            string login = "evandrodiascassimiro@gmail.com";
            string senha = "@dm1n@SVR2014#";
            try
            {
                //Instancia o Objeto Email como MailMessage 


                //Atribui ao método From o valor do Remetente 
                objEmail.From = new MailAddress(remetente);

                //email para resposta(quando o destinatário receber e clicar em responder, vai para:)

                objEmail.ReplyTo = new MailAddress(responderPara);

                //destinatário(s) do email(s). Obs. pode ser mais de um, pra isso basta repetir a linha

                //abaixo com outro endereço

                foreach (var item in destinatario)
                {
                    objEmail.To.Add(item);
                }

                //se quiser enviar uma cópia oculta pra alguém, utilize a linha abaixo:

                if (copiaOculta != null)
                    objEmail.Bcc.Add(copiaOculta);

                //prioridade do email

                objEmail.Priority = MailPriority.Normal;

                //utilize true pra ativar html no conteúdo do email, ou false, para somente texto

                objEmail.IsBodyHtml = html;

                //Assunto do email

                objEmail.Subject = assunto;

                //corpo do email a ser enviado

                objEmail.Body = conteudo;

                //codificação do assunto do email para que os caracteres acentuados serem reconhecidos.

                objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");


                //codificação do corpo do emailpara que os caracteres acentuados serem reconhecidos.

                objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                //cria o objeto responsável pelo envio do email
                SmtpClient objSmtp = new SmtpClient();


                //endereço do servidor SMTP(para mais detalhes leia abaixo do código)

                objSmtp.Host = smtp;

                objSmtp.Port = porta;

                objSmtp.EnableSsl = ssl;

                //para envio de email autenticado, coloque login e senha de seu servidor de email

                //para detalhes leia abaixo do código

                objSmtp.Credentials = new NetworkCredential(login, senha);


                //envia o email

                objSmtp.Send(objEmail);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                objEmail.Dispose();
            }
        }
    }
}
