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
    public partial class index1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["mensaje"] != null)
            {
                lblMensaje.Text = Session["mensaje"].ToString();
                Session["mensaje"] = null;
            }
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            controladorUsuario cu = new controladorUsuario();
            if (cu.loginUsuario(txtId.Text, txtPass.Text).Equals("Login Exitoso."))
            {

                lblMensaje.Text = cu.loginUsuario(txtId.Text, txtPass.Text);
                Usuario us = cu.buscarUsuario(txtId.Text);
                Session["usuario"] = us;
                Response.Redirect("foro.aspx");
            }
            else
            {
                lblMensaje.Text = cu.loginUsuario(txtId.Text, txtPass.Text);
            }
        }
    }
}