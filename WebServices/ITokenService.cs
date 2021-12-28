using Models.Models;

namespace WebServices.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, IEnumerable<string> roles);
    }
}
