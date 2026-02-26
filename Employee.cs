using EmployeeMgmtSystem.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization; 
using System.IO;




namespace EmployeeMgmtSystem.Models {

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(FullTime), "FullTime")]
    [JsonDerivedType(typeof(Intern), "Intern")]
    [JsonDerivedType(typeof(Contract), "Contract")]
	[JsonDerivedType(typeof(NSS), "Nss")]

    public abstract class Employee
	{
		//protected//
		public string name { get; set; }
		public int age { get; set; }
		public string emp_id { get; set; }
		public Role role { get; set; }
		public bool isactive { get; set; }
		public  Department department { get; set; }
		public  decimal salary { get; set; }
		

		public Employee()
		{
			name = string.Empty;
			age = 0;
			salary = 0;
			emp_id = string.Empty;
			isactive = false;	
		}


		public Employee(string name, int age, Department department, Role role, bool isactive)
		{
			this.name = name; 
			this.age = age;
			this.role = role;
			this.department = department;
			this.isactive = isactive;
			emp_id = "EMP-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        }

        public Employee(string name, int age, Department department, bool isactive)
        {
            this.name = name;
            this.age = age;
            this.department = department;
            this.isactive = isactive;
            emp_id = "EMP-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        }



        public virtual void DisplayInfo()
		{
			Console.WriteLine("Employee Details");
			Console.WriteLine("---------------------------------");
			Console.WriteLine($"ID: {this.emp_id}\n" +
						  $"Name: {this.name}\n" +
						  $"Age: {this.age}\n" +
						  $"Status(Is active?): {this.isactive}\n" +
						  $"Department: {DepartmentData.Info[department].DisplayName}" +
						  $"Role: {this.role}\n" +
						  $"Salary: ${this.salary}\n");

		}

	    public abstract decimal CalculatePay();

        public static void SaveEmployees(List<Employee> employees)
        {
            var options = new JsonSerializerOptions 
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve 
            };
            options.Converters.Add(new JsonStringEnumConverter());

            File.WriteAllText("employees.json",
                JsonSerializer.Serialize(employees, options));
        } 


        public static List<Employee> LoadEmployees()
        {
            if (!File.Exists("employees.json"))
                return new List<Employee>();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            options.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<List<Employee>>(
                File.ReadAllText("employees.json"),
                options
            ) ?? new List<Employee>();
        }


    }
}
