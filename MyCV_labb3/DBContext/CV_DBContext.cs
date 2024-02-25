using Microsoft.EntityFrameworkCore;
using MyCV_labb3.Model;

namespace MyCV_labb3.DBContext
{
    internal class CV_DBContext : DbContext
    {
        public DbSet<Project> Projects {  get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserModel> Users { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CVDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            //options.UseSqlServer("Server = tcp:gunjancv.database.windows.net, 1433; Initial Catalog = CVDatabase; Persist Security Info = False; User ID = gunjanadmin; Password =Gunjan@1911; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
        }
    }
}
