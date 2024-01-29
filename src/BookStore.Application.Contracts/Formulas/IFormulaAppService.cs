using BookStore.Books;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BookStore.Formulas
{
    public interface IFormulaAppService: IApplicationService
    {
        Task<IReadOnlyList<FormulaDto>> GetListAsync();
        Task<FormulaDto> CreateAsync(CreateUpdateFormulaDto formulaDto);
        Task<FormulaDto> UpdateAsync(int id, CreateUpdateFormulaDto formulaDto);
        Task DeleteAsync(int id);
        Task<bool> IsNameUnique(string name, int id);
    }
}
