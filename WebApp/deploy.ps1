dotnet publish --configuration Release --output .\bin\publish

Compress-Archive .\bin\publish\* .\bin\publish\web.zip -Force

Connect-AzAccount

$appName = "54534wesdr23"

$params = @{

	webAppName = $appName

}

New-AzResourceGroupDeployment -ResourceGroupName "rg-azure-samples" -TemplateFile "deployment.json" -TemplateParameterObject $params

Publish-AzWebApp -ResourceGroupName rg-azure-samples -Name $appName -ArchivePath .\bin\publish\web.zip