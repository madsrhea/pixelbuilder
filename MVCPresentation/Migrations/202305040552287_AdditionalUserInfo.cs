namespace MVCPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalUserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ShortBio", c => c.String());
            AddColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime(nullable: false));
            DropTable("dbo.Arts");
            DropTable("dbo.Canvas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Canvas",
                c => new
                    {
                        CanvasID = c.Int(nullable: false, identity: true),
                        ArtID = c.Int(nullable: false),
                        Row = c.Int(nullable: false),
                        Column = c.Int(nullable: false),
                        BeadID = c.String(),
                        Username = c.String(),
                        TagName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CanvasID);
            
            CreateTable(
                "dbo.Arts",
                c => new
                    {
                        ArtID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Username = c.String(),
                        ArtName = c.String(),
                        Description = c.String(),
                        PostedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        Hidden = c.Boolean(nullable: false),
                        CanvasID = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ArtID);
            
            DropColumn("dbo.AspNetUsers", "CreatedOn");
            DropColumn("dbo.AspNetUsers", "ShortBio");
        }
    }
}
