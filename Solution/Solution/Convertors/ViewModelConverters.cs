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
                    Email = player.Email,
                    Name = player.Name.ToUpper() //instead of ToUpper(), the logic can become more or less complex and Automappper cant handle it
                };
        }

        public static IEnumerable<PlayerViewModel> ToPlayerViewModelList(this IEnumerable<ScrabblePlayer> items)
        {
            
            if (items != null)
            {
                var scrabblePlayers = items as IList<ScrabblePlayer> ?? items.ToList();
                var viewModelList = new List<PlayerViewModel>(scrabblePlayers.Count());
                viewModelList.AddRange(scrabblePlayers.Select(c => c.ToPlayerViewModel()));
               
                //if automapper will do fine then use it, expecially when haveign big antities with lots of properties
                //viewModelList.AddRange(scrabblePlayers.Select(c => AutoMapper.Mapper.Map<PlayerViewModel>(c)));

                return viewModelList;
            }
            return new List<PlayerViewModel>();
        }


    }
}