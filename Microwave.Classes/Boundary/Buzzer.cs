using System.Threading;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private IOutput myOutput;
        private bool BuzzerIsOn = false;
        
        public Buzzer(IOutput output)
        {
            myOutput = output;
        }
        
        public void BuzzerOn()
        {
            if (!BuzzerIsOn)
            {
                myOutput.OutputLine("BEEP");
                Thread.Sleep(1000);
                myOutput.OutputLine("BEEP");
                Thread.Sleep(1000);
                myOutput.OutputLine("BEEP");
                BuzzerIsOn = true;
            }
        }
        
        
        public void BuzzerOff()
        {
            if (BuzzerIsOn)
            {
                myOutput.OutputLine("Buzzer is silent");
                BuzzerIsOn = false;
            }
        }
    }
}