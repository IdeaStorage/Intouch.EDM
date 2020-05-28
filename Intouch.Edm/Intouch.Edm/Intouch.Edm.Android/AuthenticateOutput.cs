namespace Intouch.Edm.Droid
{
    public class UserMobileAppTokenInput
    {
        public long UserId { get; set; }
        public string Token { get; set; }
    }

    public class AuthenticateInput
    {
        public string UserNameOrEmailAddress { get; set; }

        public string Password { get; set; }
    }

    public class AuthenticateOutput
    {
        public AuthenticateOutputResult Result { get; set; }
    }

    public class AuthenticateOutputResult
    {
        public string AccessToken { get; set; }
    }
}