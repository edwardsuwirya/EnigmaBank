using Common.Enums;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Contexts;

internal class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts", "enigma")
            .HasIndex(a => a.AccountNumber)
            .IsUnique()
            .HasDatabaseName("IX_Accounts_AccountNumber");

        builder.Property(a => a.Type)
            .HasConversion(new EnumToStringConverter<AccountType>());
    }
}

internal class AccountHolderConfig : IEntityTypeConfiguration<AccountHolder>
{
    public void Configure(EntityTypeBuilder<AccountHolder> builder)
    {
        builder.ToTable("AccountHolders", "enigma");
    }
}

internal class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions", "enigma")
            .Property(t => t.Type)
            .HasConversion(new EnumToStringConverter<TransactionType>());
    }
}