using System;
using ReCaptcha.ResourceSettings;

namespace ReCaptcha
{
    public class ReCaptchaResourceSettingsLocator
    {
        private static IReCaptchaResourceSettings _settings;

        public static void Register(IReCaptchaResourceSettings reCaptchaResourceSettings)
        {
            _settings = reCaptchaResourceSettings;
        }

        public static string Get(Func<IReCaptchaResourceSettings, string> func)
        {
            return func.Invoke(_settings);
        }
    }
}