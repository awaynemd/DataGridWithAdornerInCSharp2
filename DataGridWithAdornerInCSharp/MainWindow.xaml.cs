using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

namespace DataGridWithAdornerInCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel();
        }

        private AdornerLayer _adornerLayer;
        private DataGridAnnotationAdorner _adorner;

        private void ListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AdornerClose();

            var s = (ListView)e.Source;
            Grid fe = (Grid)s.Parent;
            var selecteditem = (InnerRow)s.SelectedItem;

            _adorner = new DataGridAnnotationAdorner(fe, selecteditem, DataContext);

            InstallAdorner(fe, _adorner);
        }
        private void AdornerClose()
        {
            if (_adorner != null)
            {
                _adorner.Control = null;

                _adornerLayer.Remove(_adorner);
                _adornerLayer = null;
                _adorner = null;
            }
        }
        private void InstallAdorner(FrameworkElement fe, Adorner adorner)
        {
            _adornerLayer = AdornerLayer.GetAdornerLayer(fe);

            if (_adornerLayer == null)
            {

                Dispatcher.BeginInvoke(
                    DispatcherPriority.Loaded,
                    new Action(() => InstallAdorner(fe, adorner)));
                return;
            }

            if (_adornerLayer == null)
                throw new ArgumentException("datagrid does not have have an adorner layer.");

            _adornerLayer.Add(adorner);
        }

    }
}

