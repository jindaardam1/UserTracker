using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SingleUsernameTextBox.GotFocus += OnSingleTextBoxGotFocus;
            SingleUsernameTextBox.LostFocus += OnSingleTextBoxLostFocus;
            MultipleUsernameTextBox.GotFocus += OnMultipleTextBoxGotFocus;
            MultipleUsernameTextBox.LostFocus += OnMultipleTextBoxLostFocus;
        }

        private void OnSingleSearchButtonClick(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void OnMultipleSearchButtonClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnSingleTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (SingleUsernameTextBox.Text.Equals("Single username"))
            {
                SingleUsernameTextBox.Text = "";
                SingleUsernameTextBox.Foreground = Brushes.Black;
            }
        }

        private void OnSingleTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (SingleUsernameTextBox.Text.Equals(""))
            {
                SingleUsernameTextBox.Text = "Single username";
                SingleUsernameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333")); ;
            }
        }
        
        private void OnMultipleTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (MultipleUsernameTextBox.Text.Equals("Multiple usernames"))
            {
                MultipleUsernameTextBox.Text = "";
                MultipleUsernameTextBox.Foreground = Brushes.Black;
            }
        }

        private void OnMultipleTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (MultipleUsernameTextBox.Text.Equals(""))
            {
                MultipleUsernameTextBox.Text = "Multiple usernames";
                MultipleUsernameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333")); ;
            }
        }
    }
}