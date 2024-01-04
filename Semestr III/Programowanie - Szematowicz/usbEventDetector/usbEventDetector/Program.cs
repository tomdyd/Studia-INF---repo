using System;
using System.Management;
using System.Threading;
using usbEventDetector;
using usbEventDetector.Interfaces;

internal class Program
{
    static void Main(string[] args)
    {
        IConsole myConsole = new MyConsole();
        var appRunner  = new AppRunner(myConsole);
        appRunner.StartApp();
    }
}

