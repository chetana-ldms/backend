namespace LDP.Common.Helpers.PDFGenerator
{
    public interface IPdfGenerator
    {
        void GeneratePDF<T>(IEnumerable<T> data, string outputPath, string headerText);
    }
}
