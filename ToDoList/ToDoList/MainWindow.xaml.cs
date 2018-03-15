using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Linq;
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

        SaveFileDialog saveFileDialog;
        OpenFileDialog openFileDialog;
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

            saveFileDialog = new SaveFileDialog();
            openFileDialog = new OpenFileDialog();

            saveFileDialog.Filter = "Xml files (.xml)|*.xml";
            openFileDialog.Filter = "Xml files (.xml)|*.xml";
        }
        //------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "")
            {
                tbName.BorderBrush = alertCol;
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
            tbComment.Text = "";
        }
        //------------------------------------------------
        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            tbName.BorderBrush = standCol;
        }
        //------------------------------------------------
        void SaveToXml()
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                var xmldoc = new XDocument(new XElement("Tasks")); //new XDeclaration("1.0", "utf-8", "yes")

                foreach (var item in Tasks)
                {
                    xmldoc.Root.Add(new XElement("task", new XElement("name", item.Name),     // new XAttribute("Id", item.Id)
                                                        new XElement("priority", item.Priority),
                                                        new XElement("deadline", item.Deadline),
                                                        new XElement("comment", item.Comment)));
                }

                xmldoc.Save(saveFileDialog.FileName);
            }
        }
        //------------------------------------------------
        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.Count != 0)
            {
                SaveToXml();
                Tasks.Clear();
            }
        }
        //------------------------------------------------
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.Count != 0)
                SaveToXml();

            //    XmlTextWriter writer = new XmlTextWriter(saveFileDialog.FileName, Encoding.UTF8);

            //    writer.WriteStartElement("Tasks");

            //    foreach (var item in Tasks)
            //    {
            //        writer.WriteStartElement("task");

            //        writer.WriteAttributeString("name", item.Name);
            //        writer.WriteAttributeString("priority", item.Priority);
            //        writer.WriteAttributeString("deadline", item.Deadline);
            //        writer.WriteAttributeString("comment", item.Comment);

            //        writer.WriteEndElement();
            //    }

            //    writer.WriteEndElement();
            //    writer.Close();
        }
        //------------------------------------------------
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.Count != 0)
            {
                SaveToXml();
                Tasks.Clear();
            }

            if (openFileDialog.ShowDialog() == true)
            {

            }
        }
        //------------------------------------------------
        private void lbTasks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string info;

                info = "Name:\t\t" + Tasks[0].Name + '\n';
                info += "Priority:\t\t" + Tasks[0].Priority + '\n';
                info += "Deadline:\t" + Tasks[0].Deadline + '\n';
                info += "Comment:\t" + Tasks[0].Comment + '\n';

                MessageBox.Show(info);
            }
        }
    }
}
//------------------------------------------------