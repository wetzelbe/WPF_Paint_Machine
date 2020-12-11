using System;
using System.Collections.Generic;
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

namespace WPF_Paint_Machine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point pt1 = new Point();
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Input.Stylus.AddStylusDownHandler(canvas1, new StylusDownEventHandler(StylusdownHandler));
            System.Windows.Input.Stylus.AddStylusMoveHandler(canvas1, new StylusEventHandler(StylusmoveHandler));
        }

        void StylusdownHandler(object sender, StylusDownEventArgs e)
        {
            Console.WriteLine("Stylus is down!!");
            foreach (StylusPoint _styluspoint in e.GetStylusPoints(this.canvas1))
            {
                
                Line _line = new Line();
                _line.Stroke = System.Windows.Media.Brushes.Black;

                _line.X1 = _styluspoint.X;
                _line.X2 = _styluspoint.X + 3;
                _line.Y1 = _styluspoint.Y;
                _line.Y2 = _styluspoint.Y + 3;
                canvas1.Children.Add(_line);

                APIHandler.UpdateData(new Point(_styluspoint.X, _styluspoint.Y), true);
            }
        }
    void StylusmoveHandler(object sender, StylusEventArgs e)
        {
            Console.WriteLine("Stylus is down!!");
            foreach (StylusPoint _styluspoint in e.GetStylusPoints(this.canvas1))
            {
                
                Line _line = new Line();
                _line.Stroke = System.Windows.Media.Brushes.Black;

                _line.X1 = _styluspoint.X;
                _line.X2 = _styluspoint.X + 3;
                _line.Y1 = _styluspoint.Y;
                _line.Y2 = _styluspoint.Y + 3;
                canvas1.Children.Add(_line);
                APIHandler.UpdateData(new Point(_styluspoint.X, _styluspoint.Y), true);
            }
        }

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            Console.WriteLine("Frame Reported!!");
            if (this.canvas1 != null)
            {
                foreach (TouchPoint _touchPoint in e.GetTouchPoints(this.canvas1))
                {
                    if (_touchPoint.Action == TouchAction.Down)
                    {
                        // Clear the canvas and capture the touch to it.
                        this.canvas1.Children.Clear();
                        _touchPoint.TouchDevice.Capture(this.canvas1);
                    }

                    else if (_touchPoint.Action == TouchAction.Move && e.GetPrimaryTouchPoint(this.canvas1) != null)
                    {
                        // This is the first (primary) touch point. Just record its position.
                        if (_touchPoint.TouchDevice.Id == e.GetPrimaryTouchPoint(this.canvas1).TouchDevice.Id)
                        {
                            pt1.X = _touchPoint.Position.X;
                            pt1.Y = _touchPoint.Position.Y;
                            Line _line = new Line();
                            _line.Stroke = System.Windows.Media.Brushes.Black;

                            _line.X1 = pt1.X;
                            _line.X2 = pt1.X + 3;
                            _line.Y1 = pt1.Y;
                            _line.Y2 = pt1.Y + 3;
                            canvas1.Children.Add(_line);

                        }
                    }

                    else if (_touchPoint.Action == TouchAction.Up)
                    {
                        // If this touch is captured to the canvas, release it.
                        if (_touchPoint.TouchDevice.Captured == this.canvas1)
                        {
                            //this.canvas1.ReleaseTouchCapture(_touchPoint.TouchDevice);
                        }
                    }
                }
            }
        }

    }


}
