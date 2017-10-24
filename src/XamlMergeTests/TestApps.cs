namespace IKriv.XamlMerge.Tests
{
    static class TestApps
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

        public static readonly string AppWithEmptyMdElement =
            R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
            </ResourceDictionary.MergedDictionaries>
            <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
        </ResourceDictionary>
    </Application.Resources>
</Application>");

        public static readonly string AppWithEmptyMdElementProcessed =
R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
  <Application.Resources>
    <ResourceDictionary>
      <Resourcedictionary.MergedDictionaries />
      <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
    </ResourceDictionary>
  </Application.Resources>
</Application>");

        public static readonly string AppWithEmptyMdElementAsResOnly =
R(@"<ResourceDictionary xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <Resourcedictionary.MergedDictionaries />
  <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
</ResourceDictionary>");

        public static readonly string AppWithLocalMd =
R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <MergedDictionary Source='Local.xaml' />
            </ResourceDictionary.MergedDictionaries>
            <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
        </ResourceDictionary>
    </Application.Resources>
</Application>");

        public static readonly string AppWithLocalMdProcessed =
            R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml' xmlns:vm='clr-namespace:Local.ViewModels'
xmlns:views='clr-namespace:Local.Views'>
  <Application.Resources>
    <ResourceDictionary>
      <!-- Merged from c:\some\dir\Local.xaml -->
      <SolidColorBrush x:Key='Foreground' Color='#FF0000' />
      <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
        <StackPanel Orientation='Vertical'>
          <Label>This is views:CoolControl</Label>
          <views:CoolControl />
        </StackPanel>
      </DataTemplate>
      <!-- End merged from c:\some\dir\Local.xaml -->
      <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
    </ResourceDictionary>
  </Application.Resources>
</Application>");

        public static readonly string AppWithLocalMdAsResOnly =
            R(@"<ResourceDictionary xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:vm='clr-namespace:Local.ViewModels'
xmlns:views='clr-namespace:Local.Views'
xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <!-- Merged from c:\some\dir\Local.xaml -->
  <SolidColorBrush x:Key='Foreground' Color='#FF0000' />
  <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
    <StackPanel Orientation='Vertical'>
      <Label>This is views:CoolControl</Label>
      <views:CoolControl />
    </StackPanel>
  </DataTemplate>
  <!-- End merged from c:\some\dir\Local.xaml -->
  <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
</ResourceDictionary>");


        public static readonly string AppWithExternalMd =
            R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <MergedDictionary Source='pack://application,,,/External.Assembly;component/Resources/Stuff.xaml' />
            </ResourceDictionary.MergedDictionaries>
            <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
        </ResourceDictionary>
    </Application.Resources>
</Application>");

        public static readonly string AppWithExternalMdProcessed =
            R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
  <Application.Resources>
    <ResourceDictionary>
      <ResourceDictionary.MergedDictionaries>
        <MergedDictionary Source='pack://application,,,/External.Assembly;component/Resources/Stuff.xaml' />
      </ResourceDictionary.MergedDictionaries>
      <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
    </ResourceDictionary>
  </Application.Resources>
</Application>");

        public static readonly string AppWithExternalMdAsResOnly =
            R(@"<ResourceDictionary xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <ResourceDictionary.MergedDictionaries>
    <MergedDictionary Source='pack://application,,,/External.Assembly;component/Resources/Stuff.xaml' />
  </ResourceDictionary.MergedDictionaries>
  <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
</ResourceDictionary>");

        public static readonly string AppWithOurAssemblyMd =
            R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <MergedDictionary Source='pack://application,,,/Our.Assembly;component/Resources/Stuff.xaml' />
            </ResourceDictionary.MergedDictionaries>
            <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
        </ResourceDictionary>
    </Application.Resources>
</Application>");

        public static readonly string AppWithOurAssemblyMdProcessed =
        R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml' xmlns:vm='clr-namespace:Local.ViewModels;assembly=Our.Assembly'
xmlns:views='clr-namespace:Local.Views;assembly=Our.Assembly'>
  <Application.Resources>
    <ResourceDictionary>
      <!-- Merged from c:\some\dir\Our.Assembly\Resources\Stuff.xaml -->
      <SolidColorBrush x:Key='Foreground' Color='#FF0000' />
      <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
        <StackPanel Orientation='Vertical'>
          <Label>This is views:CoolControl</Label>
          <views:CoolControl />
        </StackPanel>
      </DataTemplate>
      <!-- End merged from c:\some\dir\Our.Assembly\Resources\Stuff.xaml -->
      <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
    </ResourceDictionary>
  </Application.Resources>
</Application>");

        public static readonly string AppWithOurAssemblyMdAsResOnly =
            R(@"<ResourceDictionary xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:vm='clr-namespace:Local.ViewModels;assembly=Our.Assembly'
xmlns:views='clr-namespace:Local.Views;assembly=Our.Assembly'
xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <!-- Merged from c:\some\dir\Our.Assembly\Resources\Stuff.xaml -->
  <SolidColorBrush x:Key='Foreground' Color='#FF0000' />
  <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
    <StackPanel Orientation='Vertical'>
      <Label>This is views:CoolControl</Label>
      <views:CoolControl />
    </StackPanel>
  </DataTemplate>
  <!-- End merged from c:\some\dir\Our.Assembly\Resources\Stuff.xaml -->
  <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
</ResourceDictionary>");

        public static readonly string AppWithRecursiveMd =
            R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml'>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <MergedDictionary Source='pack://application,,,/Some.Assembly;component/Bla/Bla.xaml' />
                <MergedDictionary Source='pack://application,,,/Our.Assembly;component/Resources/Recursive.xaml' />
                <MergedDictionary Source='pack://application,,,/UX.Themes;component/Theme/generic.xaml' />
            </ResourceDictionary.MergedDictionaries>
            <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
        </ResourceDictionary>
    </Application.Resources>
</Application>");

        public static readonly string AppWithRecursiveMdProcessed =
            R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
StartupUri='MainWindow.xaml' xmlns:ourVm='clr-namespace:Our.Assembly.ViewModels;assembly=Our.Assembly'
xmlns:ourViews='clr-namespace:Our.Assembly.Views;assembly=Our.Assembly'
xmlns:vm='clr-namespace:Local.ViewModels;assembly=Another.Assembly'
xmlns:views='clr-namespace:Local.Views;assembly=Another.Assembly'>
  <Application.Resources>
    <ResourceDictionary>
      <ResourceDictionary.MergedDictionaries>
        <MergedDictionary Source='pack://application,,,/Some.Assembly;component/Bla/Bla.xaml' />
        <MergedDictionary Source='pack://application,,,/External.Assembly;component/Resources/Thing.xaml' />
        <MergedDictionary Source='pack://application,,,/UX.Themes;component/Theme/generic.xaml' />
      </ResourceDictionary.MergedDictionaries>
      <!-- Merged from c:\some\dir\Another.Assembly\Resources\Stuff.xaml -->
      <SolidColorBrush x:Key='Foreground' Color='#FF0000' />
      <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
        <StackPanel Orientation='Vertical'>
          <Label>This is views:CoolControl</Label>
          <views:CoolControl />
        </StackPanel>
      </DataTemplate>
      <!-- End merged from c:\some\dir\Another.Assembly\Resources\Stuff.xaml -->
      <!-- Merged from c:\some\dir\Our.Assembly\Resources\Recursive.xaml -->
      <DataTemplate DataType='{x:Type ourVm:CoolViewModel}'>
        <StackPanel Orientation='Vertical'>
          <Label>This is views:CoolControl</Label>
          <ourViews:CoolControl />
        </StackPanel>
      </DataTemplate>
      <!-- End merged from c:\some\dir\Our.Assembly\Resources\Recursive.xaml -->
      <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
    </ResourceDictionary>
  </Application.Resources>
</Application>");

        public static readonly string AppWithRecursiveMdResOnly =
            R(@"<ResourceDictionary xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:ourVm='clr-namespace:Our.Assembly.ViewModels;assembly=Our.Assembly'
xmlns:ourViews='clr-namespace:Our.Assembly.Views;assembly=Our.Assembly'
xmlns:vm='clr-namespace:Local.ViewModels;assembly=Another.Assembly'
xmlns:views='clr-namespace:Local.Views;assembly=Another.Assembly'
xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <ResourceDictionary.MergedDictionaries>
    <MergedDictionary Source='pack://application,,,/Some.Assembly;component/Bla/Bla.xaml' />
    <MergedDictionary Source='pack://application,,,/External.Assembly;component/Resources/Thing.xaml' />
    <MergedDictionary Source='pack://application,,,/UX.Themes;component/Theme/generic.xaml' />
  </ResourceDictionary.MergedDictionaries>
  <!-- Merged from c:\some\dir\Another.Assembly\Resources\Stuff.xaml -->
  <SolidColorBrush x:Key='Foreground' Color='#FF0000' />
  <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
    <StackPanel Orientation='Vertical'>
      <Label>This is views:CoolControl</Label>
      <views:CoolControl />
    </StackPanel>
  </DataTemplate>
  <!-- End merged from c:\some\dir\Another.Assembly\Resources\Stuff.xaml -->
  <!-- Merged from c:\some\dir\Our.Assembly\Resources\Recursive.xaml -->
  <DataTemplate DataType='{x:Type ourVm:CoolViewModel}'>
    <StackPanel Orientation='Vertical'>
      <Label>This is views:CoolControl</Label>
      <ourViews:CoolControl />
    </StackPanel>
  </DataTemplate>
  <!-- End merged from c:\some\dir\Our.Assembly\Resources\Recursive.xaml -->
  <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
</ResourceDictionary>");

        public static readonly string AppWithRecursiveMdAlteredPaths =
            R(@"<ResourceDictionary xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:ourVm='clr-namespace:Our.Assembly.ViewModels;assembly=Our.Assembly'
xmlns:ourViews='clr-namespace:Our.Assembly.Views;assembly=Our.Assembly'
xmlns:vm='clr-namespace:Local.ViewModels;assembly=Another.Assembly'
xmlns:views='clr-namespace:Local.Views;assembly=Another.Assembly'
xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <ResourceDictionary.MergedDictionaries>
    <MergedDictionary Source='pack://application,,,/Some.Assembly;component/Bla/Bla.xaml' />
    <MergedDictionary Source='pack://application,,,/External.Assembly;component/Resources/Thing.xaml' />
    <MergedDictionary Source='pack://application,,,/UX.Themes;component/Theme/generic.xaml' />
  </ResourceDictionary.MergedDictionaries>
  <!-- Merged from c:\some\dir\Another\Resources\Stuff.xaml -->
  <SolidColorBrush x:Key='Foreground' Color='#FF0000' />
  <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
    <StackPanel Orientation='Vertical'>
      <Label>This is views:CoolControl</Label>
      <views:CoolControl />
    </StackPanel>
  </DataTemplate>
  <!-- End merged from c:\some\dir\Another\Resources\Stuff.xaml -->
  <!-- Merged from c:\some\dir\Our\Resources\Recursive.xaml -->
  <DataTemplate DataType='{x:Type ourVm:CoolViewModel}'>
    <StackPanel Orientation='Vertical'>
      <Label>This is views:CoolControl</Label>
      <ourViews:CoolControl />
    </StackPanel>
  </DataTemplate>
  <!-- End merged from c:\some\dir\Our\Resources\Recursive.xaml -->
  <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
</ResourceDictionary>");

        public static readonly string AppWithCustomMd =
                    R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:custom='clr-namespace:My.Custom.Namespace;assembly=Some.Assembly'
StartupUri='MainWindow.xaml'>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <custom:MyMergedDictionary Magic='winguardia leviosa' />
                <MergedDictionary Source='Local.xaml' />
            </ResourceDictionary.MergedDictionaries>
            <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
        </ResourceDictionary>
    </Application.Resources>
</Application>");

        public static readonly string AppWithCustomMdProcessed =
                    R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:custom='clr-namespace:My.Custom.Namespace;assembly=Some.Assembly'
StartupUri='MainWindow.xaml' xmlns:vm='clr-namespace:Local.ViewModels'
xmlns:views='clr-namespace:Local.Views'>
  <Application.Resources>
    <ResourceDictionary>
      <ResourceDictionary.MergedDictionaries>
        <custom:MyMergedDictionary Magic='winguardia leviosa' />
      </ResourceDictionary.MergedDictionaries>
      <!-- Merged from c:\some\dir\Local.xaml -->
      <SolidColorBrush x:Key='Foreground' Color='#FF0000' />
      <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
        <StackPanel Orientation='Vertical'>
          <Label>This is views:CoolControl</Label>
          <views:CoolControl />
        </StackPanel>
      </DataTemplate>
      <!-- End merged from c:\some\dir\Local.xaml -->
      <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
    </ResourceDictionary>
  </Application.Resources>
</Application>");

        public static readonly string AppWithCustomMdResOnly =
                    R(@"<ResourceDictionary xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:custom='clr-namespace:My.Custom.Namespace;assembly=Some.Assembly'
xmlns:vm='clr-namespace:Local.ViewModels'
xmlns:views='clr-namespace:Local.Views'
xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <ResourceDictionary.MergedDictionaries>
    <custom:MyMergedDictionary Magic='winguardia leviosa' />
  </ResourceDictionary.MergedDictionaries>
  <!-- Merged from c:\some\dir\Local.xaml -->
  <SolidColorBrush x:Key='Foreground' Color='#FF0000' />
  <DataTemplate DataType='{x:Type vm:CoolViewModel}'>
    <StackPanel Orientation='Vertical'>
      <Label>This is views:CoolControl</Label>
      <views:CoolControl />
    </StackPanel>
  </DataTemplate>
  <!-- End merged from c:\some\dir\Local.xaml -->
  <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
</ResourceDictionary>");

        public static readonly string AppWithIndirectCustomMd =
                    R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:custom='clr-namespace:My.Custom.Namespace;assembly=Some.Assembly'
StartupUri='MainWindow.xaml'>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <MergedDictionary Source='Local.xaml' />
            </ResourceDictionary.MergedDictionaries>
            <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
        </ResourceDictionary>
    </Application.Resources>
</Application>");

        public static readonly string AppWithIndirectCustomMdProcessed =
                    R(@"<Application x:Class='TestWpfProject.App' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:custom='clr-namespace:My.Custom.Namespace;assembly=Some.Assembly'
StartupUri='MainWindow.xaml'>
  <Application.Resources>
    <ResourceDictionary>
      <ResourceDictionary.MergedDictionaries>
        <custom:MyMergedDictionary Magic='winguardia leviosa' />
      </ResourceDictionary.MergedDictionaries>
      <!-- Merged from c:\some\dir\Local.xaml -->
      <SolidColorBrush x:Key='something' Color='Red' />
      <!-- End merged from c:\some\dir\Local.xaml -->
      <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
    </ResourceDictionary>
  </Application.Resources>
</Application>");

        public static readonly string AppWithIndirectCustomMdResOnly =
                    R(@"<ResourceDictionary xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
xmlns:custom='clr-namespace:My.Custom.Namespace;assembly=Some.Assembly'
xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
  <ResourceDictionary.MergedDictionaries>
    <custom:MyMergedDictionary Magic='winguardia leviosa' />
  </ResourceDictionary.MergedDictionaries>
  <!-- Merged from c:\some\dir\Local.xaml -->
  <SolidColorBrush x:Key='something' Color='Red' />
  <!-- End merged from c:\some\dir\Local.xaml -->
  <SolidColorBrush x:Key='bla' Color='#FFFFFF' />
</ResourceDictionary>");

    }
}
