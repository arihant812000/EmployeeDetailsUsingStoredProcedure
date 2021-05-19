namespace ApiStoredprocedure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertEmployee : DbMigration
    {
        public override void Up()
        {
            String insert= "declare @firstName nvarchar(30),"+
"@lastname nvarchar(30),"+
"@age int,"+
"@dateofbirth nvarchar(30),"+
"@department nvarchar(30),"+
"@email nvarchar(30),"+
"@jobprofile nvarchar(30),"+
"@phonenumber nvarchar(30),"+
" @addresstype nvarchar(30),"+
"@housenumber nvarchar(30),"+
"@street nvarchar(20),"+
"@city nvarchar(30),"+
"@state nvarchar(30),"+
"@country nvarchar(30),"+
"@pincode int,"+
"@landmark nvarchar(30),"+
"@add nvarchar(max);"+
            "set @firstName = JSON_VALUE(@employee, '$.First_Name');"+
            "set @lastname = JSON_VALUE(@employee, '$.Last_Name');"+
            "set @dateofbirth = JSON_VALUE(@employee, '$.Date_of_Birth');"+
            "set @age = JSON_VALUE(@employee, '$.Age');"+
            "set @department = JSON_VALUE(@employee, '$.Department');"+
            "set @email = JSON_VALUE(@employee, '$.Email');"+
            "set @phonenumber = JSON_VALUE(@employee, '$.Phone_No');"+
            "set @jobprofile = JSON_VALUE(@employee, '$.Job_Role');"+
            "set @add = JSON_VALUE(@employee, '$.empaddr');"+
            "insert into employeeDetails(First_Name, Last_Name, Date_of_Birth, Age, Email, Phone_No, Department, Job_Role) values(@firstName, @lastname, @dateofbirth, @age, @email, @phonenumber, @department, @jobprofile);"+
           " declare @id int;"+
          "  set @id = SCOPE_IDENTITY()"+

"declare @key nvarchar(20),"+
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

            CreateStoredProcedure("AddEmployee", p => new
            {
                employee= p.String(),
            }, body: insert);
        }
        
        public override void Down()
        {
        }
    }
}
