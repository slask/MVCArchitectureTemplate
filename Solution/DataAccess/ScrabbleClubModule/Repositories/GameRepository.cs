using DataAccess.Context;
using DataAccess.Core;
using Domain.Entities;
using Domain.Repositories;

namespace DataAccess.ScrabbleClubModule.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public GameRepository(ScrabbleClubContext unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion
    }
}
