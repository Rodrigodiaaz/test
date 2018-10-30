using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Tesis.modelos;

namespace Tesis.controlador
{
    public class controladorUsuario
    {
        //metodo para validar correo
        public bool validaCorreo(string seMailAComprobar)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(seMailAComprobar, sFormato))
            {
                if (Regex.Replace(seMailAComprobar, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //metodo que pasa la inicial de nombres y apellidos a mayusculas.
        public string NombreMayuscula(string Nombre)
        {
            string palabra;
            String palabraMayuscula = "";
            string[] separados = Nombre.Split(' ');
            for (int i = 0; i < separados.Length; i++)
            {
                for (int j = 0; j < separados[i].Length; j++)
                {
                    if (i == 0)
                    {
                        palabra = separados[i];

                        if (j == 0)
                        {
                            palabraMayuscula += palabra[j].ToString().ToUpper();
                        }
                        else
                        {
                            palabraMayuscula += palabra[j].ToString().ToLower();
                        }
                    }
                    else
                    {
                        palabra = separados[i];
                        if (j == 0)
                        {
                            palabraMayuscula += " " + palabra[j].ToString().ToUpper();
                        }
                        else
                        {
                            palabraMayuscula += palabra[j].ToString().ToLower();
                        }
                    }

                }
            }
            return palabraMayuscula;
        }

        //metodo para buscar usuario desde controlador
        public Usuario buscarUsuario(string id)
        {
            Usuario u = new Usuario();
            Usuario us = null;
            us = u.buscaUno(id, id);
            return us;
        }
        //metodo para autenticar usuario
        public String loginUsuario(string id, string pass)
        {
            if (id.Equals("") || pass.Equals(""))
            {
                return "Campos vacios.";
            }
            else
            {
                Usuario u = new Usuario();
                Usuario us = null;
                us = u.buscaUno(id, id);
                if (us == null)
                {
                    return "El correo o rut ingresado no existen.";
                }
                else
                {
                    if (us.Pass.Equals(pass))
                    {
                        return "Login Exitoso.";
                    }
                    else
                    {
                        return "Contraseña incorrecta.";
                    }
                }
            }
        }
        //metodo para registrar usuario
        public String registroUsuario(Usuario u, string repiteClave)
        {
            if (u.Rut.Equals("") || u.Correo.Equals("") || u.Nombre.Equals("") || u.Pass.Equals("") || repiteClave.Equals(""))
            {
                return "Campos vacios, por favor verifiquelos";
            }
            else
            {
                if (validaCorreo(u.Correo))
                {
                    if (validarRut(u.Rut))
                    {
                        Usuario us = null;
                        u.Rut = u.Rut.Replace(".", "");
                        u.Nombre = NombreMayuscula(u.Nombre);
                        us = u.buscaUno(u.Correo, u.Rut);
                        if (us == null)
                        {
                            if (u.Pass.Equals(repiteClave))
                            {
                                if (u.insertaUsuario(u))
                                {
                                    return "Usuario Registrado correctamente.";
                                }
                                else
                                {
                                    return "Error al registrar usuario.";
                                }
                            }
                            else
                            {
                                return "Las contraseñas no coinciden.";
                            }
                        }
                        else
                        {
                            return "Rut o Correo ya en uso.";
                        }
                    }
                    else
                    {
                        return "Rut no valido.";
                    }
                }
                else
                {
                    return "Correo no valido.";
                }
            }
        }
        //metodo para validar rut
        public bool validarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }
    }
}
                