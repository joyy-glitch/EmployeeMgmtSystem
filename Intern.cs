using System;
using System.Collections.Generic; 



namespace EmployeeMgmtSystem.Models
{


    public class Intern : Employee
    { public const int allowancepermonth = 4000;
	  public Employee supervisor { get; set; }
      public int allowance { get; set; }
      public int month_duration { get; set; }



        public Intern(string name, int age, Department department, bool isactive, Employee supervisor, int month_duration) : base(name, age, department, isactive)
        {
            this.supervisor = supervisor;
            this.month_duration = month_duration;
            this.allowance = allowancepermonth * month_duration;
        }


        public override void DisplayInfo()
        {
            Console.WriteLine("Employee Details");
            Console.WriteLine("---------------------------------");

            Console.WriteLine($"ID: {this.emp_id}\n" +
                              $"Name: {this.name}\n" +
                              $"Age: {this.age}\n" +
                              $"Employee Type: {GetType().Name}\n" +
                              $"Status(Is active?): {this.isactive}\n" +
                              $"Department: {DepartmentData.Info[department].DisplayName}\n" +
                              $"Supervisor: {supervisor?.name}\n" +
                              $"Role: {RoleData.Info[role].DisplayName}\n" +
                              $"Allowance: ${this.allowance} \n");
        }


        public override decimal CalculatePay()
        {
                return allowancepermonth * month_duration;

            }















        }

    }

