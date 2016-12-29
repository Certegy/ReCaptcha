using ReCaptcha;

namespace Recaptcha.UnitTests.Helpers
{
    public class TestReCaptchaResourceSettings : IReCaptchaResourceSettings
    {
        public string ReCaptchaSecretKey => "1B7B9407-B4C3-4464-8892-FBA60700FCBA";
        public string ReCaptchaSiteKey => "6CAEF5BE-9C28-4340-BC97-98B04AF7902E";
        public string ReCaptchaUrl => "test.com.au";
    }
}
