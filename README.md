# orchard-cmd-modules-and-themes

This is where [BigFont.ca]() develops modules and themes for Orchard.

We regularly pull from the `master` branch at `git@github.com:OrchardCMS/Orchard.git`.

`git remote -v` displays this:

    bigfont git@github.com:bigfont/orchard-cms-modules-and-themes (fetch)
    bigfont git@github.com:bigfont/orchard-cms-modules-and-themes (push)
    origin  git@github.com:OrchardCMS/Orchard.git (fetch)
    origin  git@github.com:OrchardCMS/Orchard.git (push)

Build from the command line with: 

    msbuild /p:VisualStudioVersion=14.0 .\src\Orchard.Web\Orchard.Web.csproj
