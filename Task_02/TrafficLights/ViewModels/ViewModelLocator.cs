using Microsoft.Extensions.DependencyInjection;

namespace TrafficLights.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
