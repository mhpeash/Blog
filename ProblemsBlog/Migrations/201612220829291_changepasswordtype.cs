namespace ProblemsBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepasswordtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminControls", "Password", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdminControls", "Password");
        }
    }
}
