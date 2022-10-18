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
        RaceStats raceScreen;
        DriverStats driverScreen;

        public MainWindow() {
            InitializeComponent();
            Data.Initialize(true);
            Data.nextRaceEvent += GUIVisual.OnNextRace;
            Data.nextRaceEvent += this.OnNextRace;
            GUIVisual.drawingReady += this.OnDrawingReady;
            Data.nextRace();
            RaceContext dataContext = new RaceContext();
            Data.currentRace.DriversChanged += dataContext.OnDriversChanged;
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
        public void OnNextRace(object sender, NextRaceArgs e) {
            Data.currentRace.DriversChanged += this.OnDriversChanged;
        }
        public void OnDrawingReady(object sender, NextRaceArgs e) {
            this.MainScreen.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() => {
                this.MainScreen.Source = null;
                this.MainScreen.Source = GUIVisual.drawTrack(e.race.Track);
            }));
            
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
            this.Close();
        }

        private void Menu(object sender, RoutedEventArgs e) {

        }

        private void MenuItem_RaceStat_Click(object sender, RoutedEventArgs e) {
            raceScreen = new RaceStats();
            raceScreen.Show();
        }

        private void MenuItem_DriverStat_Click(object sender, RoutedEventArgs e) {
            driverScreen = new DriverStats();
            driverScreen.Show();
        }
    }
}
