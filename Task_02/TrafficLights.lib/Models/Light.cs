using System.ComponentModel;
using System.Windows.Media;

namespace TrafficLights.lib.Models
{
    /// <summary>
    /// Лампочка.
    /// </summary>
    public class Light : INotifyPropertyChanged
    {
        // Цвет включенной лампочки.
        readonly SolidColorBrush _ColorEnabled;
        // Цвет выключенной лампочки.
        readonly SolidColorBrush _ColorDisabled;
        // Текущий цвет лампочки.
        SolidColorBrush _CurrentColor;
        // Состояние лампочки вкл./выкл.
        private bool _Enabled;

        /// <summary>
        /// Срабатывает при изменении свойств модели.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Текущий цвет.
        /// </summary>
        public SolidColorBrush CurrentColor { get => _CurrentColor; }

        /// <summary>
        /// Включить/выключить лампочку.
        /// </summary>
        public bool Enabled
        {
            get => _Enabled;
            set
            {
                if (value)
                    _CurrentColor = _ColorEnabled;
                else
                    _CurrentColor = _ColorDisabled;
                _Enabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Enabled)));
            }
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Light() { }

        /// <summary>
        /// Конструктор указывающий цвет включенной лампочки.
        /// По умолчанию цвет в выключенном состоянии DarkGray.
        /// </summary>
        /// <param name="colorEnabled">Цвет включенной лампочки.</param>
        public Light(SolidColorBrush colorEnabled) : this(colorEnabled, Brushes.DarkGray) { }

        /// <summary>
        /// Конструктор указывающий цвет включенной и выключенной лампочки.
        /// </summary>
        /// <param name="colorEnabled">Цвет включенной лампочки.</param>
        /// <param name="colorDisabled">Цвет выключенной лампочки.</param>
        public Light(SolidColorBrush colorEnabled, SolidColorBrush colorDisabled)
        {
            _ColorEnabled = colorEnabled;
            _ColorDisabled = colorDisabled;
            _CurrentColor = colorDisabled;
        }
    }
}
