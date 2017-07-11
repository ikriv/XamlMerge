namespace IKriv.XamlMerge.Tests
{
    static class TestFiles
    {
        public const char Apostrophe = '\'';
        public const char Quote = '"';

        private static string R(string input)
        {
            return input.Replace(Apostrophe, Quote);
        }

        public static readonly string EmptyApp =
R(@"<Application x:Class='TestWpfProject.App' 
              xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
              xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' StartupUri='MainWindow.xaml'>
</Application>");

        public static readonly string EmptyAppProcessed =
R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'></Application>");

        public static readonly string AppWithoutRdElement =
R(@"<Application x:Class='TestWpfProject.App'
              xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
              xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' StartupUri='MainWindow.xaml'>
    <Application.Resources>
        <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
    </Application.Resources>
</Application>");

        public static readonly string AppWithoutRdElementProcessed =
R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
  <Application.Resources>
    <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
  </Application.Resources>
</Application>");

        public static readonly string AppWithoutMdElement =
R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
    <Application.Resources>
        <ResourceDictionary>
            <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
        </ResourceDictionary>
    </Application.Resources>
</Application>");

        public static readonly string AppWithoutMdElementProcessed =
R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
  <Application.Resources>
    <ResourceDictionary>
      <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
    </ResourceDictionary>
  </Application.Resources>
</Application>");

        public static readonly string AppWithoutMdElementAsResOnly =
R(@"<ResourceDictionary xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
</ResourceDictionary>");

    }
}
