using CsUtils.Models;

namespace CsUtils;

internal class SlnParser
{
    private readonly string _slnFilePath;

    public SlnParser(string slnFilePath)
    {
        _slnFilePath = slnFilePath;
    }


    public bool Validate()
    {
        if (!File.Exists(_slnFilePath))
        {
            Console.WriteLine("File does not exist in given path. Remember to add hyphens \"<full path>\".");
            return false;
        }

        if (Path.GetExtension(_slnFilePath) != ".sln")
        {
            Console.WriteLine("Given file is not of type '.sln'");
            return false;
        }

        return true;
    }

    public IReadOnlyList<ProjectInfo> Parse()
    {
        var lines = File.ReadAllLines(_slnFilePath).ToList();

        var projectListing = ParseProjects(lines);
        var infos = new List<ProjectInfo>();
        var directory = Path.GetDirectoryName(_slnFilePath) ?? throw new ArgumentException();
        foreach (var (name, relativePath) in projectListing)
        {
            var fullPath = Path.Combine(directory, relativePath);
            if (!File.Exists(fullPath))
            {
                // Sub-directory etc
                continue;
            }

            infos.Add(new ProjectInfo(name, relativePath, fullPath));
        }

        return infos;
    }

    // Microsoft Visual Studio Solution File, Format Version 12.00
    // # Visual Studio Version 17
    // VisualStudioVersion = 17.2.32519.379
    // MinimumVisualStudioVersion = 10.0.40219.1
    // Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "projectA", "projectA\projectA.csproj", "{C1CD129C-27BE-4FA1-BEBA-493292174B61}"
    // EndProject
    // Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "projectB", "projectB\projectB.csproj", "{73A84E27-75DA-4719-9528-D92E74C01FFB}"
    // EndProject
    // Global
    private static List<ProjectNameAndPath> ParseProjects(List<string> lines)
    {
        var projects = new List<ProjectNameAndPath>();
        var projectLineRegex = new System.Text.RegularExpressions.Regex(
            @"^Project\(""\{[^""]+\}""\)\s*=\s*""([^""]+)"",\s*""([^""]+)""",
            System.Text.RegularExpressions.RegexOptions.Compiled);

        foreach (var line in lines)
        {
            var match = projectLineRegex.Match(line);
            if (match.Success)
            {
                var name = match.Groups[1].Value;
                var path = match.Groups[2].Value;
                projects.Add(new ProjectNameAndPath(name, path));
            }
        }
        return projects;
    }

    private sealed record ProjectNameAndPath(string Name, string Path);
}