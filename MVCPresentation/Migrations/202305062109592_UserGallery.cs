namespace MVCPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserGallery : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Arts");
        }
    }
}