using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using DataAccess.Context;
using DataAccess.Migrations;
using Domain.Entities;
using NUnit.Framework;
using Solution.Bootstrapping;
using Solution.Controllers;
using Solution.Models.Players;
using Subtext.TestLibrary;
using TechTalk.SpecFlow;

namespace AcceptanceTests.PlayersManagement
{
    [Binding]
    public class PlayersManagementSteps
    {
        private const string TestDbConStrName = "TestDB";

        #region Setup/TearDown

        [BeforeTestRun]
        public static void SetupTestRun()
        {
            Bootstrapper.Run(true);

            //init/create database
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ScrabbleClubContext, Configuration>(TestDbConStrName));
            using (var context = new ScrabbleClubContext(TestDbConStrName))
            {
                context.ScrabblePlayers.Find(Guid.Empty);
                context.SaveChanges();
            }

            PlayersController.AutoMapperStart();
        }

        private TransactionScope Scope { get; set; }
        private HttpSimulator _httpSimulator;

        [BeforeScenario]
        public void ScenarioSetup()
        {
            _httpSimulator = new HttpSimulator().SimulateRequest();
            Scope = new TransactionScope(TransactionScopeOption.RequiresNew);

        }

        [AfterScenario]
        public void ScenarioCleanup()
        {
            if (Scope != null)
            {
                Scope.Dispose();
            }
            if (_httpSimulator != null)
            {
                _httpSimulator.Dispose();
            }
        }

        #endregion

        private PlayersController _sut;
        private ActionResult _sutResult;

        #region List all players

        [Given(@"I already have some players existing in the system")]
        public void GivenIAlreadyHaveSomePlayersExistingInTheSystem()
        {
            var player1 = new ScrabblePlayer
                {
                    Email = "a@a.com",
                    Name = "a"
                };
            player1.GenerateNewIdentity();

            var player2 = new ScrabblePlayer
                {
                    Email = "b@b.com",
                    Name = "b"
                };
            player2.GenerateNewIdentity();

            ScenarioContext.Current["plr1"] = player1;
            ScenarioContext.Current["plr2"] = player2;

            using (var context = new ScrabbleClubContext(TestDbConStrName))
            {
                context.ScrabblePlayers.Add(player1);
                context.ScrabblePlayers.Add(player2);
                context.SaveChanges();
            }
        }

        [When(@"I navigate to the players list")]
        public void WhenINavigateToThePlayersList()
        {
            
            _sut = CreatePlayersController();
            _sutResult = _sut.Index();
        }

        private static PlayersController CreatePlayersController()
        {
            return DependencyResolver.Current.GetService<PlayersController>();
        }

        [Then(@"I will see a list with the two players")]
        public void ThenIWillSeeAListWithTheTwoPlayers()
        {
            var viewResult = _sutResult as ViewResult;
            Debug.Assert(viewResult != null, "viewResult != null");
            var model = viewResult.Model as PlayersListViewModel;
            Debug.Assert(model != null, "model != null");
            Assert.That(model.Players.Any(vm => vm.Id == ((ScrabblePlayer) ScenarioContext.Current["plr1"]).Id), Is.True);
            Assert.That(model.Players.Any(vm => vm.Id == ((ScrabblePlayer) ScenarioContext.Current["plr2"]).Id), Is.True);
        }

        #endregion

        #region Add new player

        [Given(@"I change his profile data to the following")]
        [Given(@"I Enter the following information for a new player")]
        public void GivenIEnterTheFollowingInformationForANewPlayer(Table table)
        {
            var player = new PlayerEditModel
                {
                    ContactPhoneNumber = table.Rows[0]["ContactNo"],
                    Email = table.Rows[0]["Email"],
                    JoinDate = DateTime.Parse(table.Rows[0]["JoinDate"]),
                    Name = table.Rows[0]["Name"],
                    StreetAddress = table.Rows[0]["Address"]
                };

            if (ScenarioContext.Current.ContainsKey("plr1"))
            {
                var scrabblePlayer = ScenarioContext.Current["plr1"] as ScrabblePlayer;
                if (scrabblePlayer != null)
                {
                    var id = scrabblePlayer.Id;
                    player.Id = id; //set the ID in edit mode
                }
            }
            ScenarioContext.Current["addplr"] = player;
        }

        [When(@"I submit the changes")]
        [When(@"Submiting the player data")]
        public void WhenSubmitingThePlayerData()
        {
            _sut = CreatePlayersController();
            _sut.Save(ScenarioContext.Current["addplr"] as PlayerEditModel);
        }

       
        [Then(@"The new player is added in the system")]
        public void ThenTheNewPlayerIsAddedInTheSystem()
        {
            var playerModel = ScenarioContext.Current["addplr"] as PlayerEditModel;
            ScrabblePlayer playerRetrieved;
            using (var ctx = new ScrabbleClubContext(TestDbConStrName))
            {
                playerRetrieved = ctx.ScrabblePlayers.FirstOrDefault(
                    p =>
                    p.ContactPhoneNumber == playerModel.ContactPhoneNumber && p.Email == playerModel.Email && p.JoinDate == playerModel.JoinDate &&
                    p.Name == playerModel.Name && p.StreetAddress == playerModel.StreetAddress);
            }
            Assert.NotNull(playerRetrieved);
        }


        #endregion

        #region Edit player

        [Given(@"I have an existing player in the system")]
        public void GivenIHaveAnExistingPlayerInTheSystem()
        {
            var player1 = new ScrabblePlayer
            {
                Email = "a@a.com",
                Name = "a"
            };
            player1.GenerateNewIdentity();

            ScenarioContext.Current["plr1"] = player1;
         
            using (var context = new ScrabbleClubContext(TestDbConStrName))
            {
                context.ScrabblePlayers.Add(player1);
                context.SaveChanges();
            }
        }

        [Then(@"The modified data in stored in the system")]
        public void TheModifiedDataIsStoredInTheSystem()
        {
            var playerModel = ScenarioContext.Current["addplr"] as PlayerEditModel;
            var id = (ScenarioContext.Current["plr1"] as ScrabblePlayer).Id;
            ScrabblePlayer playerRetrieved;
            using (var ctx = new ScrabbleClubContext(TestDbConStrName))
            {
                playerRetrieved = ctx.ScrabblePlayers.FirstOrDefault(
                    p => p.Id == id &&
                         p.ContactPhoneNumber == playerModel.ContactPhoneNumber && p.Email == playerModel.Email && p.JoinDate == playerModel.JoinDate &&
                         p.Name == playerModel.Name && p.StreetAddress == playerModel.StreetAddress);
            }
            Assert.NotNull(playerRetrieved);
        }

        #endregion

        #region Remove player from club

        [When(@"I delete the player")]
        public void WhenIDeleteThePlayer()
        {
            _sut = CreatePlayersController();
            _sut.Delete((ScenarioContext.Current["plr1"] as ScrabblePlayer).Id);
        }

        [Then(@"The player is no longer stored in the system")]
        public void ThenThePlayerIsNoLongerStoredInTheSystem()
        {
            ScrabblePlayer playerRetrieved;
            using (var ctx = new ScrabbleClubContext(TestDbConStrName))
            {
                playerRetrieved = ctx.ScrabblePlayers.Find((ScenarioContext.Current["plr1"] as ScrabblePlayer).Id);
            }
            Assert.Null(playerRetrieved);
        }


        #endregion

    }
}
