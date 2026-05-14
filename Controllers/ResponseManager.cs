namespace CybersecurityChatbotWPF.Controllers
{
    public class ResponseManager
    {
        private Random _random;
        private Dictionary<string, List<string>> _randomResponses;
        private Dictionary<string, string> _detailedResponses;

        public ResponseManager()
        {
            _random = new Random();

            _randomResponses = new Dictionary<string, List<string>>
            {
                ["phishing_tips"] = new List<string>
                {
                    " Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                    " Never click on links in suspicious emails. Hover over links first to see the actual URL.",
                    " Check sender email addresses carefully - scammers use addresses that look similar to real ones.",
                    " Legitimate companies never ask for passwords or banking details via email.",
                    " Be wary of urgent messages claiming your account will be closed if you don't act immediately.",
                    " Look for spelling and grammar mistakes - these are common signs of phishing attempts."
                },
                ["password_tips"] = new List<string>
                {
                    " Use at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols.",
                    " Never reuse passwords across different accounts - each account needs a unique password.",
                    " Use a password manager to generate and store strong, unique passwords.",
                    " Enable 2-factor authentication (2FA) whenever possible for an extra layer of security.",
                    " Avoid using personal information like birthdays, names, or common words in passwords.",
                    " Consider using passphrases - a sequence of random words that's long but memorable."
                },
                ["privacy_tips"] = new List<string>
                {
                    " Review privacy settings on social media regularly - limit who can see your posts.",
                    " Be careful what personal information you share online - once posted, it's hard to remove.",
                    " Check app permissions on your phone - many apps request access they don't need.",
                    " Use a VPN on public WiFi to encrypt your internet traffic.",
                    " Regularly Google yourself to see what information about you is publicly available.",
                    " Consider using email aliases for different services to protect your primary email."
                },
                ["malware_tips"] = new List<string>
                {
                    " Keep your antivirus software updated and run regular scans.",
                    " Don't download software from untrusted sources - stick to official websites.",
                    " Be extremely careful with email attachments, even from people you know.",
                    " Keep your operating system and all software updated with security patches.",
                    " Regularly backup important data to an external drive or cloud storage.",
                    " Avoid using USB drives from unknown sources."
                },
                ["social_engineering_tips"] = new List<string>
                {
                    " Never share sensitive information over the phone unless you initiated the call.",
                    " Always verify unexpected requests through a different communication channel.",
                    " Be suspicious of anyone creating a sense of urgency or pressure.",
                    " Trust your instincts - if something feels wrong, it probably is.",
                    " Verify email requests for money or information by calling the person directly.",
                    " No legitimate organization will ask for your password or 2FA codes."
                },
                ["safe_browsing_tips"] = new List<string>
                {
                    " Look for 'https://' and the padlock icon in your browser's address bar.",
                    " Don't click on pop-up ads claiming your computer is infected.",
                    " Use ad-blockers and privacy extensions in your browser.",
                    " Keep your browser updated to the latest version for security patches.",
                    " Be careful what you download - even 'free' software can contain malware.",
                    " Clear your browser cache and cookies regularly."
                }
            };

            _detailedResponses = new Dictionary<string, string>
            {
                ["password"] = "*Password Security Explained**\n\nStrong passwords are your first line of defense. Here's what makes a password secure:\n\n**Do's:**\n• Use 12+ characters with uppercase, lowercase, numbers, symbols\n• Create unique passwords for every account\n• Use a password manager like Bitwarden or LastPass\n• Enable 2-factor authentication wherever possible\n\n**Don'ts:**\n• Use 'password123' or 'qwerty'\n• Use personal info (birthdays, names)\n• Reuse passwords across sites\n• Write passwords on sticky notes\n\n **Pro Tip:** Consider using passphrases - e.g., 'PurpleMonkeyDishwasher$2024'",

                ["phishing"] = "**Phishing Attacks Explained**\n\nPhishing is when scammers try to trick you into revealing sensitive information. Here's how to spot them:\n\n **Red Flags:**\n• Urgent language demanding immediate action\n• Generic greetings like 'Dear Customer'\n• Suspicious sender email addresses\n• Spelling and grammar mistakes\n• Requests for personal information\n• Unexpected attachments\n\n **How to Protect Yourself:**\n• Never click links in suspicious emails\n• Verify by calling the organization directly\n• Use email filters and spam protection\n• Report phishing attempts to the legitimate company",

                ["privacy"] = " **Online Privacy Protection**\n\nYour personal data is valuable. Here's how to protect it:\n\n **What to Protect:**\n• Full name and address\n• ID/passport numbers\n• Banking details\n• Login credentials\n• Medical information\n\n🛠 **Protection Strategies:**\n• Use privacy-focused browsers (Firefox, Brave)\n• Enable Do Not Track features\n• Use search engines that don't track (DuckDuckGo)\n• Regularly clear cookies and browsing history\n• Use email aliases for online signups"
            };
        }

        public string GetRandomTip(string category)
        {
            string key = $"{category}_tips";
            if (_randomResponses.ContainsKey(key) && _randomResponses[key].Count > 0)
            {
                return _randomResponses[key][_random.Next(_randomResponses[key].Count)];
            }
            return GetDefaultTip(category);
        }

        private string GetDefaultTip(string category)
        {
            switch (category)
            {
                case "password": return " Use a password manager to create and store strong, unique passwords for all your accounts.";
                case "phishing": return " Never click links in unsolicited emails. Hover over links to see the actual URL first.";
                case "privacy": return " Review your privacy settings on social media at least once a month.";
                case "malware": return " Keep your antivirus software updated and run weekly scans.";
                default: return " Stay vigilant and always think before clicking links or sharing information.";
            }
        }

        public string GetDetailedResponse(string topic)
        {
            if (_detailedResponses.ContainsKey(topic))
                return _detailedResponses[topic];
            return string.Empty;
        }

        public string GetFollowUpResponse(string topic)
        {
            List<string> followUps = new List<string>
            {
                $"Would you like to learn more about {topic}? I can share advanced tips!",
                $"That's just the basics of {topic}.",
                $"There's much more to know about {topic}. Shall I continue?",
                $"I have more {topic} tips if you're interested in learning further!"
            };
            return followUps[_random.Next(followUps.Count)];
        }

        public string GetDefaultResponse()
        {
            List<string> defaults = new List<string>
            {
                "I'm not sure I understand. Can you try rephrasing?",
                "Could you please ask about cybersecurity topics like passwords, phishing, or privacy?",
                "I specialize in cybersecurity education. Would you like tips on password safety, phishing protection, or online privacy?",
                "Let me help you with cybersecurity! Try asking about 'password safety', 'phishing tips', or 'privacy protection'."
            };
            return defaults[_random.Next(defaults.Count)];
        }
    }
}
