﻿using BLL;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class InicioController : Controller
    {
        public ActionResult Index()
        {
            Bll_Login.VerificarSesionActiva();

            Bll_NotasRapidas Bll_NotasRapidas = new Bll_NotasRapidas();
            List<NotasRapidas> Lista = Bll_NotasRapidas.VisualizarNotas();
            return View(Lista);
        }

    }
}