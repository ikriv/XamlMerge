using System.Windows;

namespace MyLibrary
{
    public static class MyKeys
    {
        public static readonly object MainBackground = new ComponentResourceKey(typeof(MyKeys), "MainBackground");
        public static readonly object MainForeground = new ComponentResourceKey(typeof(MyKeys), "MainForeground");
    }
}
