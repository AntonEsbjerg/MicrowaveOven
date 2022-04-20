using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class TDStep1
    {
        private IDoor door;
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IButton extendButton;
        private IButton shortenButton;
        private IButton configurePowerTubeButton;

        private IUserInterface ui;

        private ILight light;
        private IDisplay display;
        private ICookController cooker;
        private IBuzzer buzzer;
        private IPowerTube powerTube;

        [SetUp]
        public void Setup()
        {
            door = new Door();
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();

            light = Substitute.For<ILight>();
            display = Substitute.For<IDisplay>();
            cooker = Substitute.For<ICookController>();
            buzzer = Substitute.For<IBuzzer>();
            shortenButton = Substitute.For<IButton>();
            extendButton = Substitute.For<IButton>();

            ui = new UserInterface(
                powerButton, timeButton, startCancelButton, extendButton, shortenButton, configurePowerTubeButton,
                door,
                display, light, powerTube, cooker, buzzer);
        }

        [Test]
        public void Door_UI_DoorOpen()
        {
            door.Open();

            light.Received(1).TurnOn();
        }
        public void Door_UI_DoorClose()
        {
            door.Open();
            door.Close();

            light.Received(1).TurnOff();
        }

        [Test]
        public void PowerButton_UI_PowerPressed()
        {
            powerButton.Press();

            display.Received(1).ShowPower(50);
        }

        [Test]
        public void TimeButton_UI_TimePressed()
        {
            powerButton.Press();
            timeButton.Press();

            display.Received(1).ShowTime(1, 0);
        }


    }
}