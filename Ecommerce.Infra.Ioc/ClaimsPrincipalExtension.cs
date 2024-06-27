using System.Security.Claims;

namespace Ecommerce.Infra.Ioc
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            var existId = user.FindFirst("id");

            if (existId == null)
            {
                return 0;
            }

            return int.Parse(user.FindFirst("id").Value);
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst("email").Value;
        }
    }
}
