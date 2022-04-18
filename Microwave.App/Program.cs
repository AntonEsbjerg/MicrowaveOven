using System;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            IButton startCancelButton = new Button();
            IButton powerButton = new Button();
            IButton timeButton = new Button();
            IButton extendButton = new Button();
            IButton shortenButton = new Button();
            IButton configurePowerTubeButton = new Button();

            IDoor door = new Door();

            IOutput output = new Output();

            IDisplay display = new Display(output);

            IPowerTube powerTube = new PowerTube(output);

            ILight light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            IUserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, extendButton, shortenButton,door, display, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();
            // Test 3 Jenkins Webhook 
            // Rebase test
            timeButton.Press();

            startCancelButton.Press();
            Thread.Sleep(5000);
            extendButton.Press();
            Thread.Sleep(5000);
            shortenButton.Press();
            Thread.Sleep(5000);
            shortenButton.Press();
            Thread.Sleep(5000);
            shortenButton.Press();
            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input
            
            System.Console.ReadLine();
        }
    }
}
