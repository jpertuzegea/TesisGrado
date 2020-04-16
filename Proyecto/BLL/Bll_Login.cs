﻿using BCrypt.Net;
using BLL.Enums;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace BLL
{
    public class Bll_Login
    {

        private TESIS_BD BD = new TESIS_BD();


        // Metodo para validar Inicio de Sesion al sistema
        public bool IniciarSesion(Personas Persona)
        {
            try
            {
                var MyClave = Persona.Clave.ComputeHash(HashType.SHA256);
                Personas login = BD.Personas.Where(u => u.Email.ToUpper().Equals(Persona.Email.ToUpper())
                   &&
                   u.Clave.Equals(MyClave)
                   &&
                   u.Estado == (byte)EnumEstadoFiltro.Activo).FirstOrDefault();

                Bll_IngresoAlSistema Bll_IngresoAlSistema = new Bll_IngresoAlSistema();

                if ((login != null))
                {
                    System.Web.HttpContext.Current.Session["IdUsuarioTesis"] = login.PersonaId;
                    System.Web.HttpContext.Current.Session["NombreUsuarioTesis"] = login.NombreCompleto;
                    Bll_CursoEstudiante Bll_CursoEstudiante = new Bll_CursoEstudiante();

                    System.Web.HttpContext.Current.Session["CursosMatriculadosActivos"] = Bll_CursoEstudiante.ObtenerCantidadCusosActivosByPersonaId(login.PersonaId);

                    Bll_IngresoAlSistema.RegistroIngresoAlSitema(Persona.Email.ToUpper(), EnumEstadoAcceso.Acceso_Exitoso);
                  
                    return true;
                }
                else
                {
                    Bll_IngresoAlSistema.RegistroIngresoAlSitema(Persona.Email.ToUpper(), EnumEstadoAcceso.Acceso_Fallido);
                    return false;
                }
            }
            catch (Exception error)
            {
                Bll_File.EscribirLog(error.ToString());
                return false;
            }

        }

        // Metodo para cerrar la sesiojn y eliminar las variables de Sesion
        public void CerrarSesion()
        {
            System.Web.HttpContext.Current.Session["IdUsuarioTesis"] = null;
            System.Web.HttpContext.Current.Session["NombreUsuarioTesis"] = null;
            System.Web.HttpContext.Current.Session.Abandon();// destruye los objetos de sesion existentes
            FormsAuthentication.SignOut();
        }

        // Metodo que valida si existe una sesion activa
        public static void VerificarSesionActiva()
        {
            String NombreUsuarioTesis = (String)System.Web.HttpContext.Current.Session["NombreUsuarioTesis"];// se captura la variable de sesion con la que se validara que el usuario este logueado 

            if (NombreUsuarioTesis != null)
            {
                if (NombreUsuarioTesis.Length < 3)
                {
                    System.Web.HttpContext.Current.Response.Redirect("/login");
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect("/login");
            }
        }

        // Metodo para crear Cookies
        public void CrearCookie(string NombreCookie, string ValorCookie, int ExpiracionSegundos)
        {
            System.Web.HttpCookie Cookie = new System.Web.HttpCookie(NombreCookie);
            Cookie.Value = ValorCookie;
            Cookie.Expires = DateTime.Now.AddSeconds(ExpiracionSegundos);
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
        }
    }
}
