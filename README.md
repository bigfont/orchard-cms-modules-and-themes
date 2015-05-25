# orchard-cms-modules-and-themes

This is the repository at which BigFont develops Orchard CMS modules and themes.

Its `origin` remote is `git@github.com:OrchardCMS/Orchard.git`, from which we clonedand regularly pull `master`.

    git clone -b master git@github.com:OrchardCMS/Orchard.git
    git fetch origin
    git merge origin/master

Its `bigfont` remote is `git@github.com:bigfont/orchard-cms-modules-and-themes`, to which we push our development work. 

    git add –A
    git commit –m “Some message”
    git push bigfont

`git remote -v` outputs this:

    bigfont git@github.com:bigfont/orchard-cms-modules-and-themes (fetch)
    bigfont git@github.com:bigfont/orchard-cms-modules-and-themes (push)
    origin  git@github.com:OrchardCMS/Orchard.git (fetch)
    origin  git@github.com:OrchardCMS/Orchard.git (push)
