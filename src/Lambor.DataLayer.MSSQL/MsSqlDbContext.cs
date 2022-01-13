using Lambor.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace Lambor.DataLayer.MSSQL
{
    public class MsSqlDbContext : ApplicationDbContext
    {
        public MsSqlDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}