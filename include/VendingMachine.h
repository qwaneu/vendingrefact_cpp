/*
 * VendingMachine.h
 *
 *  Created on: Apr 20, 2014
 *      Author: vagrant
 */

#ifndef VENDINGMACHINE_H_
#define VENDINGMACHINE_H_
#include <map>
#include "enums.h"

class Chipknip;

class VendingMachine {
public:
	VendingMachine();
	virtual ~VendingMachine();

	void set_value(int v);

	void insert_chip(Chipknip& chipknip);

	// delivers the can if all ok {
	Can deliver(Choice choice);
	int get_change();

	void configure(Choice choice, Can c, int n) {
		configure(choice, c, n, 0);
	}
	void configure(Choice choice, Can c, int n, int price);

private:
	std::map<Choice, CanContainer*> cans;
	int payment_method;
	Chipknip& chipknip;
	int c = -1;
	int price;
};

#endif /* VENDINGMACHINE_H_ */
