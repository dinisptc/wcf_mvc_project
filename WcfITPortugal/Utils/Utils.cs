using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Text;

namespace MvcTIC_IT.Libraries
{
    public class settings
    {
        public string logo = "G:/logos/";
    }

    public enum AdStatus
    {
        ActivationPending = -200,
        Deleted = -100,
        Inactive = -50,
        Unspecified = 0,
        Activated = 100
    }

    public class Utils
    {

        public static bool sendSimpleEmail(string toEmail, string utilizador, string fromEmail, string subjectEmail, string bodyEmail)
        {
            try
            {
                //Nova mensagem de Email
                MailMessage mail = new MailMessage();

                //Tópico e mensagem em UTF-8 que é o encoding utilizador pela aplicação
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;

                //prioridade
                mail.Priority = MailPriority.High;

                //Destinatário
                mail.To.Add(toEmail);

                //Remetente
                mail.From = new MailAddress(fromEmail, utilizador, Encoding.UTF8);
                //Tópico
                mail.Subject = subjectEmail;
                //mensagem de email simples
                mail.IsBodyHtml = false;
                //Conteúdo
                mail.Body = bodyEmail;


                mail.Body += Environment.NewLine;
                mail.Body += Environment.NewLine;

                mail.Body += "Por favor não responda a este e-mail." + Environment.NewLine;

                mail.Body += Environment.NewLine;

                mail.Body += "Envie se necessário a sua resposta para " + fromEmail + ".";

                mail.Body += Environment.NewLine;
                mail.Body += Environment.NewLine;

                mail.Body += "Mensagem enviada por Agropt.";


                //Dados do cliente/servidor SMTP
                SmtpClient smtp = new SmtpClient();

                //Servidor SMTP do sistema
                smtp.Host = ConfigurationManager.AppSettings["SMTP"];

                if (ConfigurationManager.AppSettings["SMTP_AUTENTICATION"].Equals("true"))
                {
                    //Credenciais
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FROMEMAIL"], ConfigurationManager.AppSettings["FROMPWD"]);
                }

                //Activar SSL
                smtp.EnableSsl = ConfigurationManager.AppSettings["SMTP_SSL"].Equals("true");

                //Enviar Email
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;//pageErrors.Add(ex.Message);
            }
        }
    }
}