using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MeNet.Seguridad.Administrador;
using DRP.Modelo;
using System.Data.Entity.Core.Objects;
using System.Data;
namespace Sitio.Comun.Controles
{
    public partial class UcWebArbol : System.Web.UI.UserControl
    {
        private string _elementosSeleccionados = string.Empty;
        private string _tipoElemento = "P";
        private int _idElemento = 1;
        static private DataSet datos;
        static public string _parametrosDinamicos = string.Empty;
        static public bool CambioParametrosDinamicos = false;
        public string ParametrosDinamicos
        {
            get { return _parametrosDinamicos; }
            set
            {
                if (_parametrosDinamicos != value)
                    CambioParametrosDinamicos = true;
                _parametrosDinamicos = value;
            }
        }

        public string ElementosSeleccionados { get { return _elementosSeleccionados; } set { _elementosSeleccionados = value; } }
        protected void Page_Load(object sender, EventArgs e)
         {
            if (!IsPostBack || CambioParametrosDinamicos)
            {
                tvArbol.Nodes.Clear();
                CrearNodos(datos);
                CambioParametrosDinamicos = false;
            }


            //ObtenerElementosSeleccionados();
            //AsignarElementosSeleccionados("1");
        }
        public string TipoElemento
        {
            get { return _tipoElemento; }
            set { _tipoElemento = value; }
        }
        public int IdElemento
        {
            get { return _idElemento; }
            set { _idElemento = value; }
        }
        public void CargarDatos(DataSet datosEntrada)
        {

     
            try
            {

                    //tvArbol.Nodes.Clear();
                    datos = datosEntrada;
                    //CrearNodos(datos);
              
            }
            catch (Exception error)
            {

            }
        }
        public  void CrearNodos(DataSet datos)
        {
            TreeNode nodePrincipal = new TreeNode();
            TreeNode nodeSecundario = new TreeNode();
            TreeNode nodo = new TreeNode();
            string IdNodo1 = string.Empty;
            string IdNodoTemp1 = string.Empty;
            string Nodo1 = string.Empty;
            string ActivoNodo1 = string.Empty;

            string IdNodo2 = string.Empty;
            string IdNodoTemp2 = string.Empty;
            string Nodo2 = string.Empty;
            string ActivoNodo2 = string.Empty;

            string IdNodo3 = string.Empty;
            string IdNodoTemp3 = string.Empty;
            string Nodo3 = string.Empty;
            string ActivoNodo3 = string.Empty;
            if (datos != null)
            {
        

                for (int num = 0; num < datos.Tables[0].Rows.Count; num++)
                {
                    DataRow row = datos.Tables[0].Rows[num];
                    if (row != null)
                    {
                        IdNodoTemp1 = row.ItemArray.GetValue(0).ToString();
                        if (IdNodoTemp1 != null && IdNodo1 != IdNodoTemp1)
                        {
                            IdNodo1 = IdNodoTemp1;
                            Nodo1 = row.ItemArray.GetValue(1).ToString();
                            ActivoNodo1 = row.ItemArray.GetValue(2).ToString();
                            nodePrincipal = AsigarNodo(IdNodo1, IdNodo1 + ":" + Nodo1, Nodo1, ActivoNodo1);
                            tvArbol.Nodes.Add(nodePrincipal);
                        }

                        IdNodoTemp2 = row.ItemArray.GetValue(3).ToString();
                        if (IdNodoTemp2 != null && IdNodo2 != IdNodoTemp2)
                        {
                            IdNodo2 = IdNodoTemp2;
                            Nodo2 = row.ItemArray.GetValue(4).ToString();
                            ActivoNodo2 = row.ItemArray.GetValue(5).ToString();
                            nodeSecundario = AsigarNodo(IdNodo2, IdNodo2 + ":" + Nodo2, Nodo2, ActivoNodo2);
                            nodePrincipal.ChildNodes.Add(nodeSecundario);
                        }

                        IdNodo3 = row.ItemArray.GetValue(6).ToString();
                        if (IdNodo3 != null)
                        {
                            Nodo3 = row.ItemArray.GetValue(7).ToString();
                            ActivoNodo3 = row.ItemArray.GetValue(8).ToString();
                            nodo = new TreeNode();
                            nodo = AsigarNodo(IdNodo3, IdNodo3 + ":" + Nodo3, Nodo3, ActivoNodo3);
                            nodeSecundario.ChildNodes.Add(nodo);
                        }
                    }
                }
            }
        }
        public TreeNode AsigarNodo(string clave, string titulo,   string mensaje, string activo)
        {
            TreeNode nodo= new TreeNode();
            if (titulo != null)
            {
                nodo.Value = clave;
                nodo.Text = titulo;
                nodo.ToolTip = mensaje;
                if (activo != string.Empty && activo != "0")
                {
                    nodo.Selected = true;
                    nodo.Checked = true;
                }
                else
                {
                    nodo.Selected = false;
                    nodo.Checked = false;
                }
             }
            return nodo;
        }

        public string  ObtenerElementosSeleccionados()
        {
            string elementos = string.Empty;
            foreach (TreeNode item in tvArbol.Nodes)
            {
                elementos = ObtenerCadenaElementosArbol(item,elementos);
                foreach (TreeNode item2 in item.ChildNodes)
                {
                    elementos = ObtenerCadenaElementosArbol(item2,elementos);
                    foreach (TreeNode item3 in item2.ChildNodes)
                    {
                        elementos=ObtenerCadenaElementosArbol(item3,elementos);
                    }
            
                }

            }
            return elementos;
        }
        public string ObtenerCadenaElementosArbol(TreeNode item, string elementos)
        {
            if (item.Checked || item.Selected)
            {
                if (elementos != string.Empty)
                    elementos = elementos + ",";
                elementos = elementos + item.Value;
            }
            return elementos;
        }
        public void AsignarElementosSeleccionados(string valor)
        {
 
            foreach (TreeNode item in tvArbol.Nodes)
            {
                foreach (TreeNode item2 in item.ChildNodes)
                {
                    if (valor.Contains(item2.Value) )
                    {
                        item2.Checked = true;
                        item2.Selected = true;
                    }

                }

            }
        }

    }
}