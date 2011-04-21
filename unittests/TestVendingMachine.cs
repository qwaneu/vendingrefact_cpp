using System;
using System.Collections.Generic;
using NUnit.Framework;
using Vender;

namespace Vender.UnitTests
{

[TestFixture]
public class VendingMachineTest {
  private VendingMachine machine;

  [SetUp]
  public void Setup() {
    machine = new VendingMachine();
  }
  
  [Test]
  public void Testchoiceless_machine_delivers_nothing() {
    Assert.AreEqual(Can.none, machine.deliver(Choice.cola));
    Assert.AreEqual(Can.none, machine.deliver(Choice.fanta));
  }

  [Test]
  public void Testdelivers_can_of_choice() {
    machine.configure(Choice.cola, Can.cola, 10);
    machine.configure(Choice.fanta, Can.fanta, 10);
    machine.configure(Choice.sprite, Can.sprite, 10);
    Assert.AreEqual(Can.cola, machine.deliver(Choice.cola));
    Assert.AreEqual(Can.fanta, machine.deliver(Choice.fanta));
    Assert.AreEqual(Can.sprite, machine.deliver(Choice.sprite));
  }
  
  [Test]
  public void Testdelivers_nothing_when_making_invalid_choice() {
    machine.configure(Choice.cola, Can.cola, 10);
    machine.configure(Choice.fanta, Can.fanta, 10);
    machine.configure(Choice.sprite, Can.sprite, 10);
    Assert.AreEqual(Can.none, machine.deliver(Choice.beer));
  }

  [Test]
  public void Testdelivers_nothing_when_not_paid() {
    machine.configure(Choice.fanta, Can.fanta, 10, 2);
    machine.configure(Choice.sprite, Can.sprite, 10, 1);

    Assert.AreEqual(Can.none, machine.deliver(Choice.fanta));
  }

  [Test]
  public void Testdelivers_fanta_when_paid() {
    machine.configure(Choice.sprite, Can.sprite, 10, 1);
    machine.configure(Choice.fanta, Can.fanta, 10, 2);

    machine.set_value(2);
    Assert.AreEqual(Can.fanta, machine.deliver(Choice.fanta));
    Assert.AreEqual(Can.none, machine.deliver(Choice.sprite));
  }

  [Test]
  public void Testdelivers_sprite_when_paid() {
    machine.configure(Choice.sprite, Can.sprite, 10, 1);
    machine.configure(Choice.fanta, Can.fanta, 10, 2);

    machine.set_value(2);
    Assert.AreEqual(Can.sprite, machine.deliver(Choice.sprite));
    Assert.AreEqual(Can.sprite, machine.deliver(Choice.sprite));
    Assert.AreEqual(Can.none, machine.deliver(Choice.sprite));
  }

  [Test]
  public void Testadd_payments() {
    machine.configure(Choice.sprite, Can.sprite, 10, 1);
    machine.configure(Choice.fanta, Can.fanta, 10, 2);

    machine.set_value(1);
    machine.set_value(1);
    Assert.AreEqual(Can.sprite, machine.deliver(Choice.sprite));
    Assert.AreEqual(Can.sprite, machine.deliver(Choice.sprite));
    Assert.AreEqual(Can.none, machine.deliver(Choice.sprite));
  }

  [Test]
  public void Testreturns_change() {
    machine.configure(Choice.sprite, Can.sprite, 10, 1);
    machine.set_value(2);
    Assert.AreEqual(2, machine.get_change());
    Assert.AreEqual(0, machine.get_change());
  }

  [Test]
  public void Teststock() {
    machine.configure(Choice.sprite, Can.sprite, 1, 0);
    Assert.AreEqual(Can.sprite, machine.deliver(Choice.sprite));
    Assert.AreEqual(Can.none, machine.deliver(Choice.sprite));
  }

  [Test]
  public void Testadd_stock() {
    machine.configure(Choice.sprite, Can.sprite, 1, 0);
    machine.configure(Choice.sprite, Can.sprite, 1, 0);
    Assert.AreEqual(Can.sprite, machine.deliver(Choice.sprite));
    Assert.AreEqual(Can.sprite, machine.deliver(Choice.sprite));
    Assert.AreEqual(Can.none, machine.deliver(Choice.sprite));
  }

  [Test]
  public void Testcheckout_chip_if_chipknip_inserted() {
    machine.configure(Choice.sprite, Can.sprite, 1, 1);
    Chipknip chip = new Chipknip(10);
    machine.insert_chip(chip);
    Assert.AreEqual(Can.sprite, machine.deliver(Choice.sprite));
    Assert.AreEqual(9, chip.credits);
  }
  
  [Test]
  public void Testcheckout_chip_empty() {
    machine.configure(Choice.sprite, Can.sprite, 1, 1);
    Chipknip chip = new Chipknip(0);
    machine.insert_chip(chip);
    Assert.AreEqual(Can.none, machine.deliver(Choice.sprite));
    Assert.AreEqual(0, chip.credits);
  }
}

}
