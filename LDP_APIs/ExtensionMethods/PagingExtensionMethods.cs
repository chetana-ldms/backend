namespace LDP_APIs.ExtensionMethods
{
    public static class PagingExtensionMethods
    {
        public static int CalculatePagingStart(this int pageNumber, int recordsPerPage)
        {
        //    var start = (pageNumber - 1) * recordsPerPage + 1;
        //    var end = start + recordsPerPage - 1;
        //    return (start, end);
        return 1;
        }
    }
}
