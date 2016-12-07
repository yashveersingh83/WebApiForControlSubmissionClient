namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MileStones", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MileStones", "Name", c => c.String(nullable: false));
        }
    }
}
