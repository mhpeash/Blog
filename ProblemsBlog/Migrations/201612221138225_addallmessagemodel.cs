namespace ProblemsBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addallmessagemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageToAdmins",
                c => new
                    {
                        MessageToAdminId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(),
                        MessageSubject = c.String(nullable: false),
                        UserMessage = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageToAdminId);
            
            CreateTable(
                "dbo.MessageToUsers",
                c => new
                    {
                        MessageToUserId = c.Int(nullable: false, identity: true),
                        MessageTitle = c.String(),
                        AdminMessage = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageToUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MessageToUsers");
            DropTable("dbo.MessageToAdmins");
        }
    }
}
