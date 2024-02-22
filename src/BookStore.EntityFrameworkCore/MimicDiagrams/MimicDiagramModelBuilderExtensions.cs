using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace BookStore.MimicDiagrams
{
    public static class MimicDiagramModelBuilderExtensions
    {
        public static void ConfigureMimicDiagram([NotNull] this ModelBuilder builder)
        {

            builder.Entity<MimicDiagram>(b =>
            {
                b.ToTable("MimicDiagrams");
                b.ConfigureByConvention();
                b.Property(x => x.TenantId).HasColumnName(nameof(MimicDiagram.TenantId));
                b.Property(x => x.Active).HasColumnName(nameof(MimicDiagram.Active)).HasDefaultValue(true);
                b.Property(x => x.MimicDiagramName).HasColumnName(nameof(MimicDiagram.MimicDiagramName)).IsRequired().HasMaxLength(MimicDiagramConsts.MimicDiagramNameMaxLength);
                b.Property(x => x.MimicDiagramDescription).HasColumnName(nameof(MimicDiagram.MimicDiagramDescription)).IsRequired().HasMaxLength(MimicDiagramConsts.MimicDiagramDescriptionMaxLength);
                b.Property(x => x.MimicDiagramXML).HasColumnName(nameof(MimicDiagram.MimicDiagramXML)).IsRequired();
                b.Property(x => x.MimicDiagramAuthorization).HasColumnName(nameof(MimicDiagram.MimicDiagramAuthorization));
                b.Property(x => x.CreationTime).HasDefaultValueSql("getdate()");
            });
        }
    }
}
