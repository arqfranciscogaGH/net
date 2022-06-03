using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using Sitio.Comun.Clases;
using System.Web.UI.HtmlControls;
using Sitio.Models;

namespace Sitio
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Control contenedor = BuscadorControlesUI.ObtenerControl(this.Page, "ContenedorCarruselInicio");


            Modelo db = new Modelo();
            List<ImagenCarrusel> listaImagenes = db.ImagenCarrusel.ToList();
            foreach (ImagenCarrusel imagen in listaImagenes.Take(10))
            {
                if (imagen.nombre != null && imagen.nombre != "")
                {
                    HtmlGenericControl divItem = new HtmlGenericControl("div");
                    divItem.Attributes["class"] = "item";
                    //divItem.Attributes["style"] = "background-image: url('Carrusel/s1.jpg')";
                    divItem.Attributes["style"] = "background-image: url('Carrusel/" + imagen.nombre + "')";

                    HtmlGenericControl divTitulo = new HtmlGenericControl("div");
                    divTitulo.Attributes["class"] = "carrusel-titulo";

                    HtmlGenericControl divAnimacion = new HtmlGenericControl("div");
                    divAnimacion.Attributes["class"] = "animated bounceInDown";

                    HtmlGenericControl h2 = new HtmlGenericControl("h2");
                    h2.InnerHtml = imagen.titulo;

                    HtmlGenericControl p = new HtmlGenericControl("p");
                    p.InnerHtml = imagen.mensaje;

                    divAnimacion.Controls.Add(h2);
                    divAnimacion.Controls.Add(p);
                    divTitulo.Controls.Add(divAnimacion);
                    divItem.Controls.Add(divTitulo);

                    contenedor.Controls.Add(divItem);
                    //ContenedorCarruselInicio.Controls.Add(divItem);
                }

            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            String Servidor = "mail.kungio.mx";
            String cuenta = "contacto@kungio.mx";
            String contrasena = "123456#";
            String destinatarios = "contacto@kungio.mx";
            String correoIngresado = "";
            correoIngresado = txtCorreo.Text;

            if  (txtTelefonoMovil.Text!=null && txtTelefonoMovil.Text!="")
            {
                Modelo db = new Modelo();
                Cliente cliente = new Cliente();
                cliente.nombre = txtNombre.Text;
                cliente.correo = txtCorreo.Text;
                cliente.acciones = "Atender Mensaje";
                cliente.observaciones = txtMensaje.Text;
                cliente.telefonoMovil = txtTelefonoMovil.Text;
                db.Cliente.Add(cliente);
                db.SaveChanges();
            }

            if (correoIngresado != "")
            {

                destinatarios = destinatarios /*+ ","+ correoIngresado*/ ;

                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient();
                m.From = new MailAddress(cuenta);
                m.To.Add(destinatarios);

                m.Subject = "Atender Mensaje : " + txtNombre.Text;
                String mensaje = "";
                mensaje = "<h1> Atender Mensaje  </h1>";
                mensaje += "<p> Nombre: " + txtNombre.Text + "</p>";
                mensaje += "<p> WhatsApp: " + txtTelefonoMovilC.Text + "</p>";
                mensaje += "<p> Correo: " + txtCorreo.Text + "</p>";
                mensaje += "<p> txtMensaje: " + txtMensaje.Text + "</p>";

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
        protected void btnCali_Click(object sender, EventArgs e)
        {
            String Servidor = "mail.kungio.mx";
            String cuenta = "contacto@kungio.mx";
            String contrasena = "123456#";
            String destinatarios = "contacto@kungio.mx";
            String correoIngresado = "";
            correoIngresado = txtCorreo.Text;
            if (txtTelefonoMovil.Text != null && txtTelefonoMovil.Text != "")
            {
                Modelo db = new Modelo();
                Cliente cliente = new Cliente();
                cliente.nombre = txtNombreC.Text;
                cliente.correo = txtCorreoC.Text;
                cliente.acciones = "Precalificar";
                cliente.telefonoMovil = txtTelefonoMovilC.Text;
                cliente.referencia = txtImss.Text;
                cliente.fechaNacimiento = txtFecNac.Text;
                db.Cliente.Add(cliente);
                db.SaveChanges();
            }
            if (correoIngresado != "")
            {
                destinatarios = destinatarios /*+ ","+ correoIngresado*/ ;

                MailMessage m = new MailMessage();
                SmtpClient sc = new SmtpClient();
                m.From = new MailAddress(cuenta);
                m.To.Add(destinatarios);
                m.Subject = "Precalificar : " + txtNombreC.Text;
                String mensaje = "";
                mensaje = "<h1> Precalificar </h1>";
                mensaje += "<p> Nombre: " + txtNombreC.Text + "</p>";
                mensaje += "<p> WhatsApp: " + txtTelefonoMovilC.Text + "</p>";
                mensaje += "<p> Correo: " + txtCorreoC.Text + "</p>";
                mensaje += "<p> No. Imss: " + txtImss.Text + "</p>";
                mensaje += "<p> Fecha nacimiento: " + txtFecNac.Text + "</p>";
  
 
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
}