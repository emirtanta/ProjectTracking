using ProjectTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectTracking.Models.Context
{
    public class ProjectTrackingDbContext:DbContext
    {
        public ProjectTrackingDbContext():base("ProjectTrackingDB")
        {
            
        }

        public DbSet<PersonnelInformation> PersonnelInformations { get; set; }
        public DbSet<PersonelProject> PersonelProjects { get; set; }
    }
}