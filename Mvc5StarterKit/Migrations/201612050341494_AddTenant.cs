namespace Mvc5StarterKit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTenant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Tenant_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Tenant_Id");
            AddForeignKey("dbo.AspNetUsers", "Tenant_Id", "dbo.Tenants", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Tenant_Id", "dbo.Tenants");
            DropIndex("dbo.AspNetUsers", new[] { "Tenant_Id" });
            DropColumn("dbo.AspNetUsers", "Tenant_Id");
            DropTable("dbo.Tenants");
        }
    }
}
