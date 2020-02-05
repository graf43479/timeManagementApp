using System;
using System.Windows;

namespace YaSheduler.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CrUWindow.xaml
    /// </summary>
    public partial class CrUWindow : Window
    {
        private readonly string baseDescription;
        public CrUWindow()
        {
            InitializeComponent();
        }

        public string BaseDescription => baseDescription;

        public CrUWindow(string title, string description = "") 
        {
            InitializeComponent();
            this.Title = title;
            baseDescription = description;
            txtBoxDescription.Text = description;            
        }

        public void SetContent(string content)
        {
            this.Content = content;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //(Application.Current as App).
        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            txtBoxDescription.Text = baseDescription;
            this.Close();
        }
    }
}
