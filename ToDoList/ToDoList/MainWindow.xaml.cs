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
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
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

            dpDeadline.DisplayDateStart = DateTime.Now.Date;
            dpDeadline.DisplayDate = DateTime.Now.Date;

            alertCol = new SolidColorBrush(Colors.Red);
            standCol = new SolidColorBrush(Colors.Black);

            settings = new XmlWriterSettings();
            settings.Indent = true;
        } 
        //------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "" || cbPriority.Text == "")
            {
                tbName.BorderBrush = alertCol;
                cbPriority.BorderBrush = alertCol;
                MessageBox.Show("Fill Name, Priority", "Warning!", MessageBoxButton.OK);
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
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        //------------------------------------------------
        private void tbName_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            tbName.BorderBrush = standCol;
        }
        //------------------------------------------------
        private void cbPriority_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            cbPriority.BorderBrush = standCol;
        }
    }
}
//------------------------------------------------