using Gtk;
using System;

namespace DesktopFileCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            MainScreen mainScreen = new MainScreen();
            mainScreen.DeleteEvent += delete_event;
            Application.Run();
        }

        static void delete_event(object obj, DeleteEventArgs args)
        {
            Application.Quit();
        }
    }
}
