# SvgIcons

**SvgIcons** is a collection of popular icons in the form of a .NET dll written in C#.
The icons are embedded into the DLL's resources for quick access, as using them as a git-submodule can be slow with thousands of icons. The DLL also contains generated classes to allow you to access the icons with autocomplete.

Currently, the library includes two sets of icons:

|  name | repository | site |
| --- | --- | --- |
| MaterialDesignIcons | [simple-icons/simple-icons](https://github.com/simple-icons/simple-icons) | https://materialdesignicons.com |
| SimpleIcons | [Templarian/MaterialDesign](https://github.com/simple-icons/simple-icons) | https://simpleicons.org |


SvgIcons is available as a NuGet package
```
Install-Package SvgIcons
```

Once you have a reference to the DLL, you can access the icons through the
`SvgIcons.SimpleIcons` and `SvgIcons.MaterialDesignIcons` static classes.

The `Icon` class represents a single icon, and has a `GetSvg()` method that returns the icon as an SVG string, as well as `Width` and `Height` properties that return the dimensions of the icon.

```cs
using SvgIcons;

// Access the GitHub icon from the SimpleIcons set
Icon githubIcon = SimpleIcons.GitHub;

// Get the SVG string for the icon
string svg = githubIcon.GetSvg();

// Get the width and height of the icon
float width = githubIcon.Width;
float height = githubIcon.Height;
```

:star: If you found the SvgIcons library helpful and would like to show your support for the project, please consider giving the repository a star on GitHub.