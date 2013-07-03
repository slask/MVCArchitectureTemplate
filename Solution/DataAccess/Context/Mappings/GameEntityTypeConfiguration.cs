using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DataAccess.Context.Mappings
{
    /// <summary>
    /// Book entity type configuration
    /// </summary>
    internal class GameEntityTypeConfiguration : EntityTypeConfiguration<Game>
    {
        public GameEntityTypeConfiguration()
        {
            ToTable("Games");
        }
    }
}
