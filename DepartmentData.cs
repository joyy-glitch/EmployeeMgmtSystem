using System;
using System.Collections.Generic;



namespace EmployeeMgmtSystem.Models
{

    public class DepartmentInfo
    {
        public string DisplayName { get; set; }

        public DepartmentInfo(string displayName)
        {
            DisplayName = displayName;
        }
    }
    public static class DepartmentData
    {
        public static Dictionary<Department, DepartmentInfo> Info
            = new Dictionary<Department, DepartmentInfo>
        {
        { Department.HumanResource, new DepartmentInfo("Human Resources") },
        { Department.UIUXDesign, new DepartmentInfo("UI/UX Design") },
        { Department.Finance, new DepartmentInfo("Finance") },
        { Department.SoftwareEngineering, new DepartmentInfo("Software Engineering") },
        { Department.ProductManagement, new DepartmentInfo("Product Management") },
        { Department.Cybersecurity, new DepartmentInfo("Cybersecurity") },
        { Department.Facility, new DepartmentInfo("Facility") },
        { Department.DevOps, new DepartmentInfo("DevOps") },
        { Department.Operations, new DepartmentInfo("Operations") },


        };
    }
}