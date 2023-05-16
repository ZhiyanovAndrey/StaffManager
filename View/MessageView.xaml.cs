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
using System.Windows.Shapes;

namespace StaffManager.View
{
    /// <summary>
    /// Логика взаимодействия для MassageView.xaml
    /// </summary>
    public partial class MessageView : Window
    {
        public MessageView(string text) // прилетит из конструктора в VM, задаем параметры конструктора прямо здесь
        {
            InitializeComponent();
            MessageText.Text = text; // передаст текст в TextBlock x:Name="MessageText
        }

        private void Button_Click(object sender, RoutedEventArgs e) // можно делать через Комманд, но сейчас это не обязательно
        {
            this.Close();
        }
    }
}
