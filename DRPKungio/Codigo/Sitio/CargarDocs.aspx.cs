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
    public partial class CargarDocs : System.Web.UI.Page
    {
        static private int id = 0;
        private int? idReferencia = 0;
        private int? idDocumento = 0;
        static private string nombreDocumento="";
        static private FlujoTrabajoDocumento documentoActual;
        protected void Page_Load(object sender, EventArgs e)
        {
            obtenerArgumentos();
            mostrarDocumento();
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

            //  http://localhost:50954/CargarDocs.aspx?id=0&idReferencia=1&idDocumento=2&nombreDocumento=Acta  

            //  http://kungio.mx/CargarDocs.aspx?id=39&idReferencia=1&idDocumento=2&nombreDocumento=Acta   
            obtenerArgumentos();
            mostrarDocumento();

            FlujoTrabajoDocumento documento = instanciar();
            documentoActual = documento;
            //strFolder = Server.MapPath("./");


            // Retrieve the name of the file that is posted.
            //strFileName = oFile.PostedFile.FileName;
            //strFileName = Path.GetFileName(strFileName);

            strFolder = Server.MapPath("./Docs/");
            strFileName = System.IO.Path.GetFileName(CargaArchivo.PostedFile.FileName);
            strFileName = CargaArchivo.FileName;
            strFileExt= System.IO.Path.GetExtension(CargaArchivo.PostedFile.FileName);
            if (CargaArchivo.FileName != "")
            {
                // Create the folder if it does not exist.
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }
                // Save the uploaded file to the server.
                strFilePath = strFolder + strFileName;

                strFileNew = idReferencia +"_"+ idDocumento + "_" + nombreDocumento + strFileExt;
                strFilePathNew = strFolder + strFileNew;
                if (File.Exists(strFilePathNew))
                {
                    lblUploadResult.Text = "Ya existe  el documento:" + strFilePathNew;
                    CargaArchivo.PostedFile.SaveAs(strFilePathNew);
                    documento.ruta = strFileNew;
                    documentoActual = guardar(documento);
                    lblUploadResult.Text = strFileName + " El dcoumento:" + strFileNew + " se ha cargado exitosamente.";
         
                }
                else
                {
                    CargaArchivo.PostedFile.SaveAs(strFilePathNew);
                    documento.ruta = strFileNew;
                    documentoActual=guardar(documento);
                    lblUploadResult.Text = " El dcoumento:"+ strFileNew + " se ha cargado exitosamente.";
                }
            }
            else
            {
                lblUploadResult.Text = "Selecionar el documento para cargarlo.";
            }
            // Display the result of the upload.
            frmConfirmation.Visible = true;
        }

        protected void obtenerArgumentos()
        {
            if  (id==null && id==0)
                 id = int.Parse(Request.QueryString["id"]);
            idReferencia = int.Parse(Request.QueryString["idReferencia"]);
            idDocumento = int.Parse(Request.QueryString["idDocumento"]);
            nombreDocumento = Request.QueryString["nombreDocumento"];
        }
        protected void mostrarDocumento()
        {
             lblDocumento.Text ="Documento:"+ nombreDocumento;
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
                db.Entry(documentoI).State = EntityState.Modified;
            }
            db.SaveChanges();
            id = documentoI.id;
            return documentoI;

        }
        protected FlujoTrabajoDocumento obtener(FlujoTrabajoDocumento documentoI)
        {
            Modelo db = new Modelo();
            FlujoTrabajoDocumento documento = db.FlujoTrabajoDocumento.Find(documentoI.id);
            if (documento == null)
            {
                 documento =  new FlujoTrabajoDocumento();
            }
            else
            {
                
            }
            return documento;
        }

        protected void btnDes_Click(object sender, EventArgs e)
        {
            obtenerArgumentos();
            mostrarDocumento();
            FlujoTrabajoDocumento documento = instanciar();
            String strFolder = Server.MapPath("./Docs/");
            String strRuta = "";
            //if (documento.id==0  && documentoActual.id!=0)
            //    documento.id=documentoActual.id; 
            documento = obtener(documento);
            strRuta = strFolder + documento.ruta;
            if (documento.ruta.Contains("pdf"))
                Response.ContentType = "application/pdf";
            else
                Response.ContentType = "application/xls";

            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("NombreCabecera", "MensajeCabecera");
            Response.TransmitFile(strRuta);
            Response.End();
        }
    }
}