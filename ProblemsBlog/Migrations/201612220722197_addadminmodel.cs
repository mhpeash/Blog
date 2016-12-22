namespace ProblemsBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addadminmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminControls",
                c => new
                    {
                        AdminControlId = c.Int(nullable: false, identity: true),
                        AdminName = c.String(),
                        ConfirmPassword = c.String(),
                        Picture = c.String(),
                        MessageToAdmin = c.String(),
                        MessageToUser = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AdminControlId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminControls");
        }
    }
}
