using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace maui_radio_buttons
{
    public partial class MainPage : ContentPage
    {
        int count = 0; bool isButtonClicking = false;
        public MainPage()
        {
            InitializeComponent();
            BindingContext.AutoWithSwitchCollection.Add(new AutoWithSwitch
            {
                Description = "Tesla Model S",
                CarType = CarType.Tesla,
            });
            BindingContext.AutoWithSwitchCollection.Add(new AutoWithSwitch
            {
                CarType = CarType.Audi,
                Description = "Audi R8",
            });
            BindingContext.AutoWithSwitchCollection.Add(new AutoWithSwitch
            {
                CarType = CarType.Porsche,
                Description = "Porsche 911 Turbo",
            });
        }
        new MainPageBindingContext BindingContext => (MainPageBindingContext)base.BindingContext;
    }
    class MainPageBindingContext : INotifyPropertyChanged
    {
        public ObservableCollection<AutoWithSwitch> AutoWithSwitchCollection { get; } = new ObservableCollection<AutoWithSwitch>();

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;
            string enumValue = value.ToString();
            string targetValue = parameter.ToString();
            return enumValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return Enum.Parse(targetType, parameter.ToString(), true);
            }
            return Binding.DoNothing;
        }
    }
}
