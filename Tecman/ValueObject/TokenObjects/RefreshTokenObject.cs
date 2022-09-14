/**
 * Created: 
 * Date: 
 * Modified: Daniel Quintal
 * Date: November, 23, 2021
 *
 * Refresh token model file - FRONT
 * 
 */

namespace Tecman.ValueObject {
    public class RefreshTokenObject {
        public int userId { get; set; }
        public string userCorporateEmail { get; set; }
        public string userName { get; set; }
        public string refreshToken { get; set; }
    }
}