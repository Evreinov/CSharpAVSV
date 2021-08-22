using System.Linq;
using System.Windows;
using System.Windows.Input;
using TrafficLights.Infrastructure.Commands;
using TrafficLights.lib.ViewModels.Base;
using System.Collections.ObjectModel;
using TrafficLights.lib.Interfaces;
using TrafficLights.lib.Models;
using TrafficLights.lib.ViewModels;

namespace TrafficLights.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private string _Title = "Светофор";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private object _Content;

        public object Content
        {
            get => _Content;
            set => Set(ref _Content, value);
        }

        private ObservableCollection<ITrafficLights> _TrafficLights;

        public ObservableCollection<ITrafficLights> TrafficLights
        {
            get => _TrafficLights;
            set => Set(ref _TrafficLights, value);
        }

        private ITrafficLights _SelectedTrafficLights;

        public ITrafficLights SelectedTrafficLights
        {
            get => _SelectedTrafficLights;
            set
            {
                if (value is PedestrianTrafficLights)
                {
                    Content = new PedestrianTrafficLightsViewModel((lib.Models.PedestrianTrafficLights)value);
                }
                else if (value is lib.Models.TrafficLights)
                {
                    Content = new TrafficLightsViewModel((lib.Models.TrafficLights)value);
                }
                Set(ref _SelectedTrafficLights, value);
            }
        }

        public MainWindowViewModel()
        {
            TrafficLights = new ObservableCollection<ITrafficLights>
            {
                new lib.Models.TrafficLights(),
                new PedestrianTrafficLights()
            };
        }

        #region Commands

        #region CloseWindowCommand
        private ICommand _CloseWindowCommand;

        public ICommand CloseWindowCommand => _CloseWindowCommand
            ??= new RelayCommand(OnCloseWindowCommandExecuted);

        private void OnCloseWindowCommandExecuted(object p)
        {
            if (p is not Window window)
            {
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);
            }

            if (window is null)
            {
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);
            }

            window?.Close();
        }
        #endregion

        #region SwitchToPermissivePhase

        private ICommand _SwitchToPermissivePhaseCommand;

        public ICommand SwitchToPermissivePhaseCommand => _SwitchToPermissivePhaseCommand
            ??= new RelayCommand(OnSwitchToPermissivePhaseExecute, CanSwitchToPermissivePhaseExecute);

        private bool CanSwitchToPermissivePhaseExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            return trafficLights is not null && trafficLights?.State == TrafficLightsState.Enabled;
        }

        private void OnSwitchToPermissivePhaseExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            if (trafficLights is null)
                return;
            trafficLights.SwitchToPermissivePhase();
        }

        #endregion

        #region SwitchToInhibitingPhase
        private ICommand _SwitchToInhibitingPhaseCommand;

        public ICommand SwitchToInhibitingPhaseCommand => _SwitchToInhibitingPhaseCommand
            ??= new RelayCommand(OnSwitchToInhibitingPhaseExecute, CanSwitchToInhibitingPhaseExecute);

        private bool CanSwitchToInhibitingPhaseExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            return trafficLights is not null && trafficLights?.State == TrafficLightsState.Enabled;
        }

        private void OnSwitchToInhibitingPhaseExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            if (trafficLights is null)
                return;
            trafficLights.SwitchToInhibitingPhase();
            //MessageBox.Show($"Включить красный на {trafficLights}", "Состояние светофора!");
        }
        #endregion

        #region Disabled

        private ICommand _DisabledCommand;

        public ICommand DisabledCommand => _DisabledCommand
            ??= new RelayCommand(OnDisabledExecute, CanDisabledExecute);

        private bool CanDisabledExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            return trafficLights is not null && trafficLights?.State != TrafficLightsState.Disabled;
        }

        private void OnDisabledExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            if (trafficLights is null)
                return;
            trafficLights.State = TrafficLightsState.Disabled;
            //MessageBox.Show($"{trafficLights} - выключен", "Состояние светофора!");
        }

        #endregion

        #region Enabled

        private ICommand _EnabledCommand;

        public ICommand EnabledCommand => _EnabledCommand
            ??= new RelayCommand(OnEnabledExecute, CanEnabledExecute);

        private bool CanEnabledExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            return trafficLights is not null && trafficLights?.State != TrafficLightsState.Enabled;
        }

        private void OnEnabledExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            if (trafficLights is null)
                return;
            trafficLights.State = TrafficLightsState.Enabled;
            //MessageBox.Show($"{trafficLights} - включен", "Состояние светофора!");
        }

        #endregion

        #region Automatic

        private ICommand _AutomaticCommand;

        public ICommand AutomaticCommand => _AutomaticCommand
            ??= new RelayCommand(OnAutomaticExecute, CanAutomaticExecute);

        private bool CanAutomaticExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            return trafficLights is not null && trafficLights?.State != TrafficLightsState.Automatic;
        }

        private void OnAutomaticExecute(object p)
        {
            var trafficLights = p as ITrafficLights ?? SelectedTrafficLights;
            if (trafficLights is null)
                return;
            trafficLights.State = TrafficLightsState.Automatic;
            //MessageBox.Show($"{trafficLights} - автоматический режим.", "Состояние светофора!");
        }

        #endregion

        #endregion

    }
}
