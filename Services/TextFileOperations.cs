using System.Text;

namespace VisitorManagementSystem.Services
{
    public class TextFileOperations : ITextFileOperations
    {
        private IWebHostEnvironment webHostEnvironment { get; }

        public TextFileOperations(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        public IEnumerable<string> LoadConditionsForAcceptanceText()
        {

            string rootPath = webHostEnvironment.WebRootPath;

            FileInfo filePath = new FileInfo(Path.Combine(rootPath, ("CFA.txt")));

            string[] lines = System.IO.File.ReadAllLines(filePath.ToString(), Encoding.UTF8);

            return lines.ToList();
        }

    }
}
