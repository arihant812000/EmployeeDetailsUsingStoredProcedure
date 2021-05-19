namespace ApiStoredprocedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterEmployee : DbMigration
    {
        public override void Up()
        {
            String edit= "declare @addresstype nvarchar(30),"+
"@housenumber nvarchar(30),"+
"@street nvarchar(20),"+
"@city nvarchar(30),"+
"@state nvarchar(30),"+
"@country nvarchar(30),"+
"@pincode int,"+
"@landmark nvarchar(30);"+
          "  update employeeDetails set First_Name = JSON_VALUE(@employee, '$.First_Name'),"+
             "Last_Name = JSON_VALUE(@employee, '$.Last_Name'),"+
             "Date_of_Birth = JSON_VALUE(@employee, '$.Date_of_Birth'),"+
             "Age = JSON_VALUE(@employee, '$.Age'),"+
             "Email = JSON_VALUE(@employee, '$.Email'),"+
             "Phone_No = JSON_VALUE(@employee, '$.Phone_No'),"+
             "Department = JSON_VALUE(@employee, '$.Department'),"+
            "Job_Role = JSON_VALUE(@employee, '$.Job_Role') where EmpId = @id;"+
"delete from EmployeeAddresses where Empid = @id;"+


"declare @key nvarchar(20)," +
"@value nvarchar(20)," +
"@type int;" +

           " declare curs cursor for select* from OpenJson(@employee, '$.empaddr');" +
            "open curs;" +
            "Fetch Next from curs into @key, @value, @type;" +
           " while @@FETCH_STATUS = 0" +
"begin;" +
"set @addresstype = JSON_VALUE(@employee, '$.empaddr[' + @key + '].Address_type')" +
"set @housenumber = JSON_VALUE(@employee, '$.empaddr[' + @key + '].House_number')" +
"set @street = JSON_VALUE(@employee, '$.empaddr[' + @key + '].Street')" +
"set @city = JSON_VALUE(@employee, '$.empaddr[' + @key + '].City')" +
"set @state = JSON_VALUE(@employee, '$.empaddr[' + @key + '].State')" +
"set @country = JSON_VALUE(@employee, '$.empaddr[' + @key + '].Country')" +
"set @pincode = JSON_VALUE(@employee, '$.empaddr[' + @key + '].Pin_Code')" +
"set @landmark = JSON_VALUE(@employee, '$.empaddr[' + @key + '].Landmark')" +
"insert into EmployeeAddresses(Address_type, House_number, Street, City, State, Country, Pin_Code, Landmark, Empid) values(@addresstype, @housenumber, @street, @city, @state, @country, @pincode, @landmark, @id)" +
"Fetch Next from curs into @key,@value,@type;" +
            "end;" +
            "close curs";
            CreateStoredProcedure("EditEmployee", p => new
            {
                id=p.Int(),
                employee = p.String(),
            }, body: edit);
        }
        
        public override void Down()
        {
        }
    }
}
