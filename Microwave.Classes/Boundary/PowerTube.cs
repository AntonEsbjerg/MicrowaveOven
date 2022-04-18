using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private enum PowerLevel
        {
            Standard,
            high,
            VeryHigh,
            VeryLow,
            Low,
        }

        private PowerLevel powerLevel;
        private IOutput myOutput;

        private bool IsOn = false;

        public PowerTube(IOutput output)
        {
            myOutput = output;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || 700 < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 700 (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException($"PowerTube.TurnOn: is already on. Power: {power}");
            }
            else
            {
                myOutput.OutputLine($"PowerTube works with {power}");
                IsOn = true;
            }
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }
            else
            {
                myOutput.OutputLine($"PowerTube already turned off");
            }
            IsOn = false;
        }

        public void AdjustPower()
        {
            if (powerLevel == PowerLevel.Standard)
            {
                powerLevel = PowerLevel.high;

            }
            else  if (powerLevel == PowerLevel.high)
            {
                powerLevel = PowerLevel.VeryHigh;

            }
            else if (powerLevel == PowerLevel.VeryHigh)
            {
                powerLevel = PowerLevel.Low;

            }
            else if (powerLevel == PowerLevel.Low)
            {
                powerLevel = PowerLevel.VeryLow;

            }
            else if (powerLevel == PowerLevel.VeryLow)
            {
                powerLevel = PowerLevel.Standard;

            }
        }
    }
}
