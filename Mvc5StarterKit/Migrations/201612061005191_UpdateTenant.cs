namespace Mvc5StarterKit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTenant : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Tenant_Id", "dbo.Tenants");
            DropIndex("dbo.AspNetUsers", new[] { "Tenant_Id" });
            AlterColumn("dbo.AspNetUsers", "Tenant_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Tenant_Id");
            AddForeignKey("dbo.AspNetUsers", "Tenant_Id", "dbo.Tenants", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Tenant_Id", "dbo.Tenants");
            DropIndex("dbo.AspNetUsers", new[] { "Tenant_Id" });
            AlterColumn("dbo.AspNetUsers", "Tenant_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Tenant_Id");
            AddForeignKey("dbo.AspNetUsers", "Tenant_Id", "dbo.Tenants", "Id");
        }
    }
}
