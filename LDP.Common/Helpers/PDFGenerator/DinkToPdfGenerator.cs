using DinkToPdf;
using DinkToPdf.Contracts;
using System.Text;

namespace LDP.Common.Helpers.PDFGenerator
{
    public class DinkToPdfGenerator : IPdfGenerator
    {
        private readonly IConverter _converter;

        public DinkToPdfGenerator(IConverter converter)
        {
            _converter = converter;
        }

        public void GeneratePDF<T>(IEnumerable<T> data, string outputPath,string headerText)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>{headerText}</h1></div>
                                <table align='center'>
                                    <tr>");

            // Get the property names of the object
            var properties = typeof(T).GetProperties();

            // Generate the table headers dynamically
            foreach (var property in properties)
            {
                sb.AppendFormat(@"<th>{0}</th>", property.Name);
            }

            sb.Append(@"</tr>");

            // Generate the table rows dynamically
            foreach (var item in data)
            {
                sb.Append("<tr>");

                foreach (var property in properties)
                {
                    var value = property.GetValue(item);
                    sb.AppendFormat("<td>{0}</td>", value);
                }

                sb.Append("</tr>");
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects = {
                    new ObjectSettings() {
                        HtmlContent = sb.ToString(),
                    }
                }
            };

            var pdfBytes = _converter.Convert(doc);

            File.WriteAllBytes(outputPath, pdfBytes);
        }

    }
}
