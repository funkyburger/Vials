using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Server.Utilities;
using Vials.Shared.Exceptions;

namespace Vials.Server.UnitTests.Utilities
{
    public class RandomGeneratorTests
    {
        [Test]
        public void ReturnsAPseudoRandomNumber()
        {
            var generator = new PseudoRandomGenerator();

            var random = generator.Generate(123);
            random.ShouldBe(1057160183);
            random = generator.GenerateNext();
            random.ShouldBe(2114320339);
        }

        [Test]
        public void UseWithoutSeedThrowsException()
        {
            var exceptionThrown = false;
            var generator = new PseudoRandomGenerator();

            try
            {
                var random = generator.GenerateNext();
            }
            catch (RandomGenerationException)
            {
                exceptionThrown = true;
            }

            exceptionThrown.ShouldBeTrue();
        }

        [Test]
        public void ReuseWithSeedResetsSequence()
        {
            var seed = new Random().Next();
            var randoms = new int[11];
            var generator = new PseudoRandomGenerator();

            randoms[0] = generator.Generate(seed);
            for(int i = 0; i < 10; i++)
            {
                randoms[i + 1] = generator.GenerateNext();
            }

            generator.Generate(seed).ShouldBe(randoms[0]);
            for (int i = 0; i < 10; i++)
            {
                generator.GenerateNext().ShouldBe(randoms[i + 1]);
            }
        }

        [Test]
        public void GeneratorIsDeterministic()
        {
            var seed = new Random().Next();
            var randoms = new int[11];
            var generator = new PseudoRandomGenerator();
            var otherGenerator = new PseudoRandomGenerator();

            randoms[0] = generator.Generate(seed);
            for (int i = 0; i < 10; i++)
            {
                randoms[i + 1] = generator.GenerateNext();
            }

            otherGenerator.Generate(seed).ShouldBe(randoms[0]);
            for (int i = 0; i < 10; i++)
            {
                otherGenerator.GenerateNext().ShouldBe(randoms[i + 1]);
            }
        }

        [Test]
        public void GeneratorIsSomewhatEquilibrated()
        {
            var seed = new Random().Next();
            var generator = new PseudoRandomGenerator();
            var randoms = new List<int>();

            randoms.Add(generator.Generate(seed) % 100);
            for(int i = 0; i < 100000; i++)
            {
                randoms.Add(generator.GenerateNext() % 100);
            }

            var average = (double)randoms.Sum() / (double)randoms.Count;
            average.ShouldBeInRange(-10d, 10d);
        }
    }
}
