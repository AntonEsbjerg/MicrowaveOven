using System;
using System.Runtime.Serialization;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class UserInterface : IUserInterface
    {
        private enum States
        {
            READY, SETPOWER, SETTIME, COOKING, DOOROPEN
        }

        private States myState = States.READY;
        private ICookController myCooker;
        private ILight myLight;
        private IDisplay myDisplay;
        private IPowerTube myPowerTube;
        private IBuzzer myBuzzer;

        private int powerLevel = 50;
        private int time = 1;

        public UserInterface(
            IButton powerButton,
            IButton timeButton,
            IButton startCancelButton,
            IButton extendButton,
            IButton shortenButton,
            IButton configurePowerTubeButton,
            IDoor door,
            IDisplay display,
            ILight light,
            IPowerTube powerTube,
            ICookController cooker,
            IBuzzer buzzer)
        {
            powerButton.Pressed += new EventHandler(OnPowerPressed);
            timeButton.Pressed += new EventHandler(OnTimePressed);
            startCancelButton.Pressed += new EventHandler(OnStartCancelPressed);
            extendButton.Pressed += new EventHandler(ExtendCooking);
            shortenButton.Pressed += new EventHandler(ShortenCooking);
            configurePowerTubeButton.Pressed += new EventHandler(ChangePowerTubePowerLevel);
            door.Closed += new EventHandler(OnDoorClosed);
            door.Opened += new EventHandler(OnDoorOpened);

            myCooker = cooker;
            myLight = light;
            myDisplay = display;
            myPowerTube = powerTube;
            myBuzzer = buzzer;
        }

        private void ResetValues()
        {
            powerLevel = 50;
            time = 1;
        }

        public void ChangePowerTubePowerLevel(object sender, EventArgs e)
        {
            
            if (myCooker.ChangePowerTubePowerLevel() != 0)
            {
                powerLevel = myCooker.ChangePowerTubePowerLevel();
            }

        }

        public void OnPowerPressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myDisplay.ShowPower(powerLevel);
                    myState = States.SETPOWER;
                    break;
                case States.SETPOWER:
                    powerLevel = (powerLevel >= 700 ? 50 : powerLevel+50);
                    myDisplay.ShowPower(powerLevel);
                    break;
            }
        }

        public void OnTimePressed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.SETPOWER:
                    myDisplay.ShowTime(time, 0);
                    myState = States.SETTIME;
                    break;
                case States.SETTIME:
                    time += 1;
                    myDisplay.ShowTime(time, 0);
                    break;
            }
        }

        public void OnStartCancelPressed(object sender, EventArgs e)
        {
            switch (myState)
            {

                case States.SETPOWER:
                    ResetValues();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
                case States.SETTIME:
                    myLight.TurnOn();
                    myCooker.StartCooking(powerLevel, time*60);
                    myState = States.COOKING;
                    break;
                case States.COOKING:
                    ResetValues();
                    myCooker.Stop();
                    myLight.TurnOff();
                    myBuzzer.BuzzerOff();
                    myDisplay.Clear();
                    myState = States.READY;
                    break;
            }
        }

        public void OnDoorOpened(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.READY:
                    myLight.TurnOn();
                    myBuzzer.BuzzerOff();
                    myState = States.DOOROPEN;
                    break;
                case States.SETPOWER:
                    ResetValues();
                    myLight.TurnOn();
                    myBuzzer.BuzzerOff();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.SETTIME:
                    ResetValues();
                    myLight.TurnOn();
                    myBuzzer.BuzzerOff();
                    myDisplay.Clear();
                    myState = States.DOOROPEN;
                    break;
                case States.COOKING:
                    myCooker.Stop();
                    myDisplay.Clear();
                    myBuzzer.BuzzerOff();
                    ResetValues();
                    myState = States.DOOROPEN;
                    break;
            }
        }

        public void OnDoorClosed(object sender, EventArgs e)
        {
            switch (myState)
            {
                case States.DOOROPEN:
                    myLight.TurnOff();
                    myBuzzer.BuzzerOff();
                    myState = States.READY;
                    break;
            }
        }

        public void CookingIsDone()
        {
            switch (myState)
            {
                case States.COOKING:
                    ResetValues();
                    myDisplay.Clear();
                    myLight.TurnOff();
                    myBuzzer.BuzzerOn();
                    myState = States.READY;
                    break;
            }
        }

        public void ExtendCooking(object sender, EventArgs e)
        {
            myCooker.ExtendCooking();
        }
        public void ShortenCooking(object sender, EventArgs e)
        {
            myCooker.ShortenCooking();
        }
    }
}