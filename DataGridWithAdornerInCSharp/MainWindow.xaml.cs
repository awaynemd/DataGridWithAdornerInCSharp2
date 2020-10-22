using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace DataGridWithAdornerInCSharp
{
    public partial class MainWindow : Window
    {
        private Action _closeAdorner = () => { };
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
        
        private void ListView_PreviewMouseLeftButtonUp(object _, MouseButtonEventArgs e)
        {
            _closeAdorner();
        
            var listView = (ListView)e.Source;
            var grid = (Grid)listView.Parent;
            var selecteditem = (InnerRow)listView.SelectedItem;
        
            var adornerLayer = AdornerLayer.GetAdornerLayer(grid);
            if (adornerLayer == null)
                throw new ArgumentException("datagrid does not have have an adorner layer");
        
            var adorner = new DataGridAnnotationAdorner(grid, selecteditem, DataContext);
            adornerLayer.Add(adorner);
        
           _closeAdorner = () => adornerLayer.Remove(adorner);
        }

    }
}