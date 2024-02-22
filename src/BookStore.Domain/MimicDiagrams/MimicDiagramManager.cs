using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace BookStore.MimicDiagrams
{
    public class MimicDiagramManager : DomainService
    {
        private readonly IMimicDiagramRepository _mimicDiagramRepository;

        public MimicDiagramManager(IMimicDiagramRepository mimicDiagramRepository)
        {
            _mimicDiagramRepository = mimicDiagramRepository;
        }

        public async Task<MimicDiagram> CreateAsync(
        bool active, string mimicDiagramName, string mimicDiagramDescription, string mimicDiagramXML, bool mimicDiagramAuthorization)
        {
            Check.NotNullOrWhiteSpace(mimicDiagramName, nameof(mimicDiagramName));
            //Check.Length(mimicDiagramName, nameof(mimicDiagramName), MimicDiagramConsts.MimicDiagramNameMaxLength);
            Check.NotNullOrWhiteSpace(mimicDiagramDescription, nameof(mimicDiagramDescription));
            //Check.Length(mimicDiagramDescription, nameof(mimicDiagramDescription), MimicDiagramConsts.MimicDiagramDescriptionMaxLength);
            Check.NotNullOrWhiteSpace(mimicDiagramXML, nameof(mimicDiagramXML));

            var mimicDiagram = new MimicDiagram(

             active, mimicDiagramName, mimicDiagramDescription, mimicDiagramXML, mimicDiagramAuthorization
             );

            return await _mimicDiagramRepository.InsertAsync(mimicDiagram);
        }

        public async Task<MimicDiagram> UpdateAsync(
            int id,
            bool active, string mimicDiagramName, string mimicDiagramDescription, string mimicDiagramXML, bool mimicDiagramAuthorization, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(mimicDiagramName, nameof(mimicDiagramName));
            //Check.Length(mimicDiagramName, nameof(mimicDiagramName), MimicDiagramConsts.MimicDiagramNameMaxLength);
            Check.NotNullOrWhiteSpace(mimicDiagramDescription, nameof(mimicDiagramDescription));
            //Check.Length(mimicDiagramDescription, nameof(mimicDiagramDescription), MimicDiagramConsts.MimicDiagramDescriptionMaxLength);
            Check.NotNullOrWhiteSpace(mimicDiagramXML, nameof(mimicDiagramXML));

            var mimicDiagram = await _mimicDiagramRepository.GetAsync(id);

            mimicDiagram.Active = active;
            mimicDiagram.MimicDiagramName = mimicDiagramName;
            mimicDiagram.MimicDiagramDescription = mimicDiagramDescription;
            mimicDiagram.MimicDiagramXML = mimicDiagramXML;
            mimicDiagram.MimicDiagramAuthorization = mimicDiagramAuthorization;

            mimicDiagram.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _mimicDiagramRepository.UpdateAsync(mimicDiagram);
        }

    }
}