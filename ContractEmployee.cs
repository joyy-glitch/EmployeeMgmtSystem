using System;
using System.Collections.Generic;


namespace EmployeeMgmtSystem.Models 
{ 


    public class Contract:Employee
	{   
		public decimal hourlyRate {  get; set; }
		public decimal hoursWorked { get; set; }


        public Contract(string name, int age, Department department, bool isactive, Role role, decimal hourlyRate, decimal hoursWorked) : base(name, age, department, role, isactive)
        {
            this.hourlyRate = hourlyRate;
            this.hoursWorked = hoursWorked;
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
                              $"Department: {DepartmentData.Info[department].DisplayName}\n"+
                              $"Role: {RoleData.Info[role].DisplayName}\n" +
                              $"Salary:${this.CalculatePay()}\n"+
                              $"Hours worked :{hoursWorked}" );
		} 

        public override decimal CalculatePay()
        {
            return hourlyRate*hoursWorked;    
        }









        }
	}






