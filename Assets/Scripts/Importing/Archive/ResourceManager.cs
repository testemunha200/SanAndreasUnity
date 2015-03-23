﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SanAndreasUnity.Importing.Archive
{
    public static class ResourceManager
    {
        public const string GameDir = @"C:\Program Files (x86)\Steam\SteamApps\common\Grand Theft Auto San Andreas";

        public static string ModelsDir { get { return Path.Combine(GameDir, "models"); } }
        public static string DataDir { get { return Path.Combine(GameDir, "data"); } }

        public static string GetPath(params string[] relative)
        {
            return relative.Aggregate(GameDir, Path.Combine);
        }

        private static readonly List<ImageArchive> _sLoadedArchives = new List<ImageArchive>();

        public static void LoadArchive(string filePath)
        {
            _sLoadedArchives.Add(ImageArchive.Load(filePath));
        }

        public static bool FileExists(string name)
        {
            return _sLoadedArchives.Any(x => x.ContainsFile(name));
        }

        public static Stream ReadFile(string name)
        {
            var arch = _sLoadedArchives.FirstOrDefault(x => x.ContainsFile(name));
            return arch != null ? arch.ReadFile(name) : null;
        }
    }
}