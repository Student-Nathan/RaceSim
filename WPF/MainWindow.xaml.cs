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
using System.Xml;
using Controller;
using Model;


namespace WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        RaceStats? raceScreen;
        CompetitionStats? driverScreen;
        RaceContext? raceContext;
        CompetitionContext? competitionContext;
        RaceStatsContext? raceStatsContext;

        public MainWindow() {
            InitializeComponent();
            Data.Initialize(true);
            Data.NextRaceEvent += GUIVisual.OnNextRace;
            Data.NextRaceEvent += this.OnNextRace;
            competitionContext = new CompetitionContext();
            raceStatsContext = new RaceStatsContext();
            Data.NextRaceEvent += competitionContext.OnNextRace;
            Data.NextRaceEvent += raceStatsContext.OnUpdatedStats;


            //GUIVisual.drawingReady += this.OnDrawingReady;
            raceContext = new RaceContext();
            NameLabel.DataContext = raceContext;
            Data.NextRace();


            //Data.currentRace.DriversChanged += raceContext.OnDriversChanged;
            //Data.currentRace.DriversChanged += this.OnDriversChanged;
        }

        public void OnDriversChanged(object? sender, DriversChangedEventArgs e) {
            this.MainScreen.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() => {
                this.MainScreen.Source = null;
                this.MainScreen.Source = GUIVisual.drawTrack(e.Track);
            }));
        }
        public void OnNextRace(object? sender, NextRaceArgs e) {
            if (Data.CurrentRace is not null) {
                Data.CurrentRace.DriversChanged += this.OnDriversChanged;
            } else {
                throw new NullReferenceException("Error: Data.currentRace is null");
            }
            if (raceContext is not null) {
                Data.CurrentRace.DriversChanged += raceContext.OnDriversChanged;
            } else {
                throw new NullReferenceException("Error: raceContext is null");
            }
            if (raceStatsContext is not null) {
                Data.CurrentRace.ParticipantFinished += raceStatsContext.OnNextLap;
            } else {
                throw new NullReferenceException("Error: raceStatsContext is null");
            }
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
            if (raceStatsContext is not null) {
                raceScreen = new RaceStats(raceStatsContext);
            } else {
                throw new NullReferenceException("Error: raceStatsContext is null");
            }
            raceScreen.Show();
        }

        private void MenuItem_DriverStat_Click(object sender, RoutedEventArgs e) {
            if (competitionContext is not null) {
                driverScreen = new CompetitionStats(competitionContext);
            } else {
                throw new NullReferenceException("Error: competitionContext is null");
            }
            driverScreen.Show();
        }
    }
}
