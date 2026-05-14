namespace CybersecurityChatbotWPF.Controllers
{
    public class SentimentAnalyzer
    {
        private Dictionary<string, List<string>> _sentimentKeywords;
        private Random _random;

        public SentimentAnalyzer()
        {
            _random = new Random();
            _sentimentKeywords = new Dictionary<string, List<string>>
            {
                ["worried"] = new List<string> { "worried", "scared", "afraid", "nervous", "anxious", "concerned", "fear", "panic" },
                ["frustrated"] = new List<string> { "frustrated", "annoyed", "angry", "upset", "mad", "irritated", "tired", "overwhelmed" },
                ["curious"] = new List<string> { "curious", "interested", "want to learn", "tell me", "explain", "teach me", "how to" },
                ["confused"] = new List<string> { "confused", "don't understand", "unclear", "what does", "meaning", "explain" },
                ["grateful"] = new List<string> { "thank", "thanks", "helpful", "appreciate", "useful", "great", "awesome" }
            };
        }

        public string AnalyzeSentiment(string userInput)
        {
            string lowerInput = userInput.ToLower();

            foreach (var sentiment in _sentimentKeywords)
            {
                foreach (var keyword in sentiment.Value)
                {
                    if (lowerInput.Contains(keyword))
                        return sentiment.Key;
                }
            }

            return "neutral";
        }

        public string GetSentimentResponse(string sentiment, string topic)
        {
            switch (sentiment)
            {
                case "worried": return GetWorriedResponse(topic);
                case "frustrated": return GetFrustratedResponse(topic);
                case "curious": return GetCuriousResponse(topic);
                case "confused": return GetConfusedResponse(topic);
                case "grateful": return GetGratefulResponse(topic);
                default: return string.Empty;
            }
        }

        private string GetWorriedResponse(string topic)
        {
            List<string> responses = new List<string>
            {
                $"It's completely understandable to feel worried about {topic}. Let me share some practical tips to help you stay protected.",
                $"I hear your concern about {topic}. Many people feel this way, but with the right knowledge, you can protect yourself effectively.",
                $"Your worry about {topic} is valid, but don't panic! Let me give you simple, actionable steps to stay safe."
            };
            return responses[_random.Next(responses.Count)];
        }

        private string GetFrustratedResponse(string topic)
        {
            List<string> responses = new List<string>
            {
                $"I understand {topic} can be frustrating. Let me break it down simply for you.",
                $"Sorry you're feeling frustrated! Let's make {topic} easier to understand.",
                $"I get it - cybersecurity can be overwhelming. Let me simplify {topic} for you."
            };
            return responses[_random.Next(responses.Count)];
        }

        private string GetCuriousResponse(string topic)
        {
            List<string> responses = new List<string>
            {
                $"Great curiosity about {topic}! Let me share some fascinating information.",
                $"I love your enthusiasm to learn about {topic}! Here's what you should know:",
                $"Excellent question! Here's a detailed explanation about {topic}:"
            };
            return responses[_random.Next(responses.Count)];
        }

        private string GetConfusedResponse(string topic)
        {
            return $"Let me clarify {topic} in simpler terms. Think of it this way: ";
        }

        private string GetGratefulResponse(string topic)
        {
            List<string> responses = new List<string>
            {
                $"You're very welcome! Since you found that helpful, would you like to learn more about {topic}?",
                $"Happy to help!Anything else about {topic} you'd like to explore?",
                $"Thank you for your kind words! Shall we dive deeper into {topic} or switch to another topic?"
            };
            return responses[_random.Next(responses.Count)];
        }
    }
}
