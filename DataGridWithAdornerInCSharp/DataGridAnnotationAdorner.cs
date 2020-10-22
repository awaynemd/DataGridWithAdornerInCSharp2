using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace DataGridWithAdornerInCSharp
{
    public class DataGridAnnotationAdorner : Adorner
    {
        private ArrayList _logicalChildren;
        private Point _location;
        public DataGridAnnotationControl Control { get; set; }

        public DataGridAnnotationAdorner(Grid adornedDataGrid, InnerRow visit, object dc)
            : base(adornedDataGrid)
        {
            Control = new DataGridAnnotationControl();

            Binding cmd2 = new Binding("SaveAppointmentCommand");
            cmd2.Source = dc;
            BindingOperations.SetBinding(Control, DataGridAnnotationControl.SaveAppointmentCommandProperty, cmd2);

            Binding myBinding1 = new Binding("LastName");
            myBinding1.Source = dc;
            myBinding1.Mode = BindingMode.TwoWay;
            myBinding1.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(Control, DataGridAnnotationControl.LastNameProperty, myBinding1);

            Binding myBinding5 = new Binding("Visit");
            myBinding5.Source = dc;
            myBinding5.Mode = BindingMode.TwoWay;
            myBinding5.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(Control, DataGridAnnotationControl.VisitProperty, myBinding5);

            AddLogicalChild(Control);
            AddVisualChild(Control);

            Control.Visit = visit;
        }

        #region Measure/Arrange      
        protected override System.Windows.Size MeasureOverride(Size constraint)
        {
            Control.Measure(constraint);
            return Control.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double xloc = (((Grid)AdornedElement).ActualWidth - Control.DesiredSize.Width) / 2;
            double yloc = (((Grid)AdornedElement).ActualHeight - Control.DesiredSize.Height) / 2;

            _location = new Point(xloc, yloc);

            Rect rect = new Rect(_location, finalSize);

            Control.Arrange(rect);

            return finalSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            SolidColorBrush screenBrush_ = new SolidColorBrush();
            screenBrush_.Color = Colors.Crimson;
            screenBrush_.Opacity = 0.3;
            screenBrush_.Freeze();

            drawingContext.DrawRectangle(screenBrush_, null, WindowRect());

            base.OnRender(drawingContext);
        }


        private Rect WindowRect()
        {
            return new Rect(new Point(0, 0), AdornedElement.DesiredSize);
        }

        #endregion 

        #region [Visual Children]
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException("index");

            return Control;
        }

        #endregion

        #region Logical Children
        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (_logicalChildren == null)
                {
                    _logicalChildren = new ArrayList();
                    _logicalChildren.Add(Control);
                }

                return _logicalChildren.GetEnumerator();
            }
        }

        #endregion
    }
}

