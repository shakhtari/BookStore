using BookStore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Formulas
{
    public class CreateUpdateFormulaDto
    {
        public int Id { get; set; }
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
