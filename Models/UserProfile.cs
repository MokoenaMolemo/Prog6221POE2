using System;
using System.Collections.Generic;

namespace CybersecurityChatbotWPF.Models
{
    public class UserProfile
    {
        public string Name { get; set; } = string.Empty;
        public string FavoriteTopic { get; set; } = string.Empty;
        public List<string> DiscussedTopics { get; set; } = new List<string>();
        public DateTime SessionStart { get; set; }
        public string LastSentiment { get; set; } = "neutral";
        public Dictionary<string, string> UserPreferences { get; set; } = new Dictionary<string, string>();

        public UserProfile()
        {
            SessionStart = DateTime.Now;
        }

        public void AddDiscussedTopic(string topic)
        {
            if (!DiscussedTopics.Contains(topic))
                DiscussedTopics.Add(topic);
        }

        public string GetSessionDuration()
        {
            var duration = DateTime.Now - SessionStart;
            return $"{duration.Minutes}:{duration.Seconds:D2}";
        }
    }
}