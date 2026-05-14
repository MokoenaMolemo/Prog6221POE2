using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CybersecurityChatbotWPF.Audio;
using CybersecurityChatbotWPF.Controllers;
using CybersecurityChatbotWPF.Models;

namespace CybersecurityChatbotWPF
{
    public partial class MainWindow : Window
    {
        private ChatbotController _chatbot = null!;
        private AudioPlayer _audioPlayer = null!;
        private ObservableCollection<ChatMessage> _messages = null!;

        public MainWindow()
        {
            InitializeComponent();
            InitializeChatbot();
        }

        private void InitializeChatbot()
        {
            _messages = new ObservableCollection<ChatMessage>();
            MessagesListBox.ItemsSource = _messages;

            _chatbot = new ChatbotController();
            _audioPlayer = new AudioPlayer();

            // Clear input box 
            if (MessageTextBox != null)
            {
                MessageTextBox.Text = "";
                MessageTextBox.Focus();
            }

            
            DisplayWelcomeMessage();

            // Plays voice greeting
            PlayVoiceGreeting();
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                var audioPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "welcome.wav");
                if (System.IO.File.Exists(audioPath))
                {
                    _audioPlayer?.PlayGreeting(audioPath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Audio error: {ex.Message}");
            }
        }

        private void DisplayWelcomeMessage()
        {
            string welcome = "Hello! Welcome to the Cybersecurity Awareness Bot!\n\n" +
                            "I'm here to help you stay safe online. You can ask me about the following:\n" +
                            "• Password safety\n" +
                            "• Phishing scams\n" +
                            "• Privacy protection\n" +
                            "• Malware prevention\n\n" +
                            "Type 'help' anytime to see all topics!\n\n" +
                            "What would you like to learn about today?";

            AddBotMessage(welcome);
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            await ProcessUserInput();
        }

        private async void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Keyboard.Modifiers != ModifierKeys.Shift)
            {
                e.Handled = true;
                await ProcessUserInput();
            }
        }

        private async Task ProcessUserInput()
        {
            string userInput = MessageTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(userInput))
                return;

            // Add user message to chat
            AddUserMessage(userInput);
            MessageTextBox.Clear();

            // Show typing indicator
            await ShowTypingIndicator(true);

            if (StatusText != null)
                StatusText.Text = "Bot is thinking...";

            // Get response from chatbot
            string response = await _chatbot.ProcessUserInputAsync(userInput);

            // Hide typing indicator
            await ShowTypingIndicator(false);

            // Add bot response
            AddBotMessage(response);

            // Update status
            if (StatusText != null)
                StatusText.Text = "Online - Ready to help you stay safe";

            // Check for exit
            if (userInput.Equals("exit", StringComparison.CurrentCultureIgnoreCase) ||
                userInput.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
            {
                await Task.Delay(1500);
                Application.Current.Shutdown();
            }

            // Refocus on input box
            MessageTextBox.Focus();
        }

        private void AddUserMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    var chatMessage = new ChatMessage(message, true);
                    _messages.Add(chatMessage);
                    ScrollToBottom();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error adding user message: {ex.Message}");
                }
            });
        }

        private void AddBotMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    var chatMessage = new ChatMessage(message, false);
                    _messages.Add(chatMessage);
                    ScrollToBottom();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error adding bot message: {ex.Message}");
                }
            });
        }

        private async Task ShowTypingIndicator(bool show)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                if (TypingIndicator != null)
                {
                    TypingIndicator.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
                }
            });
        }

        private void ScrollToBottom()
        {
            try
            {
                if (MessagesListBox != null && MessagesListBox.Items.Count > 0)
                {
                    MessagesListBox.ScrollIntoView(MessagesListBox.Items[MessagesListBox.Items.Count - 1]);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error scrolling: {ex.Message}");
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            _audioPlayer?.Dispose();
            base.OnClosing(e);
        }
    }
}
