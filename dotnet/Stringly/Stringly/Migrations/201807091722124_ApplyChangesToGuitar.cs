namespace Stringly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyChangesToGuitar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guitars", "Description", c => c.String());
            AlterColumn("dbo.Guitars", "Name", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guitars", "Name", c => c.String());
            DropColumn("dbo.Guitars", "Description");
        }
    }
}
