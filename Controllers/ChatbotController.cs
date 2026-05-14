using CybersecurityChatbotWPF.Models;

namespace CybersecurityChatbotWPF.Controllers
{
    public class ChatbotController
    {
        private ResponseManager _responseManager;
        private KeywordRecognizer _keywordRecognizer;
        private SentimentAnalyzer _sentimentAnalyzer;
        private MemoryManager _memoryManager;
        private ConversationContext _context;

        public ChatbotController()
        {
            _responseManager = new ResponseManager();
            _keywordRecognizer = new KeywordRecognizer();
            _sentimentAnalyzer = new SentimentAnalyzer();
            _memoryManager = new MemoryManager();
            _context = new ConversationContext();
        }

        public async Task<string> ProcessUserInputAsync(string userInput)
        {
            await Task.Delay(300);

            if (string.IsNullOrWhiteSpace(userInput))
                return "Please type a message. I'm here to help with cybersecurity! ";

            // Extract name
            if (userInput.ToLower().Contains("my name is") || userInput.ToLower().Contains("i'm ") || userInput.ToLower().Contains("i am "))
            {
                ExtractName(userInput);
            }

            // Extract topic preference
            if (userInput.ToLower().Contains("interested in") || userInput.ToLower().Contains("like to learn about"))
            {
                ExtractTopicPreference(userInput);
            }

            string sentiment = _sentimentAnalyzer.AnalyzeSentiment(userInput);
            string topic = _keywordRecognizer.RecognizeTopic(userInput);

            // Handle follow-up
            if (_context.IsFollowUpRequest(userInput) && !string.IsNullOrEmpty(_context.CurrentTopic))
            {
                _context.FollowUpCount++;
                string followUpResponse = _responseManager.GetRandomTip(_context.CurrentTopic);
                string sentimentResponse = _sentimentAnalyzer.GetSentimentResponse(sentiment, _context.CurrentTopic);

                if (!string.IsNullOrEmpty(sentimentResponse))
                    return $"{sentimentResponse}\n\n{followUpResponse}\n\n{_responseManager.GetFollowUpResponse(_context.CurrentTopic)}";

                return $"{followUpResponse}\n\n{_responseManager.GetFollowUpResponse(_context.CurrentTopic)}";
            }

            // recorgnises the topic
            if (!string.IsNullOrEmpty(topic))
            {
                _context.SetTopic(topic);
                _memoryManager.AddDiscussedTopic(topic);

                string sentimentResponse = _sentimentAnalyzer.GetSentimentResponse(sentiment, topic);
                string detailedResponse = _responseManager.GetDetailedResponse(topic);
                string randomTip = _responseManager.GetRandomTip(topic);

                string finalResponse = string.IsNullOrEmpty(detailedResponse) ? randomTip : detailedResponse;

                if (!string.IsNullOrEmpty(sentimentResponse))
                    finalResponse = $"{sentimentResponse}\n\n{finalResponse}";

                finalResponse = _memoryManager.GetPersonalizedResponse(finalResponse);
                _context.LastBotResponse = finalResponse;
                return finalResponse;
            }

            // Handles any special commands
            if (IsGreeting(userInput))
                return GetGreetingResponse();

            if (IsFarewell(userInput))
                return GetFarewellResponse();

            if (userInput.ToLower().Contains("help"))
                return GetHelpResponse();

            if (userInput.ToLower().Contains("summary") || userInput.ToLower().Contains("session"))
                return GetSessionSummary();

            return _responseManager.GetDefaultResponse();
        }

        private void ExtractName(string userInput)
        {
            string[] patterns = { "my name is ", "i'm ", "i am ", "call me " };
            string lowerInput = userInput.ToLower();

            foreach (string pattern in patterns)
            {
                if (lowerInput.Contains(pattern))
                {
                    int index = lowerInput.IndexOf(pattern) + pattern.Length;
                    if (index < userInput.Length)
                    {
                        string name = userInput.Substring(index).Trim().Split(' ')[0];
                        if (!string.IsNullOrEmpty(name) && name.Length > 1)
                        {
                            _memoryManager.RememberUserInfo("name", name);
                        }
                    }
                    break;
                }
            }
        }

        private void ExtractTopicPreference(string userInput)
        {
            List<string> topics = _keywordRecognizer.GetAllTopics();
            string lowerInput = userInput.ToLower();

            foreach (string topic in topics)
            {
                if (lowerInput.Contains(topic))
                {
                    _memoryManager.RememberUserInfo("topic", topic);
                    break;
                }
            }
        }

        private bool IsGreeting(string input)
        {
            string[] greetings = { "hi", "hello", "hey", "greetings", "good morning", "good afternoon", "howdy" };
            foreach (string g in greetings)
            {
                if (input.ToLower().Contains(g))
                    return true;
            }
            return false;
        }

        private bool IsFarewell(string input)
        {
            string[] farewells = { "bye", "goodbye", "exit", "quit", "see you", "farewell", "cya" };
            foreach (string f in farewells)
            {
                if (input.ToLower().Contains(f))
                    return true;
            }
            return false;
        }

        private string GetGreetingResponse()
        {
            string name = _memoryManager.RecallUserInfo("name");
            List<string> greetings = new List<string>
            {
                $"Hello{(string.IsNullOrEmpty(name) ? "!" : $" {name}!")} Welcome to the Cybersecurity Awareness Assistant! \n\nHow can I help you stay safe online today?",
                $"Hi{(string.IsNullOrEmpty(name) ? " there" : $" {name}")}! Ready to learn about cybersecurity? Ask me about passwords, phishing, or privacy!",
                $"Greetings{(string.IsNullOrEmpty(name) ? "" : $" {name}")}! I'm here to help you navigate the digital world safely. What would you like to know?"
            };
            Random rand = new Random();
            return greetings[rand.Next(greetings.Count)];
        }

        private string GetFarewellResponse()
        {
            string name = _memoryManager.RecallUserInfo("name");
            List<string> farewells = new List<string>
            {
                $"Stay safe online{(string.IsNullOrEmpty(name) ? "!" : $", {name}!")} Remember to always think before you click!",
                "Keep practicing good cybersecurity habits! Come back anytime to learn more!",
                "Thanks for learning about cybersecurity today! Stay vigilant and stay safe!"
            };
            Random rand = new Random();
            return farewells[rand.Next(farewells.Count)];
        }

        private string GetHelpResponse()
        {
            return @" **Available Commands & Topics**

 **Cybersecurity Topics:**
• Password safety - strong passwords, managers, 2FA
• Phishing - identify and avoid email scams
• Privacy - protect your personal information
• Malware - virus protection and removal
• Social engineering - recognize manipulation
• Safe browsing - secure web practices

**Conversation Tips:**
• Ask for specific topics like 'Tell me about passwords'
• Say 'tell me more' for additional tips
• Share your name for personalized responses
• Mention what interests you most

 **Commands:**
• 'help' - Show this menu
• 'summary' - View session statistics
• 'bye' or 'exit' - End conversation

What would you like to learn about today?";
        }

        private string GetSessionSummary()
        {
            UserProfile profile = _memoryManager.GetUserProfile();
            return $@"**Session Summary**

 User: {(string.IsNullOrEmpty(profile.Name) ? "Guest" : profile.Name)}
 Duration: {profile.GetSessionDuration()}
 Topics Discussed: {(profile.DiscussedTopics.Count > 0 ? string.Join(", ", profile.DiscussedTopics) : "None yet")}
{(string.IsNullOrEmpty(profile.FavoriteTopic) ? "" : $" Favorite Topic: {profile.FavoriteTopic}")}*-+-

 You're building valuable cybersecurity knowledge! Keep learning and stay safe!";
        }
    }
}
