using System.Timers;
using System.Windows.Media;
using TrafficLights.lib.Interfaces;
using TrafficLights.lib.Models.Base;

namespace TrafficLights.lib.Models
{
    public sealed class TrafficLights : TrafficLightsBase, ITrafficLights
    {
        /// <summary>
        /// Фаза ожидания.
        /// </summary>
        public Light AwaitPhase { get; set; }

        /// <summary>
        /// Иницифализация объекта с параметрами по умолчанию.
        /// </summary>
        public TrafficLights() : this (500, Brushes.Red, Brushes.Yellow, Brushes.Green) { }

        /// <summary>
        /// Время тика в мс.
        /// </summary>
        /// <param name="TickSize">Время тика в мс.</param>
        /// <param name="InhibitingPhaseColor">Цвет запрещающего сигнала.</param>
        /// <param name="AwaitPhaseColor">Цвет сигнала ожидания.</param>
        /// <param name="PermissivePhaseColor">Цвет разрешающего сигнала.</param>
        public TrafficLights(double TickSize,
            SolidColorBrush InhibitingPhaseColor,
            SolidColorBrush AwaitPhaseColor,
            SolidColorBrush PermissivePhaseColor)
            : base (TickSize, InhibitingPhaseColor, PermissivePhaseColor)
        {
            AwaitPhase = new Light(AwaitPhaseColor);
        }

        protected override void SetDisableMod()
        {
            base.SetDisableMod();
            Timer timer = new Timer(500);
            timer.AutoReset = true;
            timer.Elapsed += SetFlashingAwaitPhase;
            timer.Start();

            void SetFlashingAwaitPhase(object sender, ElapsedEventArgs e)
            {
                AwaitPhase.Enabled = !AwaitPhase.Enabled;
                if (State != TrafficLightsState.Disabled)
                {
                    timer.Stop();
                    AwaitPhase.Enabled = false;
                }  
            }
        }

        protected override void FromPermissiveToInhibitingPhases(object sender, ElapsedEventArgs e)
        {
            _ElapsedCount++;
            if (_ElapsedCount < 6)
            {
                InhibitingPhase.Enabled = false;
                PermissivePhase.Enabled = !PermissivePhase.Enabled;
            }
            else if (_ElapsedCount < 12)
            {
                PermissivePhase.Enabled = false;
                AwaitPhase.Enabled = true;
            }
            else
            {
                AwaitPhase.Enabled = false;
                InhibitingPhase.Enabled = true;
                (sender as Timer)?.Stop();
                _ElapsedCount = 0;
                _IsPermissive = false;
            }
        }

        protected override void FromInhibitingToPermissivePhases(object sender, ElapsedEventArgs e)
        {
            _ElapsedCount++;
            if (_ElapsedCount < 6)
            {
                PermissivePhase.Enabled = false;
                InhibitingPhase.Enabled = true;
                AwaitPhase.Enabled = true;
            }
            else
            {
                InhibitingPhase.Enabled = false;
                AwaitPhase.Enabled = false;
                PermissivePhase.Enabled = true;
                (sender as Timer)?.Stop();
                _ElapsedCount = 0;
                _IsPermissive = true;
            }
        }

        public override string ToString() => "Транспортный";
    }
}
