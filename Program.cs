using EmployeeMgmtSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json; 
using System.Text.Json.Serialization;  



namespace EmployeeMgmtSystem
{

    public class Program
    {

       
        static void Main(string[] args) 
        {
            List<Employee> employees = Employee.LoadEmployees();
            int option = 0;
            while (option != 6)
            {

                Console.WriteLine("   EMPLOYEE MANAGEMENT SYSTEM <3  "); 
                Console.WriteLine(Path.GetFullPath("employees.json"));
                Console.WriteLine("..................................................");  
                Console.WriteLine("Pick an option (1 - 6):");
                Console.WriteLine(" 1. Add Employee\n" + " 2. View Employees\n" + " 3. Search Employee\n" +
                                  " 4. Update Employee\n" + " 5. Delete Employee\n" + " 6. Exit");

                string decision = Console.ReadLine();
                int.TryParse(decision, out option);

                try
                {
                    switch (option)
                    {
                        case 1:
                            AddEmployee( employees);
                            break;
                        case 2:
                            ViewEmployee( employees);
                            break;
                        case 3:
                            SearchEmployee( employees);
                            break;
                        case 4:
                            UpdateEmployee(employees);
                            break;
                        case 5:
                            DeleteEmployee( employees);
                            break;
                        case 6:
                            Console.WriteLine("Exiting the system. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please select a number from 1 to 6.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

            }
        }

        
        static void AddEmployee(List<Employee> employees)
        {
            Console.WriteLine("\nSelect Employee Type:");
            Console.WriteLine("1. Full Time");
            Console.WriteLine("2. Contract");
            Console.WriteLine("3. National Service");
            Console.WriteLine("4. Intern");


            int typeChoice;
            while (true)
            {
     
                string input = Console.ReadLine();

                if (!int.TryParse(input, out typeChoice) || typeChoice < 1 || typeChoice > 4)
                {
                    Console.WriteLine("Invalid type selection. Please enter a number from 1 to 4.");
                    continue;
                }

                break;
            }

            Console.Write("Enter Name: ");
            string name;

            while (true)
            {
                 name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    continue;
                }

                break;
            }         

            Console.Write("Enter Age:");
            bool validAge = false;
            int age = 0;
            while(!validAge)
            {
                string ageInput = Console.ReadLine();
                if (!int.TryParse(ageInput, out age) || age <= 0)
                {
                    Console.WriteLine("Invalid age input. Please enter a valid number.");
                    continue;
                }
                validAge = true;
            }

            
            Console.WriteLine("Select a Department:"); 
            Department[] departments = Enum.GetValues(typeof(Department))
                                .Cast<Department>() 
                                .ToArray();

            for (int i = 0; i < departments.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {DepartmentData.Info[departments[i]].DisplayName}");
            }

            Console.Write("Enter choice number: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > departments.Length) 
            {
                Console.WriteLine("Invalid department choice."); 
                return;
            }

            Department selectedDepartment = departments[choice - 1]; 

            //Console.WriteLine("Is the employee active?");
            // Replace the current IsActive parsing block with this inline validation (no extra function)
            bool isActive;
            while (true) 
            {
                Console.Write("Is the employee active? (y/n): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter 'y' or 'n'.");
                    continue;
                }

                input = input.Trim().ToLowerInvariant();

                if (input == "y" || input == "yes")
                {
                    isActive = true;
                    break;
                }

                if (input == "n" || input == "no")
                {
                    isActive = false;
                    break;
                }

                // allow explicit "true"/"false" as well
                if (bool.TryParse(input, out bool parsed))
                {
                    isActive = parsed;
                    break;
                }

                Console.WriteLine("Invalid input. Enter 'y' or 'n' (or 'true'/'false').");
            }

            /* if (!bool.TryParse(Console.ReadLine(), out bool isActive))
            {
                isActive = false;
            }  */


            //full time. 
            if (typeChoice == 1)
            {
                Console.WriteLine("Select a role:");
                int count = 1;
                foreach (var kv in RoleData.Info)
                {
                    Console.WriteLine($"{count}. {kv.Value.DisplayName}");
                    count++;
                }

                int roleIndex = int.Parse(Console.ReadLine());

                if (roleIndex < 1 || roleIndex > RoleData.Info.Count)
                {
                    Console.WriteLine("Invalid selection.");
                    return;
                }

                Role selectedRole = RoleData.Info.ElementAt(roleIndex - 1).Key;

                FullTime emp = new FullTime(name,age, selectedDepartment, selectedRole, isActive);
                employees.Add(emp);

                Console.WriteLine("Employee added succesfully!");
                Console.WriteLine($"Employee ID is {emp.emp_id}");

            }

            // Contract.
            else if (typeChoice == 2)
            {
                Console.Write("Enter Hourly Rate: ");
                decimal rate = decimal.Parse(Console.ReadLine());

                Console.Write("Enter Hours Worked: ");
                int hours = int.Parse(Console.ReadLine());

                Console.WriteLine("Select a role:");
                int countt = 1;
                foreach (var kv in RoleData.Info)
                {
                    Console.WriteLine($"{countt}. {kv.Value.DisplayName}");
                    countt++;
                }

                int roleIndex = int.Parse(Console.ReadLine());

                if (roleIndex < 1 || roleIndex > RoleData.Info.Count)
                {
                    Console.WriteLine("Invalid selection.");
                    return;
                } 

                Role selectedRole2 = RoleData.Info.ElementAt(roleIndex - 1).Key;

                Contract emp2 = new Contract(name, age, selectedDepartment, isActive, selectedRole2, rate, hours);
                employees.Add(emp2);
                //Employee.SaveEmployees(employees);

                Console.WriteLine("Employee added succesfully!");
                Console.WriteLine($"Employee ID is {emp2.emp_id}");
            }





            //NSS
            else if (typeChoice == 3)
            {

                Console.Write("Enter Month Duration: ");
                string monthsspent = Console.ReadLine();
                int.TryParse(monthsspent, out int months_spent);

                NSS emp3 = new NSS(name, age, selectedDepartment, isActive, months_spent);
                employees.Add(emp3);
               // Employee.SaveEmployees(employees);

                Console.WriteLine("Employee added succesfully!");
                Console.WriteLine($"Employee ID is {emp3.emp_id}");
            }



            //Intern
            else if(typeChoice == 4)
            {
                Console.WriteLine("Select a supervisor:");

                var supervisors = employees
                    .Where(e => e is FullTime)
                    .ToList();

                for (int i = 0; i < supervisors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {supervisors[i].name}");
                }

                if (!int.TryParse(Console.ReadLine(), out int choice3) ||
                    choice3 < 1 || choice3 > supervisors.Count)
                {
                    Console.WriteLine("Invalid selection.");
                    return;
                }

                Employee supervisor = supervisors[choice3 - 1];
               // Console.Write("Enter Supervisor name: ");
               // string supervisorname = Console.ReadLine(); 

                Console.Write("Enter Month Duration: ");
                string numofmonths = Console.ReadLine();
                int.TryParse(numofmonths, out int month_num);

                Intern emp4 = new Intern(name, age, selectedDepartment, isActive, supervisor, month_num);
                employees.Add(emp4);
                //Employee.SaveEmployees(employees);

                Console.WriteLine("Employee added succesfully!");
                Console.WriteLine($"Employee ID is {emp4.emp_id}");

            }
            else
            {
                Console.WriteLine("Invalid employee type");
            }

           Employee.SaveEmployees(employees);

        }

        

        //option 2.

        static void ViewEmployee(List<Employee> employees)
        {
            Console.WriteLine("1. View All Employees \n" + "2.View Single Employee by ID");
            

            Console.Write("Enter choice: ");
            int viewChoice = int.Parse(Console.ReadLine());

            switch (viewChoice)
            {
                case 1:
                    if (employees.Count == 0)
                    {
                        Console.WriteLine("No employees found.");
                    }
                    else
                    {
                        foreach (Employee e in employees)
                        {
                            e.DisplayInfo();
                            Console.WriteLine("----------------");
                        }
                    }
                    break;

                case 2:
                    Console.Write("Enter Employee ID: ");
                    string id = Console.ReadLine();

                    Employee foundEmployee = null;

                    foreach (Employee e in employees)
                    {
                        if (e.emp_id == id) 
                        {
                            foundEmployee = e;
                            break;
                        }
                    }

                    if (foundEmployee != null)
                    {
                        foundEmployee.DisplayInfo();
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }

                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        }



        // option 3.
        public static void SearchEmployee(List<Employee> employees) {
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees in the system.");
                return;
                
            }

            Console.Write("Enter Employee ID to search: ");
            string id = Console.ReadLine();

            Employee foundEmployee = null;

            foreach (Employee emp in employees)
            {
                if (emp.emp_id == id)
                {
                    foundEmployee = emp;
                    break;
                }
            }

            if (foundEmployee != null)
            {
                Console.WriteLine("Employee found:");
                foundEmployee.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }

            

        }

        // option 4
        public static void UpdateEmployee(List<Employee> employees) {

            /*Console.WriteLine("Enter Employee ID to update: ") ;
            string id_to_update = Console.ReadLine();

            Employee employeetoupdate = null; */

     
                    if (employees.Count == 0)
                    {
                        Console.WriteLine("No employees available.");
                return;
                    }

                    Console.Write("Enter Employee ID to update: ");
                    string id_to_update = Console.ReadLine();

                    Employee employeeToUpdate = employees.Find(e => e.emp_id == id_to_update);

                    if (employeeToUpdate == null)
                    {
                        Console.WriteLine("Employee not found.");
                        return;
                    }

                    Console.WriteLine("\nEmployee found:");
                    employeeToUpdate.DisplayInfo(); 

                    Console.WriteLine("\nWhat would you like to update?");
                    Console.WriteLine("1. Name");
                    Console.WriteLine("2. Age");
                    Console.WriteLine("3. Department");
                    Console.WriteLine("4. Is Active");


                   if (employeeToUpdate is FullTime)
                   {
                        Console.WriteLine("5. Role");
                   }
                   if (employeeToUpdate is Contract)
                   {
                        Console.WriteLine("5. Role");
                        Console.WriteLine(" 6. Hourly Rate");
                        Console.WriteLine(" 7. Hours Worked");
                   }
                   if (employeeToUpdate is NSS)
                   {
                        Console.WriteLine("5. Month Duration");
                   }

                   if (employeeToUpdate is Intern)
                    {
                        Console.WriteLine("5. Supervisor Name");
                        Console.WriteLine("6. Duration (months)");
                    }

                   
            
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter new name: ");
                            employeeToUpdate.name = Console.ReadLine();
                            break;

                        case 2:
                            Console.Write("Enter new age: ");
                            employeeToUpdate.age = int.Parse(Console.ReadLine());
                            break;

                        case 3:
                    Console.WriteLine("Select new department:");

                    Department[] departments = DepartmentData.Info.Keys.ToArray();

                    for (int i = 0; i < departments.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {DepartmentData.Info[departments[i]].DisplayName}");
                    }

                    Console.Write("Enter choice number: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice1) ||
                        choice1 < 1 || choice1 > departments.Length)
                    {
                        Console.WriteLine("Invalid department.");
                        break;
                    }

                    employeeToUpdate.department = departments[choice1 - 1];
                    break; 

                        case 4:
                            Console.Write("Is Active (true/false):");
                            employeeToUpdate.isactive = bool.Parse(Console.ReadLine());
                            break;

                        case 5:
                    // FullTimeEmployee role update
                    if (employeeToUpdate is FullTime fullTime)
                    {
                        /*Console.WriteLine("Select new role:");

                        foreach (var kv in RoleData.Info)
                            Console.WriteLine(kv.Value.DisplayName);

                        string roleInput = Console.ReadLine();

                        var roleKv = RoleData.Info
                            .FirstOrDefault(x => x.Value.DisplayName
                            .Equals(roleInput, StringComparison.OrdinalIgnoreCase));

                        if (!roleKv.Equals(default(KeyValuePair<Role, RoleInfo>)))
                        {
                            fullTime.role = roleKv.Key;
                        }
                        else
                        {
                            Console.WriteLine("Invalid role.");
                        } */

                        Console.WriteLine("Select new role:");

                        Role[] roles = RoleData.Info.Keys.ToArray();

                        for (int i = 0; i < roles.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {RoleData.Info[roles[i]].DisplayName}");
                        }

                        Console.Write("Enter choice number: ");

                        if (!int.TryParse(Console.ReadLine(), out int choicee) ||
                            choicee < 1 || choicee > roles.Length)
                        {
                            Console.WriteLine("Invalid role.");
                            return;
                        }

                        fullTime.role = roles[choicee - 1];

                        Console.WriteLine("Role updated successfully!");
                    }

                    // ContractEmployee role update
                    else if (employeeToUpdate is Contract contract) 
                    {
                        Console.WriteLine("Select new role:");

                        Role[] roles = RoleData.Info.Keys.ToArray();

                        for (int i = 0; i < roles.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {RoleData.Info[roles[i]].DisplayName}");
                        }

                        Console.Write("Enter choice number: ");

                        if (!int.TryParse(Console.ReadLine(), out int choice2) ||
                            choice2 < 1 || choice2 > roles.Length)
                        {
                            Console.WriteLine("Invalid role.");
                            return;
                        }

                        contract.role = roles[choice2 - 1];

                        Console.WriteLine("Role updated successfully!");
                    }

                    // NSS duration update
                    else if (employeeToUpdate is NSS nss)
                    {
                        Console.Write("Enter new month duration: ");
                        nss.monthly_duration = int.Parse(Console.ReadLine());
                    }

                    else if (employeeToUpdate is Intern intern)
                    {
                        //Console.Write("Enter new supervisor name: ");
                        //intern.supervisor = Console.ReadLine();
                        Console.WriteLine("Select a new supervisor:");

                        var supervisors = employees
                            .Where(e => e is FullTime)
                            .ToList();

                        for (int i = 0; i < supervisors.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {supervisors[i].name}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int choice4) ||
                            choice4 < 1 || choice4 > supervisors.Count)
                        {
                            Console.WriteLine("Invalid selection.");
                            return;
                        }

                        Employee supervisor = supervisors[choice4 - 1];
                    }
                    else
                    {
                        Console.WriteLine("Option not valid for this employee type.");
                    }


                    break;

                    case 6:
                        if (employeeToUpdate is Contract ce2)
                        {
                        Console.Write("Enter new hourly rate: ");
                        ce2.hourlyRate = decimal.Parse(Console.ReadLine());
                    }
                    else if (employeeToUpdate is Intern intern2)
                    {
                        Console.Write("Enter duration in months: ");
                        intern2.month_duration = int.Parse(Console.ReadLine());
                    }

                    else
                    {
                        Console.WriteLine("Invalid option for this employee type.");
                    }
                    break;

                case 7:
                    if (employeeToUpdate is Contract ce3)
                    {
                        Console.Write("Enter hours worked: ");
                        ce3.hoursWorked = int.Parse(Console.ReadLine());
                    }
                    
                    else
                    {
                        Console.WriteLine("Invalid option for this employee type.");
                    }
                    break;
                    }

                    Console.WriteLine("Employee updated successfully.");

                   Employee.SaveEmployees(employees);

        }


                // when they enter the persons ID, they should be able to update any of the details of the employee except for the ID.
                // then you'll have to check if the employee exists, if they do, you can ask them which detail they want to update and then update it accordingly.
                //the details that can be updated are according to the type of employee, for example, if it's a full time employee,  
                //they can update their role, but if it's an intern, they can't update their role but they can update their supervisor name and month duration.
                //there are also general details that can be updated for all employees such as name, age, department and isactive status.


            




        // option 5
        public static void DeleteEmployee(List<Employee> employees) {
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees to delete.");
                return;
            }

            Console.WriteLine("1. Delete All Employees");
            Console.WriteLine("2. Delete Employee by ID");

            Console.Write("Enter choice: ");
            int deleteChoice = int.Parse(Console.ReadLine());

            switch (deleteChoice)
            {
                case 1:
                    Console.Write("Are you sure you want to delete ALL employees? (y/n): ");
                    string confirm = Console.ReadLine();

                    if (confirm.ToLower() == "y")
                    {
                        employees.Clear();
                        Console.WriteLine("All employees deleted.");
                    }
                    else
                    {
                        Console.WriteLine("Delete cancelled.");
                    }

                    break;

                case 2:
                    Console.Write("Enter Employee ID to delete: "); 
                    string id = Console.ReadLine();

                    Employee employeeToDelete = null;

                    foreach (Employee emp in employees)
                    {
                        if (emp.emp_id == id)
                        {
                            employeeToDelete = emp;
                            break;
                        }
                    }

                    if (employeeToDelete != null)
                    {
                        employees.Remove(employeeToDelete);
                        Console.WriteLine("Employee deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                       // return;
                    }

                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            Employee.SaveEmployees(employees);

        }

        


    }

}

