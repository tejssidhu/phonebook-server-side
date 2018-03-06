<#

.SYNOPSIS
This is a simple Powershell script that is used to replace attribute values in an xml file.

.DESCRIPTION
This script will take a path to an xml file, find all elements of a given name with a particular attribute value and replace a given attribute on each found element with a new value.

.EXAMPLE
.\XmlReplaceAttributeValue.ps1 -relativeFilePath "../../myXmlFile.xml" -elementNameToFind "ourParameter" -attributeToFindName "name" -attributeToFindValue "OldValue" -attributeToReplaceName "value" -attributeNewValue "NewValue"

.NOTES
Given an xml file "myXmlFile.xml" which sits along side this script:
<myRoute>
    <cities>
        <city name="Shanghai" population="24,183,300">
        <city name="Beijing" population="20,794,000">
        <city name="Karachi" population="14,910,352">
        <city name="Shenzhen" population="13,732,000">
    </ourParameters>
</myRoute>

Running the following:
.\XmlReplaceAttributeValue.ps1 -relativeFilePath "myXmlFile.xml" -elementNameToFind "city" -attributeToFindName "name" -attributeToFindValue "Beijing" -attributeToReplaceName "populatiopn" -attributeNewValue "21,000,000"

Will change the xml file to:
<myRoute>
    <cities>
        <city name="Shanghai" population="24,183,300">
        <city name="Beijing" population="21,000,000">
        <city name="Karachi" population="14,910,352">
        <city name="Shenzhen" population="13,732,000">
    </ourParameters>
</myRoute>

.PARAMETER relativeFilePath
A relative path from this script file to the xml file you want to amend

.PARAMETER elementNameToFind
An element name that you want to find to amend

.PARAMETER attributeToFindName
An attribute name on the above element that you want to find

.PARAMETER attributeToFindValue
An attribute value for the above attribute name that you want to find

.PARAMETER attributeToReplaceName
An attribute name that you want to replace

.PARAMETER attributeNewValue
A new attribute value that will replace the current value of the above named attribute

#>

Param(
    [Parameter(Mandatory=$True,Position=1)]
    [string]$relativeFilePath,
    [string]$elementNameToFind,
    [string]$attributeToFindName,
    [string]$attributeToFindValue,
    [string]$attributeToReplaceName,
    [string]$attributeNewValue
)

function WriteXmlToScreen ([xml]$xml)
{
    $StringWriter = New-Object System.IO.StringWriter;
    $XmlWriter = New-Object System.Xml.XmlTextWriter $StringWriter;
    $XmlWriter.Formatting = "indented";
    $xml.WriteTo($XmlWriter);
    $XmlWriter.Flush();
    $StringWriter.Flush();
    Write-Output $StringWriter.ToString();
}

# get full path using the relative path and the powershell scripts location
[string] $fullPath = [System.IO.Path]::GetFullPath((Join-Path (Get-Location) $relativeFilePath))

#check that an xml file exists at this path
if(![System.IO.File]::Exists($fullPath)){
    Write-Host('Path: "' + $fullPath + '" doesnt exist')
}
else {
    # ensure the found file is an xml file
    [bool] $isXml = ((Get-Content $fullPath) -as [xml])
    if (!$isXml) {
        Write-Host('File: "' + $fullPath + '" is not xml')
    } else {
        # load the xml file
        [xml] $xml = [xml](Get-Content $fullPath)
        #prepare an xpath string using the passed in parameters
        [string] $xPath = "//" + $elementNameToFind + "[@" + $attributeToFindName + " = """ + $attributeToFindValue + """]"
        # find all elements matching the xPath value 
        $xmlFoundElements = $xml.SelectNodes($xPath)
        # check that at least 1 element was found
        if ($xmlFoundElements.Count -eq 0) {
            Write-Host('No elements found using xpath:' + $xPath)
        } else {
            # loop over every element and set a new attribute value using the parameters
            foreach ($element in $xmlFoundElements) {
                # TODO: do some checking to see that the element contains the attribute we intend to replace
                $element.SetAttribute($attributeToReplaceName, $attributeNewValue)
            }
    
            # save the update xml file to original file
            $xml.Save($fullPath)

            [xml] $updateXml = [xml](Get-Content $fullPath)
            WriteXmlToScreen($updateXml)
        }
    }
}
