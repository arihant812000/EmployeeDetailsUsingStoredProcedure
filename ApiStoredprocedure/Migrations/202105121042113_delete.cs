namespace ApiStoredprocedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete : DbMigration
    {
        public override void Up()
        {
            var delete = "delete from employeeDetails where EmpId=@id" +
                   " delete from EmployeeAddresses where EmpId = @id ";
            CreateStoredProcedure("deleteEmp", p => new {
                id = p.Int(),
            }, body: delete);
        
    }
        
        public override void Down()
        {
        }
    }
}
