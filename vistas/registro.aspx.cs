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
    public partial class registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["mensaje"] != null)
            {
                lblMensaje.Text = Session["mensaje"].ToString();
                Session["mensaje"] = null;
            }
                
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            controladorUsuario cu = new controladorUsuario();
            Usuario u = new Usuario();
            u.Rut = txtRut.Text;
            u.Correo = txtEmail.Text;
            u.Nombre = txtNombre.Text;
            u.Pass = txtPass.Text;
            if (cu.registroUsuario(u, txtPass2.Text.ToString()).Equals("Usuario Registrado correctamente."))
            {
                Session["mensaje"] = "Usuario Registrado correctamente.";
                Response.Redirect("index.aspx");
            }
            else
            {
                Session["mensaje"] = cu.registroUsuario(u, txtPass2.Text);
                lblMensaje.Text = cu.registroUsuario(u, txtPass2.Text);
                //Response.Redirect("registro.aspx");
            }
        }
    }
} 