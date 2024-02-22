using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Enums
{
    public enum OperatorTypes
    {
        //[Display(Name = "-")]
        //Subtraction = 1,
        //[Display(Name = "*")]
        //Multiplication = 2,
        //[Display(Name = "/")]
        //Division = 3,
        //[Display(Name = "(")]
        //OpeningParenthesis = 4,
        //[Display(Name = ")")]
        //ClosingParenthesis = 5,

        //[Display(Name = "-")]
        Subtraction = '-',
        //[Display(Name = "*")]
        Multiplication = '*',
        //[Display(Name = "/")]
        Division = '/',
        //[Display(Name = "(")]
        OpeningParenthesis = '(',
        //[Display(Name = ")")]
        ClosingParenthesis = ')',
    }
}
