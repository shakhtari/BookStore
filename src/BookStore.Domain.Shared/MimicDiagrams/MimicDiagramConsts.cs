namespace BookStore.MimicDiagrams
{
    public static class MimicDiagramConsts
    {
        private const string DefaultSorting = "{0}Active asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "MimicDiagram." : string.Empty);
        }

        public const int MimicDiagramNameMaxLength = 250;
        public const int MimicDiagramDescriptionMaxLength = 250;
    }
}