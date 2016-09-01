@echo off
Packages\xunit.runner.console.2.1.0\tools\xunit.console ^
	CarFuel.Tests\bin\Debug\CarFuel.Tests.dll ^
	-parallel all ^
	-html Result.html ^
	-nologo  
@echo on 