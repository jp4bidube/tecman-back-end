/**
 * Created: 
 * Date: 
 * Modified: Daniel Quintal
 * Date: December, 23, 2021
 *
 * Token model - FRONT
 * 
 */

namespace Tecman.ValueObject
{
    public class TokenObject
    {
        public TokenObject(bool authenticated, int userId, int userStatus, int userProfile, string userName, string name, string created, string expiration, string accessToken, string refreshToken)
        {
            Authenticated = authenticated;
            UserId = userId;
            UserStatus = userStatus;
            UserProfile = userProfile;
            UserName = userName;
            Name = name;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public bool Authenticated { get; set; }

        public int UserId { get; set; }

        public int UserStatus { get; set; }

        public int UserProfile { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Created { get; set; }

        public string Expiration { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}