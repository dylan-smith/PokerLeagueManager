call %~dp0\GenerateTypeScript.bat
cd %~dp0\angular
call npm install
call npm run build
cd ..
powershell %~dp0\..\..\deploy\TransformIndex.ps1 -SourcePath %~dp0\angular\dist\index.html -TargetPath %~dp0\angular\dist\index.cshtml
