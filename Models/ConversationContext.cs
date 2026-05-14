using System.Collections.Generic;

namespace CybersecurityChatbotWPF.Models
{
    public class ConversationContext
    {
        public string CurrentTopic { get; set; } = string.Empty;
        public string LastBotResponse { get; set; } = string.Empty;
        public int FollowUpCount { get; set; } = 0;
        public List<string> RecentTopics { get; set; } = new List<string>();

        public void SetTopic(string topic)
        {
            if (!string.IsNullOrEmpty(topic))
            {
                CurrentTopic = topic;
                RecentTopics.Add(topic);
                if (RecentTopics.Count > 5)
                    RecentTopics.RemoveAt(0);
            }
        }

        public bool IsFollowUpRequest(string userInput)
        {
            var followUpPhrases = new[] {
                "tell me more", "explain more", "another tip",
                "more information", "elaborate", "continue", "and?",
                "more", "additional", "further"
            };

            foreach (var phrase in followUpPhrases)
            {
                if (userInput.ToLower().Contains(phrase))
                    return true;
            }
            return false;
        }
    }
}