using _3DVisualization.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3DVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public StewartPlatform Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

            Model = new StewartPlatform(0.5, 10);
            Model.BasePlatform.Radius = 1.5;
            Model.BasePlatform.InitJoints(10);
            Model.WorkPlatform.Radius = 1;
            Model.WorkPlatform.InitJoints(50);
            Model.Move(new Tools.Math.Vector3D(0, 0, 1));

            OnPropertyChanged("Model");
        }

        private void HelixViewport_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HelixViewport.FitView(new Vector3D(1,0,0), new Vector3D(0,0,1), 500);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private void HelixViewport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.NumPad8)
            {
                Model.Move(new Tools.Math.Vector3D(0.1, 0, 0));
                OnPropertyChanged("Model");
            }
            if (e.Key == Key.NumPad4)
            {
                Model.Move(new Tools.Math.Vector3D(0, -0.1, 0));
                OnPropertyChanged("Model");
            }
            if (e.Key == Key.NumPad6)
            {
                Model.Move(new Tools.Math.Vector3D(0, 0.1, 0));
                OnPropertyChanged("Model");
            }
            if (e.Key == Key.NumPad2)
            {
                Model.Move(new Tools.Math.Vector3D(-0.1, 0, 0));
                OnPropertyChanged("Model");
            }
            if (e.Key == Key.NumPad7)
            {
                Model.Move(new Tools.Math.Vector3D(0, 0, -0.1));
                OnPropertyChanged("Model");
            }
            if (e.Key == Key.NumPad9)
            {
                Model.Move(new Tools.Math.Vector3D(0, 0, 0.1));
                OnPropertyChanged("Model");
            }
            if (e.Key == Key.NumPad1)
            {
                Model.Rotate(Tools.Math.Matrix.RotationMatrixXYZ(0, 0, Tools.Math.Misc.DegToRad(5)));
                OnPropertyChanged("Model");
            }
            if (e.Key == Key.NumPad3)
            {
                Model.Rotate(Tools.Math.Matrix.RotationMatrixXYZ(0, 0, Tools.Math.Misc.DegToRad(-5)));
                OnPropertyChanged("Model");
            }
        }
    }
}
