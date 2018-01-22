namespace Dentist.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DentistStudioaddedpropertyTownandSpec : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DentistStudios", "Town", c => c.String());
            AddColumn("dbo.DentistStudios", "Spec", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DentistStudios", "Spec");
            DropColumn("dbo.DentistStudios", "Town");
        }
    }
}
