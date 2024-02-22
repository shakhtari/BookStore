namespace BookStore.DerivedValueDetails
{
    public static class DerivedValueDetailConsts
    {
        private const string DefaultSorting = "{0}DVId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "DerivedValueDetail." : string.Empty);
        }

    }
}