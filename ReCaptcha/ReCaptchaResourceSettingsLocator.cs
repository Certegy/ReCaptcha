using System;

namespace ReCaptcha
{
    public class ReCaptchaResourceSettingsLocator
    {
        private static IReCaptchaResourceSettings settings;

        public static void Register(IReCaptchaResourceSettings reCaptchaResourceSettings)
        {
            settings = reCaptchaResourceSettings;
        }

        public static string Get(Func<IReCaptchaResourceSettings, string> func)
        {
            return func.Invoke(settings);
        }
    }
}