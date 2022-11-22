# assemblyinfo.exe

## Purpose

Console application written in C# with the NET Core 3.1 Framework (works on Linux, macOS, and Windows).

It prints to stdout the *AssemblyInfos* of `*.exe` and `*.dll` files.

When provided with a directory, it searches for `*.exe` and `*.dll` files inside that directory and return the info for all of them.

**Usage:**
```cmd
asseblyinfo.exe [my_file.exe] [my_file.dll] [my_directory/]
```

## Integration in `Git` as diff tool for `*.exe` and `*.dll` files

Add to `.gitconfig`:
```git
[diff "exe"]
        textconv = assemblyinfo.exe
        binary = true
[difftool "exe"]
        prompt = false
        cmd = assemblyinfodiff.sh $LOCAL $REMOTE
```

Add to `.gitattributes`:
```git
*.dll diff=exe
*.exe diff=exe
```
