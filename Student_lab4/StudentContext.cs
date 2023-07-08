using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using Student_lab4;
using Student_lab4.Configuration;
using Student_lab4.Controllers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Student_lab4
{
    public class StudentContext : IdentityDbContext<UserEntity,
UserRole, int>
    {
        public StudentContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Student { get; set; } = null!;
    }

}
