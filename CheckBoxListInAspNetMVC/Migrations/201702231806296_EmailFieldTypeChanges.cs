namespace CheckBoxListInAspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailFieldTypeChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Email", c => c.Int(nullable: false));
        }
    }
}
