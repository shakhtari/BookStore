﻿using BookStore.Books;
using BookStore.DerivedValueDetails;
using BookStore.DerivedValues;
using BookStore.Formulas;
using BookStore.MimicDiagrams;
using BookStore.MimicProfiles;
using BookStore.Publications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace BookStore.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BookStoreDbContext :
    AbpDbContext<BookStoreDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    public DbSet<Book> Books { get; set; }
    public DbSet<Publication> Publications { get; set; }
    public DbSet<MimicProfile> MimicProfiles { get; set; }
    public DbSet<MimicDiagram> MimicDiagrams { get; set; }
    public DbSet<Formula> Formulas { get; set; }
    public DbSet<DerivedValue> DerivedValues { get; set; }
    public DbSet<DerivedValueDetail> DerivedValuesDetail { get; set; }
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(BookStoreConsts.DbTablePrefix + "YourEntities", BookStoreConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        builder.Entity<Book>(b =>
        {
            b.ToTable("Books");
        });
        builder.Entity<Publication>(b => { b.ToTable("Publications"); });
        builder.Entity<MimicProfile>(b => { b.ToTable("tbl_EM_MimicProfile"); });
        builder.Entity<MimicDiagram>(b =>
        {
            b.ToTable("tbl_EM_MimicDiagram");
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(MimicDiagram.TenantId));
            b.Property(x => x.Active).HasColumnName(nameof(MimicDiagram.Active)).HasDefaultValue(true);
            b.Property(x => x.MimicDiagramName).HasColumnName(nameof(MimicDiagram.MimicDiagramName)).IsRequired();
            b.Property(x => x.MimicDiagramDescription).HasColumnName(nameof(MimicDiagram.MimicDiagramDescription)).IsRequired();
            b.Property(x => x.MimicDiagramXML).HasColumnName(nameof(MimicDiagram.MimicDiagramXML)).IsRequired();
            b.Property(x => x.MimicDiagramAuthorization).HasColumnName(nameof(MimicDiagram.MimicDiagramAuthorization));
            b.Property(x => x.CreationTime).HasDefaultValueSql("getdate()");
        });
        builder.Entity<Formula>(b => { b.ToTable("tbl_EM_Formulas").HasKey(e => e.Id); b.Property(e => e.Id).ValueGeneratedOnAdd(); });
        builder.Entity<DerivedValue>(b => { b.ToTable("tbl_EM_DerivedValues"); });
        builder.Entity<DerivedValueDetail>(b => { b.ToTable("tbl_EM_DerivedValueDetails"); });
    }
    
}
