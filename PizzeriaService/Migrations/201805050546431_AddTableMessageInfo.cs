namespace PizzeriaService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableMessageInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        VisitorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visitors", t => t.VisitorId)
                .Index(t => t.VisitorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageInfoes", "VisitorId", "dbo.Visitors");
            DropIndex("dbo.MessageInfoes", new[] { "VisitorId" });
            DropTable("dbo.MessageInfoes");
        }
    }
}
