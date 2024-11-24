using Microsoft.EntityFrameworkCore;
using LeadManagementAPI.Models;

namespace LeadManagementAPI.Data
{
    public class LeadContext:DbContext
    {
        public LeadContext(DbContextOptions<LeadContext> options) : base(options) { }
        public DbSet<Lead> Leads { get; set; }
    }
}