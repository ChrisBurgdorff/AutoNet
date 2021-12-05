using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NFTDemo.Models
{
    public partial class AutoNetDemoDBContext : DbContext
    {
        public AutoNetDemoDBContext()
        {
        }

        public AutoNetDemoDBContext(DbContextOptions<AutoNetDemoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminLevel> AdminLevels { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Nft> Nfts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<SpMintNft> MintNft { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdminLevel>(entity =>
            {
                entity.ToTable("Admin_Levels");

                entity.Property(e => e.AdminLevelId).HasColumnName("Admin_Level_ID");

                entity.Property(e => e.AdminLevelName)
                    .HasMaxLength(1000)
                    .HasColumnName("Admin_Level_Name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Created");

                entity.Property(e => e.DateDisabled)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Disabled");

                entity.Property(e => e.Email).HasMaxLength(1000);

                entity.Property(e => e.FullName)
                    .HasMaxLength(1000)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(128)
                    .HasColumnName("Password_Hash")
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(128)
                    .HasColumnName("Password_Salt")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Created");

                entity.Property(e => e.DateDisabled)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Disabled");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(1000)
                    .HasColumnName("Group_Name");
            });

            modelBuilder.Entity<Nft>(entity =>
            {
                entity.ToTable("NFTs");

                entity.Property(e => e.NftId).HasColumnName("NFT_ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Created");

                entity.Property(e => e.DateDisabled)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Disabled");

                entity.Property(e => e.IpfsUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("IPFS_URL");

                entity.Property(e => e.WalletId).HasColumnName("Wallet_ID");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Nfts)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("FK_NFTs_Wallets");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).HasColumnName("Transaction_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.NftId).HasColumnName("NFT_ID");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Transaction_Date");

                entity.Property(e => e.TransactionTypeId).HasColumnName("Transaction_Type_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Transactions_Customers");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Transactions_Groups");

                entity.HasOne(d => d.Nft)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.NftId)
                    .HasConstraintName("FK_Transactions_NFTs");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .HasConstraintName("FK_Transactions_Transaction_Types");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Transactions_Users");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("Transaction_Types");

                entity.Property(e => e.TransactionTypeId).HasColumnName("Transaction_Type_ID");

                entity.Property(e => e.TransactionTypeName)
                    .HasMaxLength(1000)
                    .HasColumnName("Transaction_Type_Name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.AdminLevelId).HasColumnName("Admin_Level_ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Created");

                entity.Property(e => e.DateDisabled)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Disabled");

                entity.Property(e => e.Email).HasMaxLength(1000);

                entity.Property(e => e.FullName)
                    .HasMaxLength(1000)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(128)
                    .HasColumnName("Password_Hash")
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(128)
                    .HasColumnName("Password_Salt")
                    .IsFixedLength(true);

                entity.HasOne(d => d.AdminLevel)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AdminLevelId)
                    .HasConstraintName("FK_Users_Admin_Levels");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Users_Groups");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.WalletId).HasColumnName("Wallet_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.WalletAddress)
                    .HasMaxLength(40)
                    .HasColumnName("Wallet_Address");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Wallets_Customers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
