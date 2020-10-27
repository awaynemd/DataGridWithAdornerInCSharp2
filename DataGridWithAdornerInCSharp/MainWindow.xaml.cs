using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace DataGridWithAdornerInCSharp
{
    public partial class MainWindow : Window
    {
        private ViewModel ViewModel { get; }

        private Action _closeAdorner = () => { };
        
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new ViewModel();
            DataContext = ViewModel;
        }
        
        private void ListView_PreviewMouseLeftButtonUp(object _, MouseButtonEventArgs e)
        {
            _closeAdorner();
        
            var listView = (ListView)e.Source;
            var grid = (Grid)listView.Parent;
            var selecteditem = (InnerRow)listView.SelectedItem;
            ViewModel.Visit = selecteditem;
            ViewModel.LastName = selecteditem.LastName;
        
            var adornerLayer = AdornerLayer.GetAdornerLayer(grid);
            if (adornerLayer == null)
                throw new ArgumentException("datagrid does not have have an adorner layer");

            var adorner = new DataGridAnnotationAdorner(grid);
            adornerLayer.Add(adorner);
        
           _closeAdorner = () => adornerLayer.Remove(adorner);
        }

    }
}