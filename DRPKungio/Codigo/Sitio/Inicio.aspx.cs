using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace Sitio
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            String Servidor = "mail.kungio.mx";
            String cuenta = "contacto@kungio.mx";
            String contrasena = "123456#";
            String destinatarios = "contacto@kungio.mx";
            String correoIngresado = "";
            correoIngresado = txtCorreo.Text;

            if (correoIngresado != "")
                destinatarios = destinatarios /*+ ","+ correoIngresado*/ ;

            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            m.From = new MailAddress(cuenta);
            m.To.Add(destinatarios);

            m.Subject = "El mensaje fue enviado del portal Web : " + txtNombre.Text;
            String mensaje = "";
            mensaje = "<h1> Mensaje desde portal Web </h1>";
            mensaje += "<p> Nombre: " + txtNombre.Text + "</p>";
            mensaje += "<p> txtMensaje: " + txtMensaje.Text + "</p>";
            mensaje += "<p> Correo: " + txtCorreo.Text + "</p>";
            m.Body = mensaje;
            m.IsBodyHtml = true;
            // sc.Host = txtMailServer.Text;
            sc.Host = Servidor;

            string str1 = "gmail.com";
            string str2 = cuenta;
            if (str2.Contains(str1))
            {
                try
                {
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential(cuenta, contrasena);
                    sc.EnableSsl = true;
                    sc.Send(m);
                    //lblStatus.Text = "Se envio  el correo  exitosamente";
                    //Response.Write("Email Send successfully");
                }
                catch (Exception ex)
                {
                    //lblStatus.Text = "Intenete nuevamenete,revise las credenciales";
                    //Response.Write("<BR><BR>* Please double check the From Address and Password to confirm that both of them are correct. <br>");
                    //Response.Write("<BR><BR>If you are using gmail smtp to send email for the first time, please refer to this KB to setup your gmail account: http://www.smarterasp.net/support/kb/a1546/send-email-from-gmail-with-smtp-authentication-but-got-5_5_1-authentication-required-error.aspx?KBSearchID=137388");
                    //Response.End();
                    //throw ex;
                }
            }
            else
            {
                try
                {
                    sc.Port = 25;
                    sc.Credentials = new System.Net.NetworkCredential(cuenta, contrasena);
                    sc.EnableSsl = false;
                    sc.Send(m);

                    //lblStatus.Text = "Se envio  el correo  exitosamente";
                    //Response.Write("Email Send successfully");
                }
                catch (Exception ex)
                {
                    //lblStatus.Text = "Intenete nuevamenete,revise las credenciales";
                    //Response.Write("<BR><BR>* Please double check the From Address and Password to confirm that both of them are correct. <br>");
                    //Response.Write("<BR><BR>If you are using gmail smtp to send email for the first time, please refer to this KB to setup your gmail account: http://www.smarterasp.net/support/kb/a1546/send-email-from-gmail-with-smtp-authentication-but-got-5_5_1-authentication-required-error.aspx?KBSearchID=137388");
                    //Response.End();
                    //throw ex;
                }
            }
        }
        protected void btnCali_Click(object sender, EventArgs e)
        {
            String Servidor = "mail.kungio.mx";
            String cuenta = "contacto@kungio.mx";
            String contrasena = "123456#";
            String destinatarios = "contacto@kungio.mx";
            String correoIngresado = "";
            correoIngresado = txtCorreo.Text;

            if (correoIngresado != "")
                destinatarios = destinatarios /*+ ","+ correoIngresado*/ ;

            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            m.From = new MailAddress(cuenta);
            m.To.Add(destinatarios);
            m.Subject = "Solictud  Precalificate : " + txtPNombre.Text ;
            String mensaje = "";
            mensaje = "<h1> Precalificate </h1>";
            mensaje += "<p> Nombre: " + txtNombre.Text + "</p>";
            mensaje += "<p> Fecha nacimiento: " + txtFecNac.Text + "</p>";
            mensaje += "<p> No. Imss: " + txtImss.Text + "</p>";
            mensaje += "<p> Correo: " + txtPCorreo.Text + "</p>";
            m.Body = mensaje;
            m.IsBodyHtml = true;
            // sc.Host = txtMailServer.Text;
            sc.Host = Servidor;

            string str1 = "gmail.com";
            string str2 = cuenta;
            if (str2.Contains(str1))
            {
                try
                {
                    sc.Port = 587;
                    sc.Credentials = new System.Net.NetworkCredential(cuenta, contrasena);
                    sc.EnableSsl = true;
                    sc.Send(m);
                    //lblStatus.Text = "Se envio  el correo  exitosamente";
                    //Response.Write("Email Send successfully");
                }
                catch (Exception ex)
                {
                    //lblStatus.Text = "Intenete nuevamenete,revise las credenciales";
                    //Response.Write("<BR><BR>* Please double check the From Address and Password to confirm that both of them are correct. <br>");
                    //Response.Write("<BR><BR>If you are using gmail smtp to send email for the first time, please refer to this KB to setup your gmail account: http://www.smarterasp.net/support/kb/a1546/send-email-from-gmail-with-smtp-authentication-but-got-5_5_1-authentication-required-error.aspx?KBSearchID=137388");
                    //Response.End();
                    //throw ex;
                }
            }
            else
            {
                try
                {
                    sc.Port = 25;
                    sc.Credentials = new System.Net.NetworkCredential(cuenta, contrasena);
                    sc.EnableSsl = false;
                    sc.Send(m);

                    //lblStatus.Text = "Se envio  el correo  exitosamente";
                    //Response.Write("Email Send successfully");
                }
                catch (Exception ex)
                {
                    //lblStatus.Text = "Intenete nuevamenete,revise las credenciales";
                    //Response.Write("<BR><BR>* Please double check the From Address and Password to confirm that both of them are correct. <br>");
                    //Response.Write("<BR><BR>If you are using gmail smtp to send email for the first time, please refer to this KB to setup your gmail account: http://www.smarterasp.net/support/kb/a1546/send-email-from-gmail-with-smtp-authentication-but-got-5_5_1-authentication-required-error.aspx?KBSearchID=137388");
                    //Response.End();
                    //throw ex;
                }
            }
        }
    }
}