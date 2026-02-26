using System;
using System.Collections.Generic;


namespace EmployeeMgmtSystem.Models
{

	public class NSS : Employee
	{
        public int monthly_duration { get; set; }
		public decimal monthlypay { get; set; }


        public NSS(string name, int age, Department department, bool isactive,int monthly_duration) : base(name, age, department, isactive)
        {
            
            this.monthly_duration = monthly_duration;
            this.monthlypay = 1000m;

        }


        public override void DisplayInfo()
        {
            Console.WriteLine("  Employee Details  ");
            Console.WriteLine("---------------------------------");

            Console.WriteLine($"ID: {this.emp_id}\n" +
                              $"Name: {this.name}\n" +
                              $"Age: {this.age}\n" +
                              $"Employee Type: {GetType().Name}\n" +
                              $"Status(Is active?): {this.isactive}\n" +
                              $"Department: {DepartmentData.Info[department].DisplayName}\n" +
                              $"Salary: ${this.CalculatePay()}");
		}

        public override decimal CalculatePay()
        {   return monthlypay * monthly_duration;

        }

    }

}
