using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {

        private IOutput myOutput;
        private int maxPower;

        private bool IsOn = false;

        public PowerTube(IOutput output, int maxpower)
        {
            myOutput = output;
            maxPower = maxpower;

        }

        public void TurnOn(int power)
        {
            if (power < 1 || maxPower < power)
            {
                throw new ArgumentOutOfRangeException("power ", power, $" Must be between 1 and {maxPower} (incl.)");
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


    }
}
