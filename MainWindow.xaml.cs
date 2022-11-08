using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
namespace ChatApp
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        string[] Scopes = { GmailService.Scope.GmailSend };
        string ApplicationName = "ChatApp";
        
        public MainWindow()
        {
            InitializeComponent();

            
            


        }

        





        string Base64UrlEncode(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void buttonOdesli_Click(object sender, RoutedEventArgs e)
        
            {
           
            UserCredential credential;
            //cteni json souboru
            using (FileStream stream = new FileStream(Environment.CurrentDirectory + @"/credentials.json", FileMode.Open, FileAccess.Read)) 
            {
                    string soubor = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    soubor = Path.Combine(soubor, ".credentials/gmail-dotnet-quickstart.json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(soubor, true)).Result;
                }
                try
                {
                    string message = $"To: {textBoxPrijemce.Text}\r\nSubject: {textBoxPredmet.Text}\r\nContent-Type: text/html;charset=utf-8\r\n\r\n<h1>{textBoxText.Text}</h1>";
                    //Gmail
                    var service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = ApplicationName });
                    var msg = new Google.Apis.Gmail.v1.Data.Message();
                    msg.Raw = Base64UrlEncode(message.ToString());
                    service.Users.Messages.Send(msg, "me").Execute();
                lbl1.Content = "Email byl odeslán";
                


            }
                catch
                {
                MessageBox.Show("Email nebyl odeslán !");
                }
            }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
