call %~dp0\GenerateTypeScript.bat
if not "%errorlevel%"=="0" exit /b 1

cd %~dp0\angular
call npm install
if not "%errorlevel%"=="0" exit /b 1

call npm run build-prod
if not "%errorlevel%"=="0" exit /b 1

cd ..
powershell %~dp0\..\..\deploy\TransformIndex.ps1 -SourcePath %~dp0\angular\dist\index.html -TargetPath %~dp0\angular\dist\index.cshtml
if not "%errorlevel%"=="0" exit /b 1
