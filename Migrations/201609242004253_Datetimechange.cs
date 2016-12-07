namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datetimechange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MileStones", "StartDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MileStones", "StartDate");
        }
    }
}
