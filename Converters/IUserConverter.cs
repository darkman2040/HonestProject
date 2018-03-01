namespace HonestProject.Converters
{
    public interface IUserConverter
    {
        ViewModels.User ConvertFromDbUser(DataModels.User user);
    }
}