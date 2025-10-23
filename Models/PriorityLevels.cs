// File: Models/PriorityLevels.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtisanMarketplace.Models
{
    public static class PriorityLevels
    {
        public const string Low = "LOW";
        public const string Medium = "MEDIUM";
        public const string High = "HIGH";
        public const string Urgent = "URGENT";

        public static readonly string[] AllLevels =
        {
            Low, Medium, High, Urgent
        };

        public static readonly Dictionary<string, string> LevelDisplayNames = new()
        {
            { Low, "Low" },
            { Medium, "Medium" },
            { High, "High" },
            { Urgent, "Urgent" }
        };

        public static bool IsValidLevel(string level)
        {
            return AllLevels.Contains(level.ToUpper());
        }

        public static string GetDisplayName(string level)
        {
            return LevelDisplayNames.TryGetValue(level.ToUpper(), out var displayName)
                ? displayName
                : level;
        }
    }
}
