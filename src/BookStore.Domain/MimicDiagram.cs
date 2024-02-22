using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace WebNet.MimicDiagrams
{
    public class MimicDiagram : FullAuditedAggregateRoot<int>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual bool Active { get; set; }

        [NotNull]
        public virtual string MimicDiagramName { get; set; }

        [NotNull]
        public virtual string MimicDiagramDescription { get; set; }

        [NotNull]
        public virtual string MimicDiagramXML { get; set; }

        public virtual bool? MimicDiagramAuthorization { get; set; }

        public MimicDiagram()
        {

        }

        public MimicDiagram(bool active, string mimicDiagramName, string mimicDiagramDescription, string mimicDiagramXML, bool mimicDiagramAuthorization)
        {

            Check.NotNull(mimicDiagramName, nameof(mimicDiagramName));
            //Check.Length(mimicDiagramName, nameof(mimicDiagramName), MimicDiagramConsts.MimicDiagramNameMaxLength, 0);
            Check.NotNull(mimicDiagramDescription, nameof(mimicDiagramDescription));
            //Check.Length(mimicDiagramDescription, nameof(mimicDiagramDescription), MimicDiagramConsts.MimicDiagramDescriptionMaxLength, 0);
            Check.NotNull(mimicDiagramXML, nameof(mimicDiagramXML));
            Active = active;
            MimicDiagramName = mimicDiagramName;
            MimicDiagramDescription = mimicDiagramDescription;
            MimicDiagramXML = mimicDiagramXML;
            MimicDiagramAuthorization = mimicDiagramAuthorization;
        }

    }
}