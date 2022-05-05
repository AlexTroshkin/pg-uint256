using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace PgUint256.Data.Types;

public class Account
{
    public Int64      Id      { get; set; }
    public String     Address { get; set; } = String.Empty;
    public BigInteger Amount  { get; set; } 
}

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Account> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Address)
            .HasMaxLength(42);

        //----------------------------------------------------------------
        // 78 * 2 = uint512 to avoid overflow
        //----------------------------------------------------------------

        builder
            .Property(x => x.Amount)
            .HasPrecision(78 * 2, 0);
    }
}