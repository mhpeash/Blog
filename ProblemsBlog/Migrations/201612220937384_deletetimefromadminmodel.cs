namespace ProblemsBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletetimefromadminmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AdminControls", "MessageToAdmin");
            DropColumn("dbo.AdminControls", "MessageToUser");
            DropColumn("dbo.AdminControls", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminControls", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.AdminControls", "MessageToUser", c => c.String());
            AddColumn("dbo.AdminControls", "MessageToAdmin", c => c.String());
        }
    }
}
