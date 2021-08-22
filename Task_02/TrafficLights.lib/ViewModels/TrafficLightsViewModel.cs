using System;
using System.ComponentModel;
using System.Windows.Media;
using TrafficLights.lib.Models;
using TrafficLights.lib.ViewModels.Base;

namespace TrafficLights.lib.ViewModels
{
    public class TrafficLightsViewModel : ViewModelBase
    {
        private readonly Models.TrafficLights _Model;

        public TrafficLightsViewModel(Models.TrafficLights Model)
        {
            _Model = Model ?? throw new ArgumentNullException(nameof(Model));
            _Model.InhibitingPhase.PropertyChanged += OnPropertyChanged;
            _Model.AwaitPhase.PropertyChanged += OnPropertyChanged;
            _Model.PermissivePhase.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_Model.InhibitingPhase.Enabled))
                OnPropertyChanged(nameof(Red));
            if (e.PropertyName == nameof(_Model.AwaitPhase.Enabled))
                OnPropertyChanged(nameof(Yellow));
            if (e.PropertyName == nameof(_Model.PermissivePhase.Enabled))
                OnPropertyChanged(nameof(Green));
            if (e.PropertyName == nameof(_Model.State))
                OnPropertyChanged(nameof(State));
        }

        public TrafficLightsState State
        {
            get => _Model.State;
            set => _Model.State = value;
        }

        public SolidColorBrush Red { get => _Model.InhibitingPhase.CurrentColor; }

        public SolidColorBrush Yellow { get => _Model.AwaitPhase.CurrentColor; }

        public SolidColorBrush Green { get => _Model.PermissivePhase.CurrentColor; }
    }
}
