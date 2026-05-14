namespace CybersecurityChatbotWPF.Controllers
{
    public class KeywordRecognizer
    {
        private Dictionary<string, List<string>> _keywordTopics;

        public KeywordRecognizer()
        {
            _keywordTopics = new Dictionary<string, List<string>>
            {
                ["password"] = new List<string> { "password", "passwords", "passphrase", "login", "credentials", "sign in", "authentication", "2fa", "two factor" },
                ["phishing"] = new List<string> { "phishing", "scam", "fraud", "fake email", "spoof", "impersonate", "deceptive", "baiting" },
                ["privacy"] = new List<string> { "privacy", "private", "personal info", "data", "information", "tracking", "surveillance" },
                ["malware"] = new List<string> { "malware", "virus", "trojan", "ransomware", "worm", "spyware", "adware", "keylogger" },
                ["social engineering"] = new List<string> { "social engineering", "manipulation", "trust", "trick", "impersonation", "pretexting" },
                ["safe browsing"] = new List<string> { "safe browsing", "browser", "website", "link", "url", "https", "secure site" },
                ["2fa"] = new List<string> { "2fa", "two factor", "multi factor", "mfa", "authenticator", "verification code" },
                ["backup"] = new List<string> { "backup", "back up", "restore", "recovery", "data loss" }
            };
        }

        public string RecognizeTopic(string userInput)
        {
            string lowerInput = userInput.ToLower();

            foreach (var topic in _keywordTopics)
            {
                foreach (var keyword in topic.Value)
                {
                    if (lowerInput.Contains(keyword))
                        return topic.Key;
                }
            }

            return string.Empty;
        }

        public List<string> GetAllTopics()
        {
            return new List<string>(_keywordTopics.Keys);
        }
    }
}