namespace HonestProject.Utilities
{
    public interface IPasswordHashUtility
    {
        string CalculateHash(string input);
        bool CheckMatch(string hash, string input);
    }
}