using System;
using System.Web.Mvc;
using Application.DTO;
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

        public ActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
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
                _playersService.SavePlayer(dto);
                //IEnumerable<ValidationResult> errors = commandBus.Validate(command);
                //ModelState.AddModelErrors(errors);
                //if (ModelState.IsValid)
                //{
                //    var result = commandBus.Submit(command);
                //    if (result.Success)
                //          return RedirectToAction("Index");
                //}
            }

            //if fail
            if (form.Id == Guid.Empty)
                return View("Create", form);

            return View("Edit", form);
        }
    }
}
