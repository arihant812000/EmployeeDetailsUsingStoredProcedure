namespace ApiStoredprocedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdfirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeAddresses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Address_type = c.String(),
                    House_number = c.String(),
                    Street = c.String(),
                    City = c.String(),
                    State = c.String(),
                    Country = c.String(),
                    Pin_Code = c.Int(nullable: false),
                    Landmark = c.String(),
                    Empid = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.employeeDetails", t => t.Empid, cascadeDelete: true)
                .Index(t => t.Empid);

            CreateTable(
                "dbo.employeeDetails",
                c => new
                {
                    EmpId = c.Int(nullable: false, identity: true),
                    First_Name = c.String(),
                    Last_Name = c.String(),
                    Date_of_Birth = c.String(),
                    Age = c.Int(nullable: false),
                    Email = c.String(),
                    Phone_No = c.String(),
                    Department = c.String(),
                    Job_Role = c.String(),
                })
                .PrimaryKey(t => t.EmpId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.EmployeeAddresses", "Empid", "dbo.employeeDetails");
            DropIndex("dbo.EmployeeAddresses", new[] { "Empid" });
            DropTable("dbo.employeeDetails");
            DropTable("dbo.EmployeeAddresses");
        }
    }
}
