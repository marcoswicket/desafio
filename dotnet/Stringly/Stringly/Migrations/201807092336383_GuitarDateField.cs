namespace Stringly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuitarDateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guitars", "InclusionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guitars", "InclusionDate");
        }
    }
}
