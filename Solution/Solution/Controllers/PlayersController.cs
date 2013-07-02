using System;
using System.Linq;
using System.Web.Mvc;
using Application.DTO;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Solution.Controllers;
using Solution.Convertors;
using Solution.Extensions;
using Solution.Models.Players;

[assembly: WebActivator.PreApplicationStartMethod(typeof(PlayersController), "AutoMapperStart")]
namespace Solution.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayersService _playersService;

        public static void AutoMapperStart()
        {
            Mapper.CreateMap<ScrabblePlayer, PlayerViewModel>();
            Mapper.CreateMap<ScrabblePlayer, PlayerEditModel>();
            Mapper.CreateMap<PlayerEditModel, PlayerDto>();
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
            ScrabblePlayer player = _playersService.GetPlayer(id);
            var playerModel = player.ToPlayerEditModel();
            return View(playerModel);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var result = _playersService.ExcludePlayerFromClub(id);
            if (result.Success)
            {
                var allPlayers = _playersService.GetAllPlayers();
                return PartialView("_PlayersList", allPlayers.ToPlayerViewModelList());
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(PlayerEditModel form)
        {
            if (ModelState.IsValid)
            {
                var dto = Mapper.Map<PlayerEditModel, PlayerDto>(form);
                var result = _playersService.SavePlayer(dto);
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelErrors(result.ValidationFaults);
                //maybe do here other come complex logic if necessary
            }

            //if fail
            if (form.Id == Guid.Empty)
                return View("Create", form);

            return View("Edit", form);
        }
    }
}
