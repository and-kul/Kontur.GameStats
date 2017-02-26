using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Kontur.GameStats.Server.Models;
using SQLite.CodeFirst;

namespace Kontur.GameStats.Server.Database
{
    public class GameStatsDbContext : DbContext
    {
        public GameStatsDbContext()
            : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasRequired(p => p.Statistics)
                .WithRequiredPrincipal(ps => ps.Player)
                .WillCascadeOnDelete(true);
            
            modelBuilder.Entity<Models.Server>()
                .HasRequired(s => s.Statistics)
                .WithRequiredPrincipal(ss => ss.Server)
                .WillCascadeOnDelete(true);



            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();

            var sqliteDbInitializer = new SqliteDropCreateDatabaseWhenModelChanges<GameStatsDbContext>(modelBuilder);
            System.Data.Entity.Database.SetInitializer(sqliteDbInitializer);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Models.Server> Servers { get; set; }
        public DbSet<GameMode> GameModes { get; set; }
        public DbSet<Map> Maps { get; set; }

        public DbSet<PlayerStatistics> PlayersStatistics { get; set; }
        public DbSet<PlayerGameMode> PlayersGameModes { get; set; }
        public DbSet<PlayerServer> PlayersServers { get; set; }
   
        public DbSet<ServerStatistics> ServersStatistics { get; set; }
        public DbSet<ServerGameMode> ServersGameModes { get; set; }
        public DbSet<ServerMap> ServersMaps { get; set; }
        
        public DbSet<Match> Matches { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}