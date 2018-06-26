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

            Model = new StewartPlatform();
            Model.BasePlatform.Radius = 1.5;
            Model.BasePlatform.InitJoints(10);
            Model.WorkPlatform.Radius = 1;
            Model.WorkPlatform.Origin = new Optinav.Mathlib.Vector3D(0, 0, 2);
            Model.WorkPlatform.InitJoints(50);


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
    }
}
