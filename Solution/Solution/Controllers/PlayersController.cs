using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Solution.Controllers;

[assembly: WebActivator.PreApplicationStartMethod(typeof(PlayersController), "AutoMapperStart")]
namespace Solution.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayersService _playersService;

        public static void AutoMapperStart()
        {
            //Single Responsibility Principle
            //Open-Closed Principle
            //Ease of maintenance
            
            //AutoMapper.Mapper.CreateMap<>();
        }

        public PlayersController(IPlayersService playersService)
        {
            _playersService = playersService;
            //TODO: setup Di with autofac
        }

        //
        // GET: /Players/

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Edit(Guid id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }
    }
}
