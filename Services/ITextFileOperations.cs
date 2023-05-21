namespace VisitorManagementSystem.Services
{
    public interface ITextFileOperations
    {
        IEnumerable<string> LoadConditionsForAcceptanceText();
    }
}