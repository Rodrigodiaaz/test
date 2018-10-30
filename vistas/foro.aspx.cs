using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tesis.controlador;
using Tesis.modelos;


namespace Tesis.vistas
{
    public partial class foro1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Session["mensaje"] = "No tiene permisos para acceder aqui, por favor logueese.";
                Response.Redirect("index.aspx");
            }
            else
            {
                Usuario u = (Usuario)Session["usuario"];
                lblNombre.Text = u.Nombre;

            }
              
        }
    }
}