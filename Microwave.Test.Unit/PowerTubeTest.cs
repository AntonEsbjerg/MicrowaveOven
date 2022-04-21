using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class PowerTubeTest
    {
        private PowerTube uut;
        private IOutput output;
        private int maxPower;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            maxPower = 700;
            uut = new PowerTube(output, maxPower);
        }

        [TestCase(1, 100)]
        [TestCase(50, 200)]
        [TestCase(100, 100)]
        [TestCase(699, 700)]
        [TestCase(700, 1000)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power, int maxPower)
        {
            this.maxPower = maxPower;
            uut = new PowerTube(output, this.maxPower);
            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(-5, 100)]
        [TestCase(-1, 400)]
        [TestCase(0, 600)]
        [TestCase(701, 600)]
        [TestCase(750, 749)]
        public void TurnOn_WasOffOutOfRangePower_ThrowsException(int power, int maxPower)
        {
            this.maxPower = maxPower;
            uut = new PowerTube(output, this.maxPower);
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }

    }
}