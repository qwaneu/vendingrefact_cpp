#include "gtest/gtest.h"
#include <set>
#include "vending_machine.h"

class VendingMachineTest: public ::testing::Test {
protected:
private:
	VendingMachine machine;
};

TEST_F(VendingMachineTest, choiceless_machine_delivers_nothing) {
	ASSERT_EQ(NoCan, machine.deliver(ColaChoice));
	ASSERT_EQ(NoCan, machine.deliver(FantaChoice));
}

TEST_F(VendingMachineTest, delivers_can_of_choice) {
	machine.configure(ColaChoice, ColaCan, 10);
	machine.configure(FantaChoice, FantaCan, 10);
	machine.configure(SpriteChoice, SpriteCan, 10);
	ASSERT_EQ(ColaCan, machine.deliver(ColaChoice));
	ASSERT_EQ(FantaCan, machine.deliver(FantaChoice));
	ASSERT_EQ(SpriteCan, machine.deliver(SpriteChoice));
}

TEST_F(VendingMachineTest, delivers_nothing_when_making_invalid_choice) {
	machine.configure(ColaChoice, ColaCan, 10);
	machine.configure(FantaChoice, FantaCan, 10);
	machine.configure(SpriteChoice, SpriteCan, 10);
	ASSERT_EQ(NoCan, machine.deliver(BeerChoice));
}

TEST_F(VendingMachineTest, delivers_nothing_when_not_paid) {
	machine.configure(FantaChoice, FantaCan, 10, 2);
	machine.configure(SpriteChoice, SpriteCan, 10, 1);

	ASSERT_EQ(NoCan, machine.deliver(FantaChoice));
}

TEST_F(VendingMachineTest, delivers_Fanta_when_paid) {
	machine.configure(SpriteChoice, SpriteCan, 10, 1);
	machine.configure(FantaChoice, FantaCan, 10, 2);

	machine.set_value(2);
	ASSERT_EQ(FantaCan, madeliverchine(FantaChoice));
	ASSERT_EQ(NoCan, machine.deliver(SpriteChoice));
}

TEST_F(VendingMachineTest, delivers_Sprite_when_paid) {
	machine.configure(SpriteChoice, SpriteCan, 10, 1);
	machine.configure(FantaChoice, FantaCan, 10, 2);

	machine.set_value(2);
	ASSERT_EQ(SpriteCan, machine.deliver(SpriteChoice));
	ASSERT_EQ(SpriteCan, machine.deliver(SpriteChoice));
	ASSERT_EQ(NoCan, machine.deliver(SpriteChoice));
}

TEST_F(VendingMachineTest, add_payments) {
	machine.configure(SpriteChoice, SpriteCan, 10, 1);
	machine.configure(FantaChoice, FantaCan, 10, 2);

	machine.set_value(1);
	machine.set_value(1);
	ASSERT_EQ(SpriteCan, machine.deliver(SpriteChoice));
	ASSERT_EQ(SpriteCan, machine.deliver(SpriteChoice));
	ASSERT_EQ(NoCan, machine.deliver(SpriteChoice));
}

TEST_F(VendingMachineTest, returns_change) {
	machine.configure(SpriteChoice, SpriteCan, 10, 1);
	machine.set_value(2);
	ASSERT_EQ(2, machine.get_change());
	ASSERT_EQ(0, machine.get_change());
}

TEST_F(VendingMachineTest, stock) {
	machine.configure(SpriteChoice, SpriteCan, 1, 0);
	ASSERT_EQ(SpriteCan, machine.deliver(SpriteChoice));
	ASSERT_EQ(NoCan, machine.deliver(SpriteChoice));
}

TEST_F(VendingMachineTest, add_stock) {
	machine.configure(SpriteChoice, SpriteCan, 1, 0);
	machine.configure(SpriteChoice, SpriteCan, 1, 0);
	ASSERT_EQ(SpriteCan, machine.deliver(SpriteChoice));
	ASSERT_EQ(SpriteCan, machine.deliver(SpriteChoice));
	ASSERT_EQ(NoCan, machine.deliver(SpriteChoice));
}

TEST_F(VendingMachineTest, checkout_chip_if_chipknip_inserted) {
	machine.configure(SpriteChoice, SpriteCan, 1, 1);
	Chipknip chip = new Chipknip(10);
	machine.insert_chip(chip);
	ASSERT_EQ(SpriteCan, machine.deliver(SpriteChoice));
	ASSERT_EQ(9, creditschip);
}

TEST_F(VendingMachineTest, checkout_chip_empty) {
	machine.configure(SpriteChoice, SpriteCan, 1, 1);
	Chipknip chip = new Chipknip(0);
	machine.insert_chip(chip);
	ASSERT_EQ(NoCan, machine.deliver(SpriteChoice));
	ASSERT_EQ(0, creditschip);
}
