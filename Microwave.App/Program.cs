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

            IDoor door = new Door();

            IOutput output = new Output();

            IDisplay display = new Display(output);

            int maxpower = Convert.ToInt32(700);
            IPowerTube powerTube = new PowerTube(output, maxpower);

            ILight light = new Light(output);

            IBuzzer buzzer = new Buzzer(output);

            Microwave.Classes.Interfaces.ITimer timer = new Timer();

            ICookController cooker = new CookController(timer, display, powerTube);

            IUserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, extendButton, shortenButton, door, display, light, powerTube, cooker, buzzer);

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
