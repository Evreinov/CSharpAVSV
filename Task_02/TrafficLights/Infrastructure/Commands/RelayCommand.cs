using System;
using TrafficLights.Infrastructure.Commands.Base;

namespace TrafficLights.Infrastructure.Commands
{
    class RelayCommand : CommandBase
    {
        private readonly Action<object> _Execute;

        private readonly Func<object, bool> _CanExecute;

        public RelayCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        protected override void Execute(object p) => _Execute(p);

        protected override bool CanExecute(object p) => _CanExecute?.Invoke(p) ?? true;
    }
}
