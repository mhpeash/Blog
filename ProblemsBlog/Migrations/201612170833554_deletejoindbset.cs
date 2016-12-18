namespace ProblemsBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletejoindbset : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserJoinComments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserJoinComments",
                c => new
                    {
                        UserJoinCommentId = c.Int(nullable: false, identity: true),
                        CommenterName = c.String(),
                        Image = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.UserJoinCommentId);
            
        }
    }
}
