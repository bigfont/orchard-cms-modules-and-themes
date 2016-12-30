# orchard-cmd-modules-and-themes

This is where [BigFont.ca]() develops modules and themes for Orchard.

Here is a hack to package a theme, because packaging fails in this repositor's build. 

1. Download Orchard CMS as a Zip: http://www.orchardproject.net/
1. Expand the zip `Expand-Archive .\Orchard.Web.zip` 
1. Copy the theme you want to build into the downloaded Orchard site:
 * `copy -Recurse Bootstrap_3_1_1_KongwaHill ~\Downloads\Orchard.Web\Orchard\Themes\`
 * `copy -Recurse Bootstrap_3_1_1_Base ~\Downloads\Orchard.Web\Orchard\Themes\`      
1. Run `~\Downloads\Orchard.Web\Orchard\bin\orchard.exe`.
1. At the orchard prompt, run these commands: 

Orchard Prompt:

    orchard> setup /SiteName:SITE /AdminUsername:USER /AdminPassword:PASS /DatabaseProvider:SQLCE
    orchard> package create Bootstrap_3_1_1_KongwaHill C:/temp
