mkdir coverage-results
.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:.\packages\xunit.runner.console.2.1.0\tools\xunit.console.exe "-targetargs:.\ReCaptcha.UnitTests\bin\Release\ReCaptcha.UnitTests.dll -noshadow" "-filter:+[ReCaptcha*]* -[*Tests]*" -skipautoprops -output:.\coverage-results\results.xml
#.\packages\ReportGenerator.2.5.1\tools\ReportGenerator.exe -reports:.\coverage-results\results.xml -targetdir:.\reports
.\packages\coveralls.net.0.7.0\tools\csmacnz.Coveralls.exe --opencover --input .\coverage-results\results.xml --repoToken $env:COVERALLS_REPO_TOKEN --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_JOB_ID