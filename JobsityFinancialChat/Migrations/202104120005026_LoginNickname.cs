namespace JobsityFinancialChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginNickname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nickname", c => c.String());
            DropColumn("dbo.AspNetUsers", "Hometown");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Hometown", c => c.String());
            DropColumn("dbo.AspNetUsers", "Nickname");
        }
    }
}
