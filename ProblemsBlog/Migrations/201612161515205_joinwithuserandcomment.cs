namespace ProblemsBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class joinwithuserandcomment : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.UserJoinComments");
        }
    }
}
