using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using User;
using UserTracker.NewFolder;

namespace UserTracker.view
{
    /// <summary>
    /// Lógica de interacción para UserAccounts.xaml
    /// </summary>
    public partial class UserAccounts : Window
    {
        public List<string> Usernames { get; set; }
        public ConcurrentDictionary<string, UsernameInfo> UsernamesInfo { get; set; }

        public UserAccounts(List<string> usernames)
        {
            InitializeComponent();

            UsernamesInfo = new ConcurrentDictionary<string, UsernameInfo>();

            this.Usernames = usernames;
            Usernames.Sort();

            foreach (var username in usernames)
            {
                ListBoxUsernames.Items.Add(username);
            }
        }

        private async void OnStartCheckingButtonClick(object sender, RoutedEventArgs e)
        {
            var inputText = ConcurrentTasksTextBox.Text;

            if (int.TryParse(inputText, out int concurrentTasks) && concurrentTasks > 0 && concurrentTasks < 16)
            {
                StartProgressBar();
                StartProgressTextBlock();

                SetElementsAreEnabled(false);

                await Task.Run(() => CheckAsync(concurrentTasks));

                SetElementsAreEnabled(true);
            }
            else
            {
                MessageBox.Show("Error with concurrent tasks (min.: 1) (max.: 15)", "Concurrent tasks", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void StartProgressBar()
        {
            CheckingProgressBar.Maximum = ListBoxUsernames.Items.Count;

            CheckingProgressBar.Visibility = Visibility.Visible;
        }
        
        private void StartProgressTextBlock()
        {
            ProgressNumberTextBlock.Text = "1" + " / " + ListBoxUsernames.Items.Count;

            ProgressNumberTextBlock.Visibility = Visibility.Visible;
        }

        private void SetElementsAreEnabled(bool enabled)
        {
            ListBoxUsernames.IsEnabled = enabled;

            StartCheckingButton.IsEnabled = enabled;

            ConcurrentTasksTextBox.IsEnabled = enabled;
        }

        private async Task CheckAsync(int concurrentTasks)
        {
            var semaphore = new SemaphoreSlim(concurrentTasks);

            var tasks = Usernames.Select(username => ProcessUsernameAsync(username, semaphore));

            await Task.WhenAll(tasks);

            Prueba();
        }

        private async Task ProcessUsernameAsync(string username, SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();

            try
            {
                var hasTwitter = await Task.Run(() => TwitterChecker.CheckTwitterUsername(username));
                var hasInstagram = await Task.Run(() => InstagramChecker.CheckInstagramUsername(username));
                var hasLinkedin = false;

                UsernamesInfo.TryAdd(username, new UsernameInfo(hasTwitter, hasInstagram, hasLinkedin));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing username {username}: {ex.Message}");
            }
            finally
            {
                semaphore.Release();
            }
        }

        private void Prueba()
        {
            foreach (var usernameInfo in UsernamesInfo)
            {
                var u = usernameInfo.Value;
                MessageBox.Show(usernameInfo.Key + " " + u.Twitter.ToString() + u.Instagram.ToString());
            }
        }
    }
}
