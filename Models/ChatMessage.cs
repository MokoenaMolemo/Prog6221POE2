using System;
using System.Windows.Media;

namespace CybersecurityChatbotWPF.Models
{
    public class ChatMessage
    {
        public string Text { get; set; }
        public bool IsUser { get; set; }
        public DateTime Timestamp { get; set; }

        public string SenderName => IsUser ? "User" : "ChatBot";
        public string AvatarIcon => IsUser ? "👤" : "🤖";

        
        public SolidColorBrush AvatarColor => IsUser ?
            new SolidColorBrush(Color.FromRgb(0, 102, 204)) :
            new SolidColorBrush(Color.FromRgb(100, 100, 100)); 

        public SolidColorBrush BubbleColor => IsUser ?
            new SolidColorBrush(Color.FromRgb(230, 230, 230)) : 
            new SolidColorBrush(Color.FromRgb(245, 245, 245)); 

        public SolidColorBrush TextColor => IsUser ?
            new SolidColorBrush(Color.FromRgb(0, 0, 0)) :
            new SolidColorBrush(Color.FromRgb(0, 0, 0)); 

        public ChatMessage(string text, bool isUser)
        {
            Text = text;
            IsUser = isUser;
            Timestamp = DateTime.Now;
        }
    }
}
