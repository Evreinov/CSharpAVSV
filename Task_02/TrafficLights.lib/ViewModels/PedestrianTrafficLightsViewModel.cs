using System;
using System.ComponentModel;
using System.Windows.Media;
using TrafficLights.lib.ViewModels.Base;

namespace TrafficLights.lib.ViewModels
{
    public class PedestrianTrafficLightsViewModel : ViewModelBase
    {
        private readonly Models.PedestrianTrafficLights _Model;

        public PedestrianTrafficLightsViewModel(Models.PedestrianTrafficLights Model)
        {
            _Model = Model ?? throw new ArgumentNullException(nameof(Model));
            _Model.InhibitingPhase.PropertyChanged += OnPropertyChanged;
            _Model.PermissivePhase.PropertyChanged += OnPropertyChanged;

        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_Model.InhibitingPhase.Enabled))
                OnPropertyChanged(nameof(Red));
            if (e.PropertyName == nameof(_Model.PermissivePhase.Enabled))
                OnPropertyChanged(nameof(Green));
        }

        public SolidColorBrush Red { get => _Model.InhibitingPhase.CurrentColor; }

        public SolidColorBrush Green { get => _Model.PermissivePhase.CurrentColor; }
    }
}
