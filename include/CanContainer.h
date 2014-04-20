/*
 * CanContainer.h
 *
 *  Created on: Apr 20, 2014
 *      Author: vagrant
 */

#ifndef CANCONTAINER_H_
#define CANCONTAINER_H_
#include "enums.h"

class CanContainer {
public:
	CanContainer();
	virtual ~CanContainer();

    Can Type;
    int Price;
    int Amount;
}
};

#endif /* CANCONTAINER_H_ */
