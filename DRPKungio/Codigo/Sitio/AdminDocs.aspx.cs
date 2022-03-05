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
    public partial class AdminDocs : System.Web.UI.Page
    {

        static private int id = 0;
        private int? idReferencia = 0;
        private int? idDocumento = 0;
        static private string nombreDocumento = "";
        static private FlujoTrabajoDocumento documento;

        protected void Page_Load(object sender, EventArgs e)
        {
            //lblDocumento.Text = "prueba";
            //String nombreDocumento = Request.QueryString["nombreDocumento"];
            //if (nombreDocumento != null)
            //    lblDocumento.Text = nombreDocumento;
            obtenerArgumentos();
            mostrarDocumento();
            documento = instanciar();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {


            string strFileName;
            string strFilePath;
            string strFileExt;
            string strFolder;

            string strFileNew;
            string strFilePathNew;

            //  http://localhost:50954/CargarDocs.aspx?id=39&idReferencia=1&idDocumento=2&nombreDocumento=Acta  

            // http://localhost:50954/CargarDocs?id=1043&idReferencia=20&idDocumento=4&nombreDocumento=Predial
            // http://localhost:50954/CargarDocs?id=1046&idReferencia=21&idDocumento=4&nombreDocumento=Predial
            //  http://localhost:50954/CargarDocs.aspx?id=0&idReferencia=1&idDocumento=2&nombreDocumento=Acta  

            //  cCargarDocs.aspx?id=39&idReferencia=1&idDocumento=2&nombreDocumento=Acta   

            // http://kungio.mx/CargarDocs?id=1043&idReferencia=1&idDocumento=4&nombreDocumento=Predial

            //obtenerArgumentos();
            //mostrarDocumento();

            //if  (documento==null )
            //     documento = instanciar();
     
            //strFolder = Server.MapPath("./");


            // Retrieve the name of the file that is posted.
            //strFileName = oFile.PostedFile.FileName;
            //strFileName = Path.GetFileName(strFileName);

            strFolder = Server.MapPath("./Docs/");
            strFileName = System.IO.Path.GetFileName(cargaArchivo.PostedFile.FileName);
            strFileName = cargaArchivo.FileName;
            strFileExt = System.IO.Path.GetExtension(cargaArchivo.PostedFile.FileName);
            if (cargaArchivo.FileName != "")
            {
                // Create the folder if it does not exist.
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }
                // Save the uploaded file to the server.
                strFilePath = strFolder + strFileName;

                strFileNew = idReferencia + "_" + idDocumento  + strFileExt;
                strFilePathNew = strFolder + strFileNew;
                if (File.Exists(strFilePathNew))
                {
                    lblMensaje.Text = "Ya existe  el documento:" + strFilePathNew;
                    cargaArchivo.PostedFile.SaveAs(strFilePathNew);
                    documento.ruta = strFileNew;
                    documento = guardar(documento);
                    lblMensaje.Text = strFileName + " El dcoumento:" + strFileNew + " se ha cargado exitosamente.";

                }
                else
                {
                    cargaArchivo.PostedFile.SaveAs(strFilePathNew);
                    documento.ruta = strFileNew;
                    documento= guardar(documento);
                    lblMensaje.Text = " El dcoumento:" + strFileNew + " se ha cargado exitosamente.";
                }
            }
            else
            {
                lblMensaje.Text = "Selecionar el documento para cargarlo.";
            }
            // Display the result of the upload.
            frmConfirmation.Visible = true;
        }
        protected void btnDes_Click(object sender, EventArgs e)
        {

            String strFolder = Server.MapPath("./Docs/");
            String strRuta = "";

            documento = obtener(documento);
            if (documento.id != null && (documento.ruta != null && documento.ruta != ""))
            {
                //strFolder = strFolder.Contains("C:") ? "http://kungio.mx/docs/" : strFolder;
                strRuta = strFolder + documento.ruta;
                if (documento.ruta.Contains("pdf"))
                    Response.ContentType = "application/pdf";
                else if (documento.ruta.Contains("xls"))
                    Response.ContentType = "application/xls";
                else if (documento.ruta.Contains("doc"))
                    Response.ContentType = "application/ms-word";
                else if (documento.ruta.Contains("jpg"))
                    Response.ContentType = "application/image/JPEG";
                else if (documento.ruta.Contains("gif"))
                    Response.ContentType = "application/image/GIF";
                else if (documento.ruta.Contains("mp4"))
                    Response.ContentType = "application/video/mp4";
                else if (documento.ruta.Contains("mp3"))
                    Response.ContentType = "application/video/mp3";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AppendHeader("NombreCabecera", "MensajeCabecera");
                Response.TransmitFile(strRuta);
                Response.End();
            }

        }
        protected void obtenerArgumentos()
        {
            if ( Request.QueryString["id"]!=null )
            {
                int  idI = int.Parse(Request.QueryString["id"]);
                if (idI != 0 )
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

        protected FlujoTrabajoDocumento instanciar()
        {
            FlujoTrabajoDocumento documento = new FlujoTrabajoDocumento();
            documento.id = id;
            documento.idReferencia = idReferencia;
            documento.idDocumento = idDocumento;
            return documento;
        }


        protected FlujoTrabajoDocumento guardar(FlujoTrabajoDocumento documentoI)
        {
            Modelo db = new Modelo();
            FlujoTrabajoDocumento documento = db.FlujoTrabajoDocumento.Find(documentoI.id);
            if (documento == null)
            {
                db.FlujoTrabajoDocumento.Add(documentoI);
            }
            else
            {
                db.Entry(documento).State = EntityState.Modified;
            }
            db.SaveChanges();
            id = documentoI.id;
            return documentoI;

        }
        protected FlujoTrabajoDocumento obtener(FlujoTrabajoDocumento documentoI)
        {
            Modelo db = new Modelo();
            FlujoTrabajoDocumento documentoO = db.FlujoTrabajoDocumento.Find(documentoI.id);
            if (documentoO == null)
            {
                documentoO = documento;
            }
            else
            {

            }
            return documentoO;
        }
    }
}