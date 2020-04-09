﻿using BLL;
using BLL.Enums;
using BLL.Utilidades;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class NotasRapidasController : Controller
    {
        // GET: Notas_Rapidas
        public ActionResult Index()
        {
            Bll_Login.VerificarSesionActiva();

            Bll_NotasRapidas BLL_NotasRapidas = new Bll_NotasRapidas();
            List<NotasRapidas> Notas_Rapidas = BLL_NotasRapidas.ListarNotasRapidas(EnumEstadoFiltro.Todos);

            return View(Notas_Rapidas);
        }

        // GET: Notas_RapidasAdd
        public ActionResult NotasRapidasAdd()
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text");
            return View();
        }

        // POST: Crear Notas_Rapidas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NotasRapidasAdd(NotasRapidas NotasRapidas)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)NotasRapidas.Estado);

            if (ModelState.IsValid)
            {
                Bll_NotasRapidas Bll_NotasRapidas = new Bll_NotasRapidas();

                if (Bll_NotasRapidas.GuardarNotaRapida(NotasRapidas))
                {// pregunta si la funcion de creacion se ejecuto con exito
                    return RedirectToAction("Index");
                }
                else
                {// no creado
                    return View(NotasRapidas);
                }
            }
            else
            {
                return View(NotasRapidas);
            }
        }


        [HttpGet]
        public ActionResult NotasRapidasUpdt(int id)
        {
            Bll_Login.VerificarSesionActiva();

            Bll_NotasRapidas Bll_NotasRapidas = new Bll_NotasRapidas();
            NotasRapidas NotaRapida = Bll_NotasRapidas.GetNotasRapidasByNotaRapidaId(id);
            //NotaRapida.FechaFinalizacionView = NotaRapida.FechaFinalizacion.ToString("yyyy-MM-dd");
            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)NotaRapida.Estado); 
            return View(NotaRapida);
        }

        //Update Notas_Rapidas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NotasRapidasUpdt(NotasRapidas NotasRapidas)
        {
            Bll_Login.VerificarSesionActiva();

            ViewBag.Estado = new SelectList(FuncionesEnum.ListaEnum<EnumEstados>(), "Value", "Text", (int)NotasRapidas.Estado);

            if (NotasRapidas != null)
            {
                if (ModelState.IsValid)
                {
                    Bll_NotasRapidas Bll_NotasRapidas = new Bll_NotasRapidas();

                    if (Bll_NotasRapidas.ModificarNotasRapidas(NotasRapidas))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(NotasRapidas);
                    }
                }
                else
                {
                    return View(NotasRapidas);
                }
            }
            else
            {
                return View(NotasRapidas);
            }
        }

    }
}