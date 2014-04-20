/*
 * Chipknip.cpp
 *
 *  Created on: Apr 20, 2014
 *      Author: vagrant
 */

#include "Chipknip.h"

Chipknip::Chipknip(int initial_value) :
		credits(initial_value) {
	// TODO Auto-generated constructor stub

}

Chipknip::~Chipknip() {
	// TODO Auto-generated destructor stub
}

bool Chipknip::HasValue(int p) {
	return credits >= p;
}

void Chipknip::Reduce(int p) {
	credits -= p;
}
