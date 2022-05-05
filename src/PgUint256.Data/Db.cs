using Microsoft.EntityFrameworkCore;
using PgUint256.Data.Types;
using System.Numerics;

namespace PgUint256.Data;

public class Db : DbContext
{
    public Db()
    {
        ChangeTracker.QueryTrackingBehavior    = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public Db(DbContextOptions<Db> options) : base(options)
    {        
        ChangeTracker.QueryTrackingBehavior    = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(Db).Assembly);

        builder
            .Entity<Account>()
            .HasData(
                new Account
                {
                    Id      = 1,
                    Address = "0xc4e7b02446f87386faf9b99e6d2f828f64c3fee7",
                    Amount  = BigInteger.Parse("115792089237316195423570985008687907853269984665640564039457584007913129639935")
                },
                new Account
                {
                    Id      = 2,
                    Address = "0xFbdDaDD80fe7bda00B901FbAf73803F2238Ae655",
                    Amount  = BigInteger.Parse("6116130967035491411282519902102901479169861")
                },
                new Account
                {
                    Id      = 3,
                    Address = "0x76e6e2E9Ca0ee9a2BB4148566791FD6F2fEeAc32",
                    Amount  = BigInteger.Parse("77600947487271298400590085802719009615471363693269818666652196571960497734")
                },
                new Account
                {
                    Id      = 4,
                    Address = "0x95aD61b0a150d79219dCF64E1E6Cc01f0B64C4cE",
                    Amount  = BigInteger.Parse("644635553935915926370")
                },
                new Account
                {
                    Id      = 5,
                    Address = "0xF33010d57c8ef2d1F8BfDd8B1f4bdB95325f2478",
                    Amount  = BigInteger.Parse("0")
                },
                new Account
                {
                    Id      = 6,
                    Address = "0x1D0EBe30B558c4B7752c8C47166123C8a924bCdf",
                    Amount  = BigInteger.Parse("90825904533199959")
                },
                new Account
                {
                    Id      = 7,
                    Address = "0xeE8B2bd9B9F51C6584c751172A0294f652FD790b",
                    Amount  = BigInteger.Parse("898561960255311071189251597992482498246314843902477377209272966919")
                },
                new Account
                {
                    Id      = 8,
                    Address = "0xEBd9D99A3982d547C5Bb4DB7E3b1F9F14b67Eb83",
                    Amount  = BigInteger.Parse("32799005422311601657892591803566040868654766679566")
                },
                new Account
                {
                    Id      = 9,
                    Address = "0x3D80b8A0929070038B913BC054aEd56961307145",
                    Amount  = BigInteger.Parse("199969812050627495071995560782982")
                });
    }

    public DbSet<Account> Accounts { get; set; }
}
