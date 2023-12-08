using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace maui_radio_buttons
{
    enum CarType
    {
        Tesla,
        Audi,
        Porsche,
    }
    class AutoWithSwitch: INotifyPropertyChanged
    {
        public string Description
        {
            get => _description;
            set
            {
                if (!Equals(_description, value))
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }
        string _description = string.Empty;

        public CarType CarType
        {
            get => _carType;
            set
            {
                if (!Equals(_carType, value))
                {
                    _carType = value;
                    OnPropertyChanged();
                }
            }
        }
        CarType _carType = CarType.Tesla;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
