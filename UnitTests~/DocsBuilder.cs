﻿using System;
using System.Diagnostics;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;

public static class DocsBuilder
{
    public static void BuildDocs()
    {
        ProjectGeneration projectGeneration = new ProjectGeneration();
        AssetDatabase.Refresh();
        projectGeneration.GenerateAndWriteSolutionAndProjects();

        try
        {
            RunProcess("./build-docs.sh");
        }
        catch (Exception e)
        {
            System.Console.Error.WriteLine("Failed to build docs: " + e);
        }
    }

    private static void RunProcess(string command)
    {
        System.Console.Error.WriteLine("=== Running command: " + command + " ===");
        var process = Process.Start("/bin/sh", command);
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new Exception("Process failed: " + command + " - with error code " + process.ExitCode);
        }
    }
}
