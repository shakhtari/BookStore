using AutoMapper;
using BookStore.Books;
using BookStore.MimicProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Formulas
{
    public class FormulaAppService : BookStoreAppService, IFormulaAppService
    {
        private readonly IRepository<Formula, int> _formulaRepository;
        public FormulaAppService(IRepository<Formula, int> formulaRepository)
        {
                _formulaRepository = formulaRepository;
        }
        public async Task<FormulaDto> CreateAsync(CreateUpdateFormulaDto formulaDto)
        {
            var newFormula = new Formula
            {
                FormulaName = formulaDto.FormulaName,
                Active = formulaDto.Active,
                FormulaType = formulaDto.FormulaType,
                FormulaMultiplier = formulaDto.FormulaMultiplier,
                FormulaInputMaximum = formulaDto.FormulaInputMaximum,
                FormulaInputMinimum = formulaDto.FormulaInputMinimum,
                FormulaOutputMaximim = formulaDto.FormulaOutputMaximim,
                FormulaOutputMinimum = formulaDto.FormulaOutputMinimum
            };

            newFormula = await Task.Run(() => _formulaRepository.InsertAsync(newFormula));

            return new FormulaDto
            {
                FormulaName = formulaDto.FormulaName,
                Active = formulaDto.Active,
                FormulaType = formulaDto.FormulaType,
                FormulaMultiplier = formulaDto.FormulaMultiplier,
                FormulaInputMaximum = formulaDto.FormulaInputMaximum,
                FormulaInputMinimum = formulaDto.FormulaInputMinimum,
                FormulaOutputMaximim = formulaDto.FormulaOutputMaximim,
                FormulaOutputMinimum = formulaDto.FormulaOutputMinimum
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _formulaRepository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<FormulaDto>> GetListAsync()
        {
            var items = await _formulaRepository.GetListAsync();
            return items
                .Select(item => new FormulaDto
                {
                    Id = item.Id,
                    Active = item.Active,
                    FormulaName=item.FormulaName,
                    FormulaType = item.FormulaType,
                    FormulaMultiplier = item.FormulaMultiplier,
                    FormulaInputMaximum = item.FormulaInputMaximum,
                    FormulaInputMinimum = item.FormulaInputMinimum,
                    FormulaOutputMinimum= item.FormulaOutputMinimum,
                    FormulaOutputMaximim= item.FormulaOutputMaximim
                }).ToList();
        }

        public Task<bool> IsNameUnique(string name, int id)
        {
            var isUnique = _formulaRepository.AnyAsync(x => x.FormulaName == name && x.Id != id);
            return isUnique;
        }

        public async Task<FormulaDto> UpdateAsync(int id, CreateUpdateFormulaDto formulaDto)
        {
            var formula = await Task.Run(() => _formulaRepository.GetAsync(id));

            formula.FormulaName = formulaDto.FormulaName;
            formula.Active = formulaDto.Active;
            formula.FormulaType = formulaDto.FormulaType;
            formula.FormulaMultiplier = formulaDto.FormulaMultiplier;
            formula.FormulaInputMaximum = formulaDto.FormulaInputMaximum;
            formula.FormulaInputMinimum = formulaDto.FormulaInputMinimum;
            formula.FormulaOutputMinimum = formulaDto.FormulaOutputMinimum;
            formula.FormulaInputMaximum = formulaDto.FormulaInputMaximum;

            formula = await Task.Run(() => _formulaRepository.UpdateAsync(formula));

            return new FormulaDto
            {
                Id = id,
                FormulaName = formula.FormulaName,
                FormulaType = formula.FormulaType,
                FormulaMultiplier = formula.FormulaMultiplier,
                FormulaInputMaximum = formula.FormulaInputMaximum,
                FormulaInputMinimum = formula.FormulaInputMinimum,
                FormulaOutputMinimum = formula.FormulaOutputMinimum,
                Active = formula.Active,
                FormulaOutputMaximim= formula.FormulaOutputMaximim
            };
        }
    }
}
