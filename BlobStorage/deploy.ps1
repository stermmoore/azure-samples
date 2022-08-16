Connect-AzAccount #Will prompt user to login to Azure

New-AzResourceGroupDeployment -ResourceGroupName "rg-azure-samples" -TemplateFile "deployment.json" -TemplateParameterFile "parameters.json"