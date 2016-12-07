namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InformationRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InformationRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InformationRequired = c.String(),
                        RecepientId = c.Int(nullable: false),
                        MileStoneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MileStones", t => t.MileStoneId, cascadeDelete: true)
                .ForeignKey("dbo.Recepients", t => t.RecepientId, cascadeDelete: true)
                .Index(t => t.RecepientId)
                .Index(t => t.MileStoneId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InformationRequests", "RecepientId", "dbo.Recepients");
            DropForeignKey("dbo.InformationRequests", "MileStoneId", "dbo.MileStones");
            DropIndex("dbo.InformationRequests", new[] { "MileStoneId" });
            DropIndex("dbo.InformationRequests", new[] { "RecepientId" });
            DropTable("dbo.InformationRequests");
        }
    }
}
