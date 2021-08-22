using System.Timers;
using System.Windows.Media;
using TrafficLights.lib.Interfaces;
using TrafficLights.lib.Models.Base;

namespace TrafficLights.lib.Models
{
    public sealed class PedestrianTrafficLights : TrafficLightsBase, ITrafficLights
    {
        public PedestrianTrafficLights() : base(500, Brushes.Red, Brushes.Green) { }

        protected override void FromInhibitingToPermissivePhases(object sender, ElapsedEventArgs e)
        {
            InhibitingPhase.Enabled = false;
            PermissivePhase.Enabled = true;
            (sender as Timer)?.Stop();
            _IsPermissive = true;
        }

        protected override void FromPermissiveToInhibitingPhases(object sender, ElapsedEventArgs e)
        {
            _ElapsedCount++;
            if (_ElapsedCount < 6)
            {
                InhibitingPhase.Enabled = false;
                PermissivePhase.Enabled = !PermissivePhase.Enabled;
            }
            else
            {
                InhibitingPhase.Enabled = true;
                PermissivePhase.Enabled = false;
                (sender as Timer)?.Stop();
                _ElapsedCount = 0;
                _IsPermissive = false;
            }
        }

        public override string ToString() => "Пешеходный";
    }
}
