dotnet publish --configuration Release --output .\bin\publish

Compress-Archive .\bin\publish\* .\bin\deploy\web.zip


