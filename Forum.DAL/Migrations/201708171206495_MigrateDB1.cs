namespace Forum.Core.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LOGTABLE", "Time", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LOGTABLE", "Time", c => c.DateTime(nullable: false));
        }
    }
}
