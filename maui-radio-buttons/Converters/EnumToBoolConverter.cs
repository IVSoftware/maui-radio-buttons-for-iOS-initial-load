using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_radio_buttons.Converters
{
    public class EnumToBoolConverter : IValueConverter
    {
        bool? _If_Uninitialized = null;
        public bool If_Uninitialized { set => _If_Uninitialized = value; }

        bool? _If_0 = null;
        public bool If_0 { set => _If_0 = value; }

        bool? _If_1 = null;
        public bool If_1 { set => _If_1 = value; }

        bool? _If_2 = null;
        public bool If_2 { set => _If_2 = value; }

        bool? _If_3 = null;
        public bool If_3 { set => _If_3 = value; }

        bool? _If_4 = null;
        public bool If_4 { set => _If_4 = value; }

        bool? _If_5 = null;
        public bool If_5 { set => _If_5 = value; }

        bool? _If_6 = null;
        public bool If_6 { set => _If_6 = value; }

        bool? _If_7 = null;
        public bool If_7 { set => _If_7 = value; }

        bool? _If_8 = null;
        public bool If_8 { set => _If_8 = value; }

        bool? _If_9 = null;
        public bool If_9 { set => _If_9 = value; }

        bool? _If_A = null;
        public bool If_A { set => _If_A = value; }

        bool? _If_B = null;
        public bool If_B { set => _If_B = value; }

        bool? _If_C = null;
        public bool If_C { set => _If_C = value; }

        bool? _If_D = null;
        public bool If_D { set => _If_D = value; }

        bool? _If_E = null;
        public bool If_E { set => _If_E = value; }

        bool? _If_F = null;
        public bool If_F { set => _If_F = value; }

        public bool Default { get; set; }
        bool _invert = false;

        /// <summary>
        /// Enum in Bool out
        /// </summary>
        /// <param name="value"></param>
        /// <returns>
        /// If parameter is an int, returns (int)value == (int)parameter.
        /// Otherwise assigns value based on explicit If_ values, subject to parameter=="Invert"
        /// </returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var @enum = (Enum)value;

            if (parameter != null)
            {
                var pstring = parameter.ToString().ToLower();
                if (pstring == "invert")
                {
                    _invert = true;
                }
                else
                {
                    int @int;
                    var parse = pstring.Split(':');
                    switch (parse.Length)
                    {
                        case 1:
                            @int = ToIntDetectHex(parse[0]);
                            return System.Convert.ToInt32(@enum) == @int;
                        case 2:
                            switch (parse[0].ToLower())
                            {
                                case "and":
                                    @int = ToIntDetectHex(parse[1]);
                                    return (System.Convert.ToInt32(@enum) & @int) != 0;
                                default:
                                    throw new NotImplementedException();
                            }
                    }
                }
            }

            _invert = (parameter != null) && parameter.ToString().ToLower() == "invert";

            switch (System.Convert.ToInt32(@enum))
            {
                case -1:
                    return ApplyInvert(_If_Uninitialized ?? Default);
                case 0:
                    return ApplyInvert(_If_0 ?? Default);
                case 1:
                    return ApplyInvert(_If_1 ?? Default);
                case 2:
                    return ApplyInvert(_If_2 ?? Default);
                case 3:
                    return ApplyInvert(_If_3 ?? Default);
                case 4:
                    return ApplyInvert(_If_4 ?? Default);
                case 5:
                    return ApplyInvert(_If_5 ?? Default);
                case 6:
                    return ApplyInvert(_If_6 ?? Default);
                case 7:
                    return ApplyInvert(_If_7 ?? Default);
                case 8:
                    return ApplyInvert(_If_8 ?? Default);
                case 9:
                    return ApplyInvert(_If_9 ?? Default);
                case 0xA:
                    return ApplyInvert(_If_A ?? Default);
                case 0xB:
                    return ApplyInvert(_If_B ?? Default);
                case 0xC:
                    return ApplyInvert(_If_C ?? Default);
                case 0xD:
                    return ApplyInvert(_If_D ?? Default);
                case 0xE:
                    return ApplyInvert(_If_E ?? Default);
                case 0xF:
                    return ApplyInvert(_If_F ?? Default);
                default:
                    return Default;
            }
        }

        private int ToIntDetectHex(string value)
        {
            var parse = value.ToLower().Split('x');
            int @int;
            switch (parse.Length)
            {
                case 1:
                    @int = Int32.Parse(parse[0]);
                    break;
                case 2:
                    @int = Int32.Parse(parse[1], System.Globalization.NumberStyles.HexNumber);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return @int;
        }

        private bool ApplyInvert(bool value)
        {
            if (_invert)
            {
                return !value;
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
