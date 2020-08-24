
[CmdletBinding()]
Param(
    $prefix,
    $rgName = $prefix + "-cspreport",
    $rgLocation = "westeurope",
    $additionalParameters = @{}
);

#az group create --location --name[--managed-by] [--subscription] [--tags]
az group create --location $rgLocation --name $rgName

<#
az deployment group create --resource-group
                           [--aux-subs]
                           [--aux-tenants]
                           [--confirm-with-what-if]
                           [--handle-extended-json-format]
                           [--mode {Complete, Incremental}]
                           [--name]
                           [--no-prompt {false, true}]
                           [--no-wait]
                           [--parameters]
                           [--rollback-on-error]
                           [--subscription]
                           [--template-file]
                           [--template-uri]
                           [--what-if-exclude-change-types {Create, Delete, Deploy, Ignore, Modify, NoChange}]
                           [--what-if-result-format {FullResourcePayloads, ResourceIdOnly}]
#>
$additionalParametersString =  $additionalParameters.GetEnumerator() | % { "$($_.Name)=$($_.Value)" } 

az deployment group  create --resource-group $rgName --template-file template.json --parameters prefix=$prefix $additionalParametersString
#--confirm-with-what-if
#--no-prompt 