namespace HonestProject.Utilities {

    public interface IJwtUtilities
    {   
        string GenereateJwtToken(DataModels.User user);
        string GenerateRefreshToken(DataModels.User user);
    }
}