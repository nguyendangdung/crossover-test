namespace Client.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class next : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Stocks", "Code", "RemoteId");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Stocks", "RemoteId", "Code");
        }
    }
}
