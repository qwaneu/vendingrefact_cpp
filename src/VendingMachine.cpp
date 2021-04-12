/*
 * VendingMachine.cpp
 *
 *  Created on: Apr 20, 2014
 *      Author: vagrant
 */
#include <stdio.h>
#include "VendingMachine.h"
#include "CanContainer.h"
#include "Chipknip.h"

VendingMachine::VendingMachine() : payment_method(0), c(-1), chipknip(NULL) {
	// TODO Auto-generated constructor stub

}

VendingMachine::~VendingMachine() {
	// TODO Auto-generated destructor stub
}

void VendingMachine::set_value(int v) {
	payment_method = 1;
	if (c != -1) {
		c += v;
	} else {
		c = v;
	}
}
void VendingMachine::insert_chip(std::shared_ptr<Chipknip> chipknip) {
	// TODO
	// can't pay with chip in brittain
	payment_method = 2;
	this->chipknip = chipknip;
}

// delivers the can if all ok {
Can VendingMachine::deliver(Choice choice) {
	Can res = NoCan;
	//
	//step 1: check if choice exists {
	//
	if (cans[choice]) {
		//
		// step2 : check price
		//
		if (cans[choice]->Price == 0) {
			res = cans[choice]->Type;
			// or price matches
		} else {

			switch (payment_method) {
			case 1: // paying with coins
				if (c != -1 && cans[choice]->Price <= c) {
					res = cans[choice]->Type;
					c -= cans[choice]->Price;
				}
				break;
			case 2: // paying with chipknip -
				// TODO: if this machine is in belgium this must be an error {
				if (chipknip->HasValue(cans[choice]->Price)) {
					chipknip->Reduce(cans[choice]->Price);
					res = cans[choice]->Type;
				}
				break;
			default:
				// TODO: Is this a valid situation?:
				//   larry forgot the } else { clause
				//   i added it, but i am acutally not sure as to wether this is a problem
				//   unknown payment
				break;
				//     i think(i) nobody inserted anything
			}
		}
	} else {
		res = NoCan;
	}

	//
	// step 3: check stock
	//
	if (res != NoCan) {
		if (cans[choice]->Amount <= 0) {
			res = NoCan;
		} else {
			cans[choice]->Amount -= 1;
		}
	}

	// if can is set then return {
	// otherwise we need to return the None
	if (res == NoCan) {
		return NoCan;
	}

	return res;
}

int VendingMachine::get_change() {
	int to_return = 0;
	if (c > 0) {
		to_return = c;
		c = 0;
	}
	return to_return;
}
void VendingMachine::configure(Choice choice, Can c, int n) {
	configure(choice, c, n, 0);
}
void VendingMachine::configure(Choice choice, Can c, int n, int price) {
	this->price = price;
	if (cans[choice]) {
		cans[choice]->Amount += n;
		return;
	}
	CanContainer *can = new CanContainer();
	can->Type = c;
	can->Amount = n;
	can->Price = price;
	cans[choice] = std::shared_ptr<CanContainer>(can);
}


