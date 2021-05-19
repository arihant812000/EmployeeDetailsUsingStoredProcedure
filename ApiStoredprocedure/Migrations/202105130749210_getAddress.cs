namespace ApiStoredprocedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class getAddress : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("getAdress",p=>new {
            id=p.Int()
            }, body: "select * from EmployeeAddresses where EmpId=@id");
        }
        
        public override void Down()
        {
        }
    }
}
