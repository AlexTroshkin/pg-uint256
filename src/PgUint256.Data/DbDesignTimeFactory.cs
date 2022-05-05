using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PgUint256.Data;

public class DbDesignTimeFactory : IDesignTimeDbContextFactory<Db>
{
    public Db CreateDbContext(String[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseNpgsql(
                connectionString: "Server=localhost;Port=5432;Database=PgUint256;User ID=postgres;Password=password;");

        return new Db(optionsBuilder.Options);
    }
}
