namespace ReCaptcha.ResourceSettings
{
    public interface IReCaptchaResourceSettings
    {
        string ReCaptchaSecretKey { get; }
        string ReCaptchaSiteKey { get; }
        string ReCaptchaUrl { get; }
    }
}