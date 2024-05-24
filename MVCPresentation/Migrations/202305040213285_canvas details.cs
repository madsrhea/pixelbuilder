namespace MVCPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class canvasdetails : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Canvas");
        }
    }
}
