using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Vender.UnitTests
{
    [TestFixture]
    public class TestVendingMachine
    {
        Bin _bin;
        VendingMachine _machine;
        [SetUp]
        public void SetUp()
        {
            _bin = new Bin();
            _machine = new VendingMachine(_bin);
        }

        [Test]
        public void DeliversNothingWhenEmpty()
        {
            _machine.Choose(Choice.Cola);
            Assert.That(_bin.Fetch(), Is.EqualTo(Can.None));
        }

        [Test]
        public void DeliversCanOfChoice()
        {
            _machine.AddChoice(Choice.Cola, Can.Cola);
            _machine.AddChoice(Choice.Sinas, Can.Sinas);
            _machine.Choose(Choice.Cola);
            Assert.That(_bin.Fetch(), Is.EqualTo(Can.Cola));
            _machine.Choose(Choice.Sinas);
            Assert.That(_bin.Fetch(), Is.EqualTo(Can.Sinas));
        }
    }
   

    public class VendingMachine
    {
        private readonly Bin _bin;
        private IDictionary<Choice, Can> _choices = new Dictionary<Choice, Can>();

        public VendingMachine(Bin bin)
        {
            _bin = bin;
        }

        public void Choose(Choice choice)
        {
            if (_choices.ContainsKey(choice))
            {
                _bin.Catch(_choices[choice]);
            }
        }

        public void AddChoice(Choice choice, Can can)
        {
            _choices[choice] = can;
        }
    }

    public enum Choice
    {
        Cola,
        Sinas
    }
    public enum Can
    {
        None,
        Cola,
        Sinas
    }

    public class Bin
    {
        private Can _can = Can.None;

        public Can Fetch()
        {
            return _can;
        }

        public void Catch(Can can)
        {
            _can = can;
            
        }
    }


}