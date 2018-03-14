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
//------------------------------------------------
namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> Tasks { get; set; }
        //------------------------------------------------    
        public MainWindow()
        {
            InitializeComponent();

            Tasks = new ObservableCollection<Task>();
        } 
        //------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "" || cbPriority.Text == "" || dpDeadline.Text == "")
            {
                tbName.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Fill Name, Priority, Deadline");
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
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
//------------------------------------------------