using System;
using System.Security.Claims;
using System.Threading;

namespace Api.Util
{
    public static class ClaimUsuario
    {
        //var claimsIdentity = User.Identity as ClaimsIdentity;
        //var result = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;
        public static Guid UsuarioIdClaim()
        {
            try
            {
                //var claimsIdentity = Thread.CurrentPrincipal as ClaimsPrincipal;

                
                var claimsIdentity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var result = claimsIdentity.FindFirst(ClaimTypes.Sid).Value;

                return new Guid(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);


            }


        }
        public static string UsuarioNomeClaim()
        {
            var claimsIdentity = Thread.CurrentPrincipal as ClaimsPrincipal;
            var result = claimsIdentity.FindFirst(ClaimTypes.Name).Value;

            return result.ToString();
        }
        public static string UsuarioEmailClaim()
        {
            var claimsIdentity = Thread.CurrentPrincipal as ClaimsPrincipal;
            var result = claimsIdentity.FindFirst(ClaimTypes.Email).Value;

            return result.ToString();
        }
    }
}
