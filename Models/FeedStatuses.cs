// File: Models/FeedStatuses.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtisanMarketplace.Models
{
    public static class FeedStatuses
    {
        public const string Open = "OPEN";
        public const string InReview = "IN_REVIEW";
        public const string Negotiating = "NEGOTIATING";
        public const string Closed = "CLOSED";
        public const string Completed = "COMPLETED";
        public const string Cancelled = "CANCELLED";

        public static readonly string[] AllStatuses = 
        {
            Open, InReview, Negotiating, Closed, Completed, Cancelled
        };

        public static readonly Dictionary<string, string> StatusDisplayNames = new()
        {
            { Open, "Open" },
            { InReview, "In Review" },
            { Negotiating, "Negotiating" },
            { Closed, "Closed" },
            { Completed, "Completed" },
            { Cancelled, "Cancelled" }
        };

        public static bool IsValidStatus(string status)
        {
            return AllStatuses.Contains(status.ToUpper());
        }

        public static string GetDisplayName(string status)
        {
            return StatusDisplayNames.TryGetValue(status.ToUpper(), out var displayName)
                ? displayName
                : status;
        }
    }
}
