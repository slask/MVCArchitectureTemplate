namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScrabblePlayers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        JoinDate = c.DateTime(),
                        ContactPhoneNumber = c.String(),
                        StreetAddress = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlayDate = c.DateTime(),
                        Location = c.String(),
                        Player1Score = c.Int(nullable: false),
                        Player2Score = c.Int(nullable: false),
                        Player1_Id = c.Guid(),
                        Player2_Id = c.Guid(),
                        ScrabblePlayer_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScrabblePlayers", t => t.Player1_Id)
                .ForeignKey("dbo.ScrabblePlayers", t => t.Player2_Id)
                .ForeignKey("dbo.ScrabblePlayers", t => t.ScrabblePlayer_Id)
                .Index(t => t.Player1_Id)
                .Index(t => t.Player2_Id)
                .Index(t => t.ScrabblePlayer_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Games", new[] { "ScrabblePlayer_Id" });
            DropIndex("dbo.Games", new[] { "Player2_Id" });
            DropIndex("dbo.Games", new[] { "Player1_Id" });
            DropForeignKey("dbo.Games", "ScrabblePlayer_Id", "dbo.ScrabblePlayers");
            DropForeignKey("dbo.Games", "Player2_Id", "dbo.ScrabblePlayers");
            DropForeignKey("dbo.Games", "Player1_Id", "dbo.ScrabblePlayers");
            DropTable("dbo.Games");
            DropTable("dbo.ScrabblePlayers");
        }
    }
}
