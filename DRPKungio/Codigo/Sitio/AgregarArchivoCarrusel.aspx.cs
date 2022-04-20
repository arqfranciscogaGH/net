

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Sitio.Models;
using System.Data.Entity;

namespace Sitio
{
    public partial class AgregarArchivoCarrusel : System.Web.UI.Page
    {

        static private int id = 0;
        private int? idReferencia = 0;
        private int? idDocumento = 0;
        static private string nombreDocumento = "";
        static private ImagenCarrusel archivo;

        protected void Page_Load(object sender, EventArgs e)
        {
            //obtenerArgumentos();
            //mostrarDocumento();
            //archivo = instanciar();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            string strFolder="";
            string strFileName = "";
            string strFileExt = "";
            string strFilePath = "";

            string strFileNew = "";
            string strFilePathNew = "";



            strFolder = Server.MapPath("./Carrusel/");
            strFileName = System.IO.Path.GetFileName(cargaArchivo.PostedFile.FileName);
            strFileName = cargaArchivo.FileName;
            strFileExt = System.IO.Path.GetExtension(cargaArchivo.PostedFile.FileName);
            if (strFolder != "" && strFileName != "")
            {
                // Create the folder if it does not exist.
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }
                // Save the uploaded file to the server.
                strFilePath = strFolder + strFileName;
                archivo = instanciar();
                archivo.nombre = strFileName;
                archivo.ruta = strFilePath;
                archivo = guardar(archivo);

                strFileNew = archivo.idImagen.ToString() + "_" + strFileName;
                strFilePathNew = strFolder + strFileNew;
                archivo.nombre = strFileNew;
                archivo.ruta = strFilePathNew;
               

                if (File.Exists(strFilePathNew))
                {
                    lblMensaje.Text = "Ya existe  el archivo:" + strFilePathNew;
                    cargaArchivo.PostedFile.SaveAs(strFilePathNew);
                    archivo = guardar(archivo);
                    lblMensaje.Text = strFileName + " El archivo:" + strFileNew + " se ha cargado exitosamente.";
                }
                else
                {
                    cargaArchivo.PostedFile.SaveAs(strFilePathNew);
                    archivo = guardar(archivo);
                    lblMensaje.Text = " El archivo:" + strFileNew + " se ha cargado exitosamente.";
                }
            }
            else
            {
                lblMensaje.Text = "Selecione el archivo para cargarlo.";
            }
            // Display the result of the upload.
            frmConfirmation.Visible = true;
        }


       

        protected void obtenerArgumentos()
        {
            if (Request.QueryString["id"] != null)
            {
                int idI = int.Parse(Request.QueryString["id"]);
                if (idI != 0)
                    id = idI;

            }

            if (Request.QueryString["idReferencia"] != null)
                idReferencia = int.Parse(Request.QueryString["idReferencia"]);
            if (Request.QueryString["idDocumento"] != null)
                idDocumento = int.Parse(Request.QueryString["idDocumento"]);
            nombreDocumento = Request.QueryString["nombreDocumento"];
        }
        protected void mostrarDocumento()
        {
            lblDocumento.Text = "Documento:" + nombreDocumento;

        }

        protected ImagenCarrusel instanciar()
        {
            ImagenCarrusel archivo = new ImagenCarrusel();
            archivo.idSuscriptor = 1;
            archivo.activo = true;
            archivo.titulo = txtTitulo.Text;
            archivo.mensaje= txtMensaje.Text;
            //archivo.idReferencia = idReferencia;
            //archivo.idDocumento = idDocumento;
            return archivo;
        }


        protected ImagenCarrusel guardar(ImagenCarrusel documentoI)
        {
            Modelo db = new Modelo();
            ImagenCarrusel documento = db.ImagenCarrusel.Find(documentoI.idImagen);
            if (documento == null)
            {
                db.ImagenCarrusel.Add(documentoI);
            }
            else
            {
                documento.nombre = documentoI.nombre;
                documento.ruta = documentoI.ruta;
                db.Entry(documento).State = EntityState.Modified;
            }
            db.SaveChanges();
            id = documentoI.idImagen;
            return documentoI;

        }
        protected ImagenCarrusel obtener(ImagenCarrusel documentoI)
        {
            Modelo db = new Modelo();
            ImagenCarrusel archivo = db.ImagenCarrusel.Find(documentoI.idImagen);
            if (archivo == null)
            {
                archivo = archivo;
            }
            else
            {

            }
            return archivo;
        }
    }
}