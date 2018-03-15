using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml;
//using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
//------------------------------------------------
namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> Tasks { get; set; }

        SolidColorBrush alertCol;
        SolidColorBrush standCol;

        XmlWriterSettings settings;
        //------------------------------------------------    
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Tasks = new ObservableCollection<Task>();

            //   dpDeadline.DisplayDateStart = DateTime.Now.Date;
             dpDeadline.SelectedDate = DateTime.Now.Date.AddDays(10);

            alertCol = new SolidColorBrush(Colors.Red);
            standCol = new SolidColorBrush(Colors.Black);

            settings = new XmlWriterSettings();
            settings.Indent = true;
        }
        //------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool nameEmpty = false;
            bool priorityEmpty = false;

            if (tbName.Text == "")
            {
                tbName.BorderBrush = alertCol;
                nameEmpty = true;
            }

            if (cbPriority.Text == "")
            {
                bDeadline.BorderBrush = alertCol;
                priorityEmpty = true;
            }

            if (nameEmpty && priorityEmpty)
            {
                MessageBox.Show("Fill Name and Priority", "Warning!", MessageBoxButton.OK);
                return;
            }
            else 
            if (priorityEmpty)
            {
                MessageBox.Show("Fill Priority", "Warning!", MessageBoxButton.OK);
                return;
            }
            else
            if (nameEmpty)
            {
                MessageBox.Show("Fill Name", "Warning!", MessageBoxButton.OK);
                return;
            }

            Tasks.Add(new Task
            {
                Name = tbName.Text,
                Priority = cbPriority.Text,
                Deadline = dpDeadline.Text,
                Comment = tbComment.Text
            });

            LastTaskName.Content = tbName.Text;
            TaskCount.Content = Tasks.Count;

            tbName.Text = "";
            cbPriority.Text = "";
            dpDeadline.Text = "";
            tbComment.Text = "";
        }
        //------------------------------------------------
        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            tbName.BorderBrush = standCol;
        }
        //------------------------------------------------
        private void cbPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bDeadline.BorderBrush = standCol;
        }
        //------------------------------------------------
        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            
        }
        //------------------------------------------------
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }
        //------------------------------------------------
        private void MenuLoad_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }
    }
}
//------------------------------------------------