using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YaSheduler.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TokenWindow.xaml
    /// </summary>
    public partial class TokenWindow : Window
    {
        private string token;
        public TokenWindow()
        {
            InitializeComponent();
        }

        public string Token => token;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            token = txtBoxToken.Text;
            if (String.IsNullOrEmpty(token))
            {
                MessageBox.Show("Пустое значение токена");
            }
            else
            {
                this.Close();
            }          
        }
      
    }
}
