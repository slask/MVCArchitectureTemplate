using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DataAccess.Context.Mappings
{
    /// <summary>
    /// The entity type configuration
    /// </summary>
    internal class ScrabblePlayerEntityTypeConfiguration : EntityTypeConfiguration<ScrabblePlayer>
    {
        public ScrabblePlayerEntityTypeConfiguration()
        {
            //key and properties
            HasKey(sp => sp.Id);
        }
    }
}
