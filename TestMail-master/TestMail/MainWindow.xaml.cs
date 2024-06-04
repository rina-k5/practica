using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;

/*План:
  добавить обработку разных исключений на отправку сообщений, например: неверная почта, проблема с соединением 
нужно добавить отправку сообщения с вложениями 
добавление черновика, если программа экстренно завершилась, или письмо не отправилось
возможность выбрать из шаблонов текст сообщения
возможность выбрать почту из базы данных 
возможность выбора почты для отправки сообщения*/
namespace TestMail
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // хост и порт можно узнать в интернете 
        string smtpHost = "smtp.gmail.com";
        int smtpPort = 25;
        string login;
        string password; //пароль для входа в почту, генерируется в настройках, используется протакол IMAP

       
        SmtpClient client;

        string emailSender, emailRecipient, subjectLetter, letter;


        public MainWindow()
        {
            InitializeComponent();


            login = tbEmailSender.Text;
            if (login.EndsWith("yandex.ru"))
            {
                login = "londanv@yandex.ru";
                password = "bbpbdtirrlabjpck"; //пароль для входа в почту, генерируется в настройках, используется протакол IMAP

                smtpHost = "smtp.yandex.ru";
            }
            else if (login.EndsWith("gmail.com"))
            {
                login = "korshunova340@gmail.com";
                password = "Bassfase6d";
                smtpHost = "smtp.gmail.com";
                smtpPort = 587;
            }
            client = new SmtpClient(smtpHost,smtpPort); //подключение
            client.EnableSsl = true; //защитное соединение 
            client.UseDefaultCredentials = false; //использовать учетные данные по умолчанию 
            client.Credentials = new NetworkCredential(login,password);//аунтификация

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            emailSender = tbEmailSender.Text;
            emailRecipient= tbEmailRecipient.Text;
            subjectLetter= tbSubjectLetter.Text;
            letter = tbLetter.Text;
            


            

            MailMessage message = new MailMessage(emailSender, emailRecipient, subjectLetter, letter); //сборка сообщения
            try
            {
                client.Send(message); //отправление сообщения
                MessageBox.Show("Сообщение отправлено!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Что-то пошло не так\nПопробуйте еще раз!\n{ex.Message}");
            }

        }
    }
}
