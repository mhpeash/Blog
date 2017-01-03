namespace ProblemsBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addemailmessagetoadminclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageToAdmins", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageToAdmins", "Email");
        }
    }
}
