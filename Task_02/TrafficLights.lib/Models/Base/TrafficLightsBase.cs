using System.ComponentModel;
using System.Timers;
using System.Windows.Media;
using TrafficLights.lib.Interfaces;

namespace TrafficLights.lib.Models.Base
{
    /// <summary>
    /// Базовый класс светофора.
    /// </summary>
    public abstract class TrafficLightsBase : ITrafficLights, INotifyPropertyChanged
    {
        /// <summary>
        /// Таймер автоматического переключения светофора.
        /// </summary>
        private readonly Timer _AutoTimer;

        /// <summary>
        /// Счетчик (тикер). 
        /// Перехода из разрешающей фазы в запрещающую.
        /// </summary>
        private readonly Timer _TimerFromPermissiveToInhibiting;

        /// <summary>
        /// Счетчик (тикер). 
        /// Перехода из запрещающей фазы в разрешающую.
        /// </summary>
        private readonly Timer _TimerFromInhibitingToPermissive;

        /// <summary>
        /// true - разрешающая фаза;
        /// false - запрещающая фаза;
        /// </summary>
        protected bool _IsPermissive;

        /// <summary>
        /// Количество пройденных интераций.
        /// </summary>
        protected int _ElapsedCount;
        
        // Состояние светофора.
        private TrafficLightsState _State;

        /// <summary>
        /// Срабатывает при изменении свойств модели.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Режим светофора.
        /// </summary>
        public TrafficLightsState State
        {
            get => _State;
            set
            {
                if (value == TrafficLightsState.Automatic)
                {
                    _IsPermissive = true;
                    PermissivePhase.Enabled = true;
                    _AutoTimer.Start();
                }   
                if (value == TrafficLightsState.Disabled)
                    SetDisableMod();
                _State = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
            }
        }

        /// <summary>
        /// Запрещающая фаза.
        /// </summary>
        public Light InhibitingPhase { get; set; }

        /// <summary>
        /// Разрешающая фаза.
        /// </summary>
        public Light PermissivePhase { get; set; }

        /// <summary>
        /// Инициализация светофора.
        /// </summary>
        public TrafficLightsBase() : this(500, Brushes.Red, Brushes.Green) { }

        /// <summary>
        /// Инициализация светофора.
        /// </summary>
        /// <param name="TickSize">Время тика в мс.</param>
        public TrafficLightsBase(double TickSize) : this(TickSize, Brushes.Red, Brushes.Green) { }

        /// <summary>
        /// Инициализация светофора.
        /// </summary>
        /// <param name="TickSize">Время тика в мс.</param>
        /// <param name="InhibitingPhaseColor">Цвет запрещающего сигнала.</param>
        /// <param name="PermissivePhaseColor">Цвет разрешающего сигнала.</param>
        public TrafficLightsBase(double TickSize, 
            SolidColorBrush InhibitingPhaseColor, 
            SolidColorBrush PermissivePhaseColor)
        {
            InhibitingPhase = new Light(InhibitingPhaseColor);

            PermissivePhase = new Light(PermissivePhaseColor);

            _TimerFromPermissiveToInhibiting = new Timer(TickSize);
            _TimerFromPermissiveToInhibiting.AutoReset = true;
            _TimerFromPermissiveToInhibiting.Elapsed += FromPermissiveToInhibitingPhases;

            _TimerFromInhibitingToPermissive = new Timer(TickSize);
            _TimerFromInhibitingToPermissive.AutoReset = true;
            _TimerFromInhibitingToPermissive.Elapsed += FromInhibitingToPermissivePhases;

            _AutoTimer = new Timer(6000);
            _AutoTimer.AutoReset = true;
            _AutoTimer.Elapsed += SetAutoMod;
        }

        public virtual void SwitchToInhibitingPhase()
        {
            _TimerFromPermissiveToInhibiting.Start();
        }

        public virtual void SwitchToPermissivePhase()
        {
            _TimerFromInhibitingToPermissive.Start();
        }

        /// <summary>
        /// Включить автоматический режим.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void SetAutoMod(object sender, ElapsedEventArgs e)
        {
            if (State != TrafficLightsState.Automatic)
                _AutoTimer.Stop();
            if (_IsPermissive)
                SwitchToInhibitingPhase();
            else
                SwitchToPermissivePhase();
        }

        /// <summary>
        /// Выключить светофор.
        /// </summary>
        protected virtual void SetDisableMod()
        {
            _AutoTimer.Stop();
            PermissivePhase.Enabled = false;
            InhibitingPhase.Enabled = false;   
        }

        /// <summary>
        /// Сценарий переключения с разрешающей фазы на запрещающую.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void FromPermissiveToInhibitingPhases(object sender, ElapsedEventArgs e);

        /// <summary>
        /// Сценарий переключения с запрещающей фазы на разрешающую.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void FromInhibitingToPermissivePhases(object sender, ElapsedEventArgs e);
    }
}
