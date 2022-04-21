using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class BUStep3
    {
        private IOutput output;

        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;
        private ICookController cooker;

        private IUserInterface ui;
        private ILight light;
        private IBuzzer buzzer;

        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IButton extendButton;
        private IButton shortenButton;



        private IDoor door;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();


            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();

            door = new Door();

            timer = new Timer();
            display = new Display(output);
            int maxpower = 700;
            powerTube = new PowerTube(output, maxpower);
            extendButton = new Button();
            shortenButton = new Button();
            light = new Light(output);
            buzzer = new Buzzer(output);

            cooker = new CookController(timer, display, powerTube);
            
            ui = new UserInterface(
                powerButton, timeButton, startCancelButton, extendButton, shortenButton, 
                door,
                display, light, powerTube, cooker, buzzer);

            cooker.UI = ui;

        }

        #region Button_UserInterface

        #endregion

        #region UserInterface_Display

        [Test]
        public void Button_UserInterface_PowerButtonPressed()
        {
            powerButton.Press();

            // Should now be 50 W
            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
        }

        [Test]
        public void Button_UserInterface_TimeButtonPressed()
        {
            powerButton.Press();
            timeButton.Press();

            // Should now show time 01:00
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

        [Test]
        public void Button_UserInterface_StartCancelButtonPressed()
        {
            powerButton.Press();
            startCancelButton.Press();

            // Should cancel, and clear display
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
        }
        #endregion

        #region Door_UserInterface
        [Test]
        public void Door_UserInterface_DoorOpened()
        {
            door.Open();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void Door_UserInterface_DoorClosed()
        {
            door.Open();
            door.Close();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        #endregion

    }
}