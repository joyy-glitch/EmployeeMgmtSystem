using EmployeeMgmtSystem.Models;
using System;
using System.Collections.Generic;


namespace EmployeeMgmtSystem.Models
{
    public class RoleInfo
    {
        public string DisplayName { get; set; }
        public decimal BaseSalary { get; set; }
    }
    
    public static class RoleData
    {
        public static readonly Dictionary<Role, RoleInfo> Info = new Dictionary<Role, RoleInfo>() 
    {
        { Role.LeadSoftwareEngineer, new RoleInfo { DisplayName = "Lead Software Engineer", BaseSalary = 15000m } },
        { Role.JuniorSoftwareEngineer, new RoleInfo { DisplayName = "Junior Software Engineer", BaseSalary = 6000m } },
        { Role.WebDeveloper, new RoleInfo { DisplayName = "Web Developer", BaseSalary = 8000m } },
        { Role.HumanResourceLead, new RoleInfo { DisplayName = "Human Resource Lead", BaseSalary = 12000m } },
        { Role.HumanResourceTeamMember, new RoleInfo { DisplayName = "Human Resource Team Member", BaseSalary = 5000m } },
        { Role.ManagingDirector, new RoleInfo { DisplayName = "Managing Director", BaseSalary = 30000m } }
    };
    }


}















