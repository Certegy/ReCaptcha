# ReCaptcha [![Build status](https://ci.appveyor.com/api/projects/status/x52hv1rqkvc9fnb7?svg=true)](https://ci.appveyor.com/project/Certegy/publicholidays-au) [![Coverage Status](https://coveralls.io/repos/github/Certegy/ReCaptcha/badge.svg?branch=master)](https://coveralls.io/github/Certegy/ReCaptcha?branch=master)

## Overview

This library provides an easy way to embed and verify a Google ReCapture in an ASP.NET MVC application.

To embed a ReCapture in a form on a Razor view; you can simply use *GenerateCaptcha*, which is an extension method on *HtmlHelper*:
```
@using ReCaptcha
<div>@Html.GenerateCaptcha(Theme.White)</div>
```

To verify the ReCapture; all you need to do is decorate your controller operation with the *ReCapture* attribute as shown below:
```c#
public class TestController : Controller
{
	[HttpPost, ReCapture]
	public void Index([FromBody] Test test)
	{
		//...
	}
}
```

## Configuration

The only thing that needs to be done to use the ReCapture as outlined in the *overview* section, is to register an <code lang="cs" linenumbers="off">IReCaptchaResourceSettings</code> instance with the ReCapture library. A convenience class, <code lang="cs" linenumbers="off">ReCaptchaResourceSettingsLocator</code>, exists for exactly this purpose. A good approach is do this within the *global.asax* *Application_Start* method as shown below:
```c#
public class Global : HttpApplication
{
	protected void Application_Start(object sender, EventArgs e)
	{
		ReCaptchaResourceSettingsLocator.Register(new ReCaptchaResourceSettings());
	}
}
```

The <code lang="cs" linenumbers="off">ReCaptchaResourceSettings</code> instance is simply a data class that holds the following information needed by the ReCapture library:
* The ReCapture URL
* The ReCaptcha Site Key
* The ReCaptcha Secret Key

This approach allows for ReCapture localisation. The following <code lang="cs" linenumbers="off">IReCaptchaResourceSettings</code> implementation shows an example of a localised ReCapture:
```c#
public class LocalisedReCaptchaResourceSettings : IReCaptchaResourceSettings
{
    public string ReCaptchaSecretKey => Resource.ReCaptchaSecretKey;
    public string ReCaptchaSiteKey => Resource.ReCaptchaSiteKey;
    public string ReCaptchaUrl => Resource.ReCaptchaUrl;
}
```

If we have localised resources as follows:
* Resources.en-AU.resx
* Resources.en-NZ.resx
* Resources.en-US.resx

Then registering the <code lang="cs" linenumbers="off">LocalisedReCaptchaResourceSettings</code> with the ReCapture library can allow the ReCapture to work with multiple URLs defined in the resource files.

## Contributing

Would you be interested in contributing? All PRs are welcome.