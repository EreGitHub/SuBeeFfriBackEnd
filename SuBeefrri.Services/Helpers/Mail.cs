using SuBeefrri.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SuBeefrri.Services.Helpers
{
    public class Mail : IMail
    {
        /*
        * CONFIGURACIÓN SMTP:
        ---------------------------------------------------------
        * OUTLOOK -->
        servidor SMTP: smtp-mail.outlook.com
        puerto: 587   
        ---------------------------------------------------------
        * GMAIL -->         
        servidor SMTP: smtp.gmail.com
        * puerto: 465 (SSL); 587 (TLS)
        ---------------------------------------------------------
        * YAHOO! -->
        servidor SMTP: smtp.mail.yahoo.com
        puerto: 25 ó 265
        */

        /* 
         * CARÁCTERES ESPECIALES QUE NO ADMITEN LOS NOMBRES DE ARCHIVOS:
         * / , \ , : , ? , * , ", < , > , |
         */

        public async Task Send(string message)
        {
            string Host = "smtp.office365.com";
            int Puerto = 587;
            string Remitente = "tj.ruben.lupate.c@upds.net.bo";
            string Contraseña = "u.7180687";

            string Destinatarios = "rtayler561@gmail.com";//"tj.luis.cruz.g@upds.net.bo";
            string CC = "";
            string Asunto = "Nueva Orden...!!!";
            char[] delimitador_CC = { ',' };
            var mailMessage = new MailMessage();
            mailMessage.To.Clear();
            mailMessage.From = new MailAddress(Remitente, String.Empty, System.Text.Encoding.UTF8);
            mailMessage.IsBodyHtml = true;
            string htmlCuerpo =
            "<!DOCTYPE html>" +
            "<html>" +
            "<head>" +
            "</head>" +
            "<body>" +
                    "<div style='text-align:justify;width:100%'>" +
                    "<div class='page-header' style='text-align:center'><h2>" + Asunto + "</h2></div>" +
                    "<p style='text-align:center'>" + message + "</p>" +
                    "<p style='text-align:center'><b>Carnes SuBeffri copyright©Tarija</b></p>" +
                "</div>" +
            "</body>" +
            "</html>";
            mailMessage.Body = htmlCuerpo;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Subject = Asunto;
            if (Destinatarios != "")
            {
                string[] _Destinatarios = Destinatarios.Split(delimitador_CC);
                foreach (var item in _Destinatarios)
                    mailMessage.To.Add(item.Trim());
            }
            if (CC != "")
            {
                string[] _CC = CC.Split(delimitador_CC);
                foreach (var item in _CC)
                    mailMessage.CC.Add(item.Trim());
            }
            using (var smtpClient = new SmtpClient(Host, Puerto))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(Remitente, Contraseña);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
