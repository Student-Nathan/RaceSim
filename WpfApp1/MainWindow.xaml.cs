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
using System.Windows.Threading;
using Controller;
using Model;
using WPF;

namespace WpfApp1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Data.Initialize();
            Data.nextRaceEvent += GUIVisual.OnNextRace;
            Data.nextRace();
            Data.currentRace.DriversChanged += this.OnDriversChanged;

        }

        public void OnDriversChanged(object sender, DriversChangedEventArgs e) {
            this.MainScreen.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() => {
                this.MainScreen.Source = null;
                this.MainScreen.Source = GUIVisual.drawTrack(e.Track);
            }));
        }
    }
}
