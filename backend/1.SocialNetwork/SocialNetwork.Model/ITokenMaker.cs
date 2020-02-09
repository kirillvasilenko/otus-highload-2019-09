namespace SocialNetwork.Model
{
    public interface ITokenMaker
    {
        TokenBound MakeToken(long userId);
    }
}