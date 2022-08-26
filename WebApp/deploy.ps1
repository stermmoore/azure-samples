dotnet publish --configuration Release --output .\bin\publish

Compress-Archive .\bin\publish\* .\bin\publish\web.zip

Connect-AzAccount

Publish-AzWebApp -ResourceGroupName rg-azure-samples -Name 34534wesdr23 -ArchivePath .\bin\publish\web.zip