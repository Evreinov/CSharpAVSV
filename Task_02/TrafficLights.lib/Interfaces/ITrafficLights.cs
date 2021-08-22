namespace TrafficLights.lib.Interfaces
{
    public interface ITrafficLights 
    {
        /// <summary>
        /// Состояние светофора.
        /// </summary>
        public Models.TrafficLightsState State { get; set; }

        /// <summary>
        /// Включить запрещающий сигнал светофора.
        /// </summary>
        public void SwitchToInhibitingPhase();

        /// <summary>
        /// Включить разрешающий сигнал светофора.
        /// </summary>
        public void SwitchToPermissivePhase();
    }
}
