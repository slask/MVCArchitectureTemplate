using System;
using System.Web.Mvc;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Solution.Controllers;
using Solution.Convertors;
using Solution.Models.Players;

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
            
            Mapper.CreateMap<ScrabblePlayer, PlayerViewModel>();
            Mapper.AssertConfigurationIsValid();
        }

        public PlayersController(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        public ActionResult Index()
        {
            var viewModel = new PlayersListViewModel
                {
                    Players = _playersService.GetAllPlayers().ToPlayerViewModelList()
                };
            return View(viewModel);
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
