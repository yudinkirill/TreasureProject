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
using TreasureServer.DataBase;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public MainWindow()
        {
            InitializeComponent();
            
            //тип получили данные, пишем их в какой то список
            Notes.Add(new Note() { NoteHeader = "Заголовок 1", NoteText = "авыораыовравыолрарвыло", Pin = true });
            Notes.Add(new Note() { NoteHeader = "Заголовок 2", NoteText = "fsjdfhdskj", Pin = true });
            Notes.Add(new Note() { NoteHeader = "Заголовок 3", NoteText = "gfsjkjsa", Pin = false });

            
            DataContext = this;
            //добавление заметок для п
        }
    }
}
