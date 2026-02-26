using EmployeeMgmtSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;


namespace EmployeeMgmtSystem.Models
{
	public class FullTime : Employee
	{    
        private const decimal bonusRate = 0.10m;

        public static readonly Dictionary<Role, decimal> baseSalarymap = new Dictionary<Role, decimal>()
        {   { Role.LeadSoftwareEngineer, 15000m },  
            { Role.JuniorSoftwareEngineer, 6000m }, 
            { Role.WebDeveloper, 8000m },
            { Role.HumanResourceLead, 12000m },
            { Role.HumanResourceTeamMember, 5000m },
            { Role.ManagingDirector, 25000m }
             
        };


		public FullTime(string name, int age,Department department, Role role, bool isactive) : base(name, age, department, role, isactive)
        { 
            this.salary = baseSalarymap[role];
            this.role = role;
        }

        public override decimal CalculatePay()
        {
            if (!baseSalarymap.ContainsKey(this.role)) return 0m;
            return baseSalarymap[this.role] + (baseSalarymap[this.role] * bonusRate); 

        }


		public override void DisplayInfo()
		{
            Console.WriteLine(" Employee Details");
            Console.WriteLine("---------------------------------");

            Console.WriteLine($"ID: {this.emp_id}\n" +
                              $"Name: {this.name}\n" +
                              $"Age: {this.age}\n" +
                              $"Employee Type: {GetType().Name}\n" +
                              $"Status(Is active?): {this.isactive}\n" +
                              $"Department: {DepartmentData.Info[department].DisplayName}\n" +
                              $"Role: {RoleData.Info[role].DisplayName}\n" +
                              $"Salary: ${this.CalculatePay()}");
		}

    }
}
