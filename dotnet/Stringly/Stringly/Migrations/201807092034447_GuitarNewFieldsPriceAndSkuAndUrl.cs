namespace Stringly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuitarNewFieldsPriceAndSkuAndUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guitars", "Price", c => c.Single(nullable: false));
            AddColumn("dbo.Guitars", "ImageURL", c => c.String());
            AddColumn("dbo.Guitars", "SKU", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guitars", "SKU");
            DropColumn("dbo.Guitars", "ImageURL");
            DropColumn("dbo.Guitars", "Price");
        }
    }
}
