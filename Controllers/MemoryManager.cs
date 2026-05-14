using CybersecurityChatbotWPF.Models;

namespace CybersecurityChatbotWPF.Controllers
{
    public class MemoryManager
    {
        private UserProfile _userProfile;

        public MemoryManager()
        {
            _userProfile = new UserProfile();
        }

        public void RememberUserInfo(string key, string value)
        {
            switch (key.ToLower())
            {
                case "name":
                    _userProfile.Name = value;
                    break;
                case "topic":
                case "interest":
                    _userProfile.FavoriteTopic = value;
                    break;
                default:
                    if (!_userProfile.UserPreferences.ContainsKey(key))
                        _userProfile.UserPreferences.Add(key, value);
                    else
                        _userProfile.UserPreferences[key] = value;
                    break;
            }
        }

        public string RecallUserInfo(string key)
        {
            return key.ToLower() switch
            {
                "name" => _userProfile.Name,
                "topic" or "interest" => _userProfile.FavoriteTopic,
                _ => _userProfile.UserPreferences.ContainsKey(key) ? _userProfile.UserPreferences[key] : string.Empty
            };
        }

        public string GetPersonalizedResponse(string baseResponse)
        {
            if (!string.IsNullOrEmpty(_userProfile.Name))
            {
                if (baseResponse.Contains("you") || baseResponse.Contains("your"))
                {
                    baseResponse = baseResponse.Replace("you", _userProfile.Name);
                    baseResponse = baseResponse.Replace("your", $"{_userProfile.Name}'s");
                }
            }

            if (!string.IsNullOrEmpty(_userProfile.FavoriteTopic))
            {
                baseResponse += $"\n\n💡 Since you're interested in {_userProfile.FavoriteTopic}, would you like more specific tips on this topic?";
            }

            return baseResponse;
        }

        public UserProfile GetUserProfile() => _userProfile;

        public void AddDiscussedTopic(string topic) => _userProfile.AddDiscussedTopic(topic);
    }
}