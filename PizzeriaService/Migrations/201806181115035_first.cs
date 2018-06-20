namespace PizzeriaService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CookFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderPizzas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitorId = c.Int(nullable: false),
                        PizzaId = c.Int(nullable: false),
                        CookId = c.Int(),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        TimeCreate = c.DateTime(nullable: false),
                        TimeDone = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cooks", t => t.CookId)
                .ForeignKey("dbo.Pizzas", t => t.PizzaId, cascadeDelete: true)
                .ForeignKey("dbo.Visitors", t => t.VisitorId, cascadeDelete: true)
                .Index(t => t.VisitorId)
                .Index(t => t.PizzaId)
                .Index(t => t.CookId);
            
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PizzaName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PizzaIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PizzaId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Pizzas", t => t.PizzaId, cascadeDelete: true)
                .Index(t => t.PizzaId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FridgeIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FridgeId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fridges", t => t.FridgeId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .Index(t => t.FridgeId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Fridges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FridgeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mail = c.String(),
                        VisitorFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.OrderPizzas", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.PizzaIngredients", "PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.PizzaIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.FridgeIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.FridgeIngredients", "FridgeId", "dbo.Fridges");
            DropForeignKey("dbo.OrderPizzas", "PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.OrderPizzas", "CookId", "dbo.Cooks");
            DropIndex("dbo.MessageInfoes", new[] { "VisitorId" });
            DropIndex("dbo.FridgeIngredients", new[] { "IngredientId" });
            DropIndex("dbo.FridgeIngredients", new[] { "FridgeId" });
            DropIndex("dbo.PizzaIngredients", new[] { "IngredientId" });
            DropIndex("dbo.PizzaIngredients", new[] { "PizzaId" });
            DropIndex("dbo.OrderPizzas", new[] { "CookId" });
            DropIndex("dbo.OrderPizzas", new[] { "PizzaId" });
            DropIndex("dbo.OrderPizzas", new[] { "VisitorId" });
            DropTable("dbo.MessageInfoes");
            DropTable("dbo.Visitors");
            DropTable("dbo.Fridges");
            DropTable("dbo.FridgeIngredients");
            DropTable("dbo.Ingredients");
            DropTable("dbo.PizzaIngredients");
            DropTable("dbo.Pizzas");
            DropTable("dbo.OrderPizzas");
            DropTable("dbo.Cooks");
        }
    }
}
