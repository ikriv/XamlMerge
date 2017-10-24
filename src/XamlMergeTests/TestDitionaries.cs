namespace IKriv.XamlMerge.Tests
{
    static class TestDitionaries
    {
        private static string R(string input)
        {
            return input.Replace('\'', '\"');
        }

        public static readonly string SimpleMergedDictionary =
            R(@"<ResourceDictionary xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:vm='clr-namespace:Local.ViewModels'
xmlns:views='clr-namespace:Local.Views'>
    <SolidColorBrush x:Key='Foreground' Color='#FF0000'/>
    <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
        <StackPanel Orientation='Vertical'>
             <Label>This is views:CoolControl</Label>
             <views:CoolControl />
        </StackPanel>
    </DataTemplate>
</ResourceDictionary>");

        public static readonly string RecursiveDictionary =
            R(@"<ResourceDictionary xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:ourVm='clr-namespace:Our.Assembly.ViewModels'
xmlns:ourViews='clr-namespace:Our.Assembly.Views'>
    <ResourceDictionary.MergedDictionaries>
        <MergedDictionary Source='pack://application,,,/External.Assembly;component/Resources/Thing.xaml' />
        <MergedDictionary Source='pack://application,,,/Another.Assembly;component/Resources/Stuff.xaml' />
    </ResourceDictionary.MergedDictionaries>
    <DataTemplate DataType='{x:Type ourVm:CoolViewModel}'>
        <StackPanel Orientation='Vertical'>
             <Label>This is views:CoolControl</Label>
             <ourViews:CoolControl />
        </StackPanel>
    </DataTemplate>
</ResourceDictionary>");

        public static readonly string DictionaryWithCustomMd =
            R(@"<ResourceDictionary xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:custom='clr-namespace:My.Custom.Namespace;assembly=Some.Assembly'>
    <ResourceDictionary.MergedDictionaries>
        <custom:MyMergedDictionary Magic='winguardia leviosa' />
    </ResourceDictionary.MergedDictionaries>
    <SolidColorBrush x:Key='something' Color='Red' />
</ResourceDictionary>");
    }
}
