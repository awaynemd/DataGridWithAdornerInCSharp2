using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DataGridWithAdornerInCSharp
{
    public class DataGridAnnotationControl : Control, INotifyPropertyChanged
    {
        static DataGridAnnotationControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataGridAnnotationControl), new FrameworkPropertyMetadata(typeof(DataGridAnnotationControl)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private const string LastNamePartName = "PART_LastName";
        private TextBox _tbLastName;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _tbLastName = GetTemplateChild(LastNamePartName) as TextBox;
        }

        public DataGridAnnotationControl()
        {
            BorderBrush = Brushes.Black;
            Background = Brushes.AliceBlue;
            BorderThickness = new Thickness(20, 20, 20, 20);
        }



        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register("LastName", typeof(string), typeof(DataGridAnnotationControl), new PropertyMetadata(string.Empty));

        public InnerRow Visit
        {
            get { return (InnerRow)GetValue(VisitProperty); }
            set { SetValue(VisitProperty, value); }
        }
        public static readonly DependencyProperty VisitProperty =
            DependencyProperty.Register("Visit", typeof(InnerRow), typeof(DataGridAnnotationControl), new PropertyMetadata(null, (s, e) =>
            {
                var sender = s as DataGridAnnotationControl;
                var visit = (InnerRow)e.NewValue;

                if (visit != null)
                    sender.LastName = visit.LastName;
            }));

        public ICommand SaveAppointmentCommand
        {
            get { return (ICommand)GetValue(SaveAppointmentCommandProperty); }
            set { SetValue(SaveAppointmentCommandProperty, value); }
        }
        public static DependencyProperty SaveAppointmentCommandProperty = DependencyProperty.Register("SaveAppointmentCommand", typeof(ICommand), typeof(DataGridAnnotationControl));


    }

}

