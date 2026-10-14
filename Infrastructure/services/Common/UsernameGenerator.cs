using Application.Common.Interfaces.Common;

namespace Infrastructure.services.Common
{
    public class UsernameGenerator: IUsernameGenerator
    {
        public string Generate(string email)
        {
            return email.Split('@')[0];
        }

    }
}
