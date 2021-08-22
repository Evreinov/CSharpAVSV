using System.Linq;
using System.Windows;
using TrafficLights.Infrastructure.Commands.Base;

namespace TrafficLights.Infrastructure.Commands
{
    /// <summary>
    /// Команда закрытия текущего окна.
    /// </summary>
    class CloseWindowCommand : CommandBase
    {
        protected override void Execute(object p)
        {
            var window = p as Window;

            if (window is null)
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

            if (window is null)
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

            window?.Close();
        }
    }
}
