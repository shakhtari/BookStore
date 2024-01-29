using BookStore.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BookStore.Formulas
{
    public class FormulaDto: EntityDto<int>
    {
        public bool Active { get; set; }
        public string FormulaName { get; set; }
        public FormulaType FormulaType { get; set; }
        public decimal? FormulaMultiplier { get; set; }
        public decimal? FormulaInputMinimum { get; set; }
        public decimal? FormulaInputMaximum { get; set; }
        public decimal? FormulaOutputMinimum { get; set; }
        public decimal? FormulaOutputMaximim { get; set; }
    }
}
