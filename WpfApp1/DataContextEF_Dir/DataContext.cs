using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WpfApp1.DataContextEF_Dir
{
    public partial class DataContext : DbContext
    {
        public DataContext() : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Milo\source\repos\WpfApp1\WpfApp1\LocalDataBaseDir\Database.mdf;Integrated Security=True")
        {
        }

        public virtual DbSet<Voiture> Voiture { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
