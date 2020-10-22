using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DataGridWithAdornerInCSharp
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            Rows = new ObservableCollection<Row>()
            {
                new Row(1),
                new Row(2)
            };
        }

        public ObservableCollection<Row> _rows;
        public ObservableCollection<Row> Rows
        {
            get { return _rows; }
            set { if (_rows == value) return; _rows = value; OnPropertyChanged(); }
        }

        public string LastName { get; set; }

        public InnerRow Visit { get; set; }

        public RelayCommand SaveAppointmentCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    var me = string.Format("My Name is {0}....I Am Saved!", LastName);
                    var r = MessageBox.Show(me);
                }, o => true);
            }
        }

    }

    public class Row : ViewModelBase
    {
        public Row(int k)
        {
            Columns = new ObservableCollection<Column>()
            {
                new Column(k*100 + 1),
                new Column(k*100 + 2)
            };
        }

        public ObservableCollection<Column> _columns;
        public ObservableCollection<Column> Columns
        {
            get { return _columns; }
            set { if (_columns == value) return; _columns = value; OnPropertyChanged(); }
        }

    }

    public class Column : ViewModelBase
    {
        public Column(int j)
        {
            InnerRows = new ObservableCollection<InnerRow>()
            {
                new InnerRow(j*10 + 1),
                new InnerRow(j*10 + 2)
            };
        }

        public ObservableCollection<InnerRow> _innerRows;
        public ObservableCollection<InnerRow> InnerRows
        {
            get { return _innerRows; }
            set { if (_innerRows == value) return; _innerRows = value; OnPropertyChanged(); }
        }

        public InnerRow _selectedInnerRow;
        public InnerRow SelectedInnerRow
        {
            get { return _selectedInnerRow; }
            set { if (_selectedInnerRow == value) return; _selectedInnerRow = value; OnPropertyChanged(); }
        }

        // The Visit is bound to DataGridAnnotationControl.VisitProperty from the DataGridAnnotationAdorner.
        public InnerRow Visit { get; set; }

    }

    public class InnerRow : ViewModelBase
    {
        public InnerRow(int i)
        {
            LastName = "MyName" + "_" + i.ToString();
        }

        public string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { if (_lastName == value) return; _lastName = value; OnPropertyChanged(); }
        }

    }


}
