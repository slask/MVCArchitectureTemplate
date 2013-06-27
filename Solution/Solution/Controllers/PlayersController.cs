using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Solution.Controllers;

[assembly: WebActivator.PreApplicationStartMethod(typeof(PlayersController), "AutoMapperStart")]
namespace Solution.Controllers
{
    public class PlayersController : Controller
    {
        public static void AutoMapperStart()
        {
            //Single Responsibility Principle
            //Open-Closed Principle
            //Ease of maintenance
            
            //AutoMapper.Mapper.CreateMap<>();
        }

        //
        // GET: /Players/

        public ActionResult Index()
        {
            return View();
        }

    }
}
