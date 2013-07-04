using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Solution.Models.Players;

namespace Solution.Convertors
{
    public static class ViewModelConverters
    {

        public static PlayerViewModel ToPlayerViewModel(this ScrabblePlayer player)
        {
            return new PlayerViewModel
                {
                    Id = player.Id,
                    Email = player.Email,
                    Name = player.Name.ToUpper() //instead of ToUpper(), the logic can become more or less complex and Automappper cant handle it
                };
            //return AutoMapper.Mapper.Map<PlayerViewModel>(player);
        }

        public static PlayerEditModel ToPlayerEditModel(this ScrabblePlayer player)
        {
            return AutoMapper.Mapper.Map<ScrabblePlayer, PlayerEditModel>(player);
            //or use other more complex mapping logic if necessary for the edit view
        }

        public static IEnumerable<PlayerViewModel> ToPlayerViewModelList(this IEnumerable<ScrabblePlayer> items)
        {
            if (items != null)
            {
                var scrabblePlayers = items as IList<ScrabblePlayer> ?? items.ToList();
                var viewModelList = new List<PlayerViewModel>(scrabblePlayers.Count());
                viewModelList.AddRange(scrabblePlayers.Select(c => c.ToPlayerViewModel()));

                //if automapper will do fine then use it, expecially when having big entities with lots of properties
                //viewModelList.AddRange(scrabblePlayers.Select(c => AutoMapper.Mapper.Map<PlayerViewModel>(c)));

                return viewModelList;
            }
            return new List<PlayerViewModel>();
        }
    }
}