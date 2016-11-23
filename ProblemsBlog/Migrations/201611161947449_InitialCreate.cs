namespace ProblemsBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        UserPostId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PostContent = c.String(maxLength: 1000),
                        PostTitle = c.String(),
                        Author = c.String(),
                        Image = c.String(),
                        Time = c.DateTime(nullable: false),
                        Tempvalue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserPostId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        UserName = c.String(),
                        Password = c.String(maxLength: 100),
                        ConfirmPassword = c.String(),
                        Image = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.UserPosts");
        }
    }
}
