namespace ApiStoredprocedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getAll : DbMigration
    {
        public override void Up( )
        {
          
            CreateStoredProcedure("getAll", body: "select * from employeeDetails");
          
        }
        
        public override void Down()
        {
            DropStoredProcedure("getAll");
        }
    }
}
