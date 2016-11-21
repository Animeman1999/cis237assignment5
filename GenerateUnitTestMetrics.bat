"%~dp0..\packages\OpenCover.4.5.3723\OpenCover.Console.exe" ^
-register:user ^
-target:"%VS120COMNTOOLS%\..\IDE\mstest.exe" ^
-targetargs:"/testcontainer:\"%~dp0..\BowlingSPAService.Tests\bin\Debug\BowlingSPAService.Tests.dll\" /resultsfile:\"%~dp0BowlingSPAService.trx\"" ^
-filter:"+[BowlingSPAService*]* -[BowlingSPAService.Tests]* -[*]BowlingSPAService.RouteConfig" ^
-mergebyhash ^
-skipautoprops ^
-output:"%~dp0\GeneratedReports\BowlingSPAServiceReport.xml"