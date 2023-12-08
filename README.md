## Maui Radio Buttons

As I understand it, you're trying to bind the `RadioButton` members of a data template in your `CollectionView` where `ItemsSource` is `ObservableCollection<AutoWithSwitch>`. When I tried to reproduce your issue as minimally as possible from your code,  I was getting some complaints from iOS about not liking the fact that a `StackLayout` (the error just referred to it as a `View`) was being set as `RadioButton.Content`. That "may or may not" affect what you're seeing. I do understand what you're trying to do with the margin on the `Label` but this seemed to be a big problem when building it so I margined the label by wrapping both in a horizontal stack. 

Other than that, the bindings seemed to work OK, however one difference is that I'm binding directly to an enum value and not using the `GroupName` property because that makes it redundant. You say you've tried "a ton of different solutions" and I hope that one more won't hurt and that it gives you something new to try.

[![collection view initial load][1]][1]
___

**MainPage xaml with Mock ListView of type: `ObservableCollection<AutoWithSwitch>`**

_This xaml forward-references a `EnumToBooleanConverter` that is defined below._

```xaml
<ContentPage 
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"             
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"             
    xmlns:local ="clr-namespace:maui_radio_buttons"             
    x:Class="maui_radio_buttons.MainPage">
    <ContentPage.BindingContext>
        <local:MainPageBindingContext/>
    </ContentPage.BindingContext>
    <ContentPage.Resources>
        <ResourceDictionary>
            <local:EnumToBooleanConverter x:Key="EnumToBoolConverter" />
        </ResourceDictionary>
    </ContentPage.Resources>
    <CollectionView ItemsSource="{Binding AutoWithSwitchCollection}">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <Frame Padding="5" CornerRadius="10">
                    <Grid    
                        RowSpacing="2"
                        BackgroundColor="White" 
                        Padding="5"
                        RowDefinitions="Auto,Auto"
                        ColumnDefinitions="*,*,*">
                        <Label 
                            Grid.Column="0"
                            Grid.Row="0"
                            Grid.ColumnSpan="3"
                            HorizontalOptions="Fill"
                            HorizontalTextAlignment="Center"
                            Text="{Binding Description}" 
                            FontSize="Medium"/>
                        <HorizontalStackLayout
                                Grid.Column="0"
                                Grid.Row="1">
                            <RadioButton
                                GroupName="RadioGroup"
                                WidthRequest="50"
                                IsChecked="{Binding CarType, Converter={StaticResource EnumToBoolConverter}, ConverterParameter=Tesla}"/>
                            <Label
                                Text="Tesla"
                                Margin="10,0,0,0" 
                                VerticalTextAlignment="Center"
                                HorizontalTextAlignment="Center" />
                        </HorizontalStackLayout>
                        <HorizontalStackLayout
                                Grid.Column="1"
                                Grid.Row="1">
                            <RadioButton
                                GroupName="RadioGroup"
                                WidthRequest="50"
                                HeightRequest="30"
                                IsChecked="{Binding CarType, Converter={StaticResource EnumToBoolConverter}, ConverterParameter=Audi}"
                                HorizontalOptions="Fill"/>
                                <Label 
                                    Text="Audi"
                                    Margin="10,0,0,0" 
                                    VerticalTextAlignment="Center"
                                    HorizontalTextAlignment="Center" />
                        </HorizontalStackLayout>
                        <HorizontalStackLayout
                                Grid.Column="2"
                                Grid.Row="1">
                            <RadioButton
                                GroupName="RadioGroup"
                                WidthRequest="50"
                                IsChecked="{Binding CarType, Converter={StaticResource EnumToBoolConverter}, ConverterParameter=Porsche}"
                                HorizontalOptions="Fill"/>
                            <Label 
                                Text="Porsche"
                                Margin="10,0,0,0" 
                                VerticalTextAlignment="Center"
                                HorizontalTextAlignment="Center" />
                        </HorizontalStackLayout>
                    </Grid>
                </Frame>
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>
</ContentPage>
```
___
**Mock AutoWithSwitch**
```csharp
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
```

```

```

**MainPage C#**
```csharp
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
```
___
**IValueConverter `EnumToBooleanConverter`**

```csharp
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
```


  [1]: https://i.stack.imgur.com/o7OeL.png