namespace BookStore.DerivedValues
{
    public static class DerivedValueConsts
    {
        private const string DefaultSorting = "{0}Active asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "DerivedValue." : string.Empty);
        }

        public const int DVDescriptionMaxLength = 100;
        public const int DVFormulaMaxLength = 256;
    }
}