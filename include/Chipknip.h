/*
 * Chipknip.h
 *
 *  Created on: Apr 20, 2014
 *      Author: vagrant
 */

#ifndef CHIPKNIP_H_
#define CHIPKNIP_H_

class Chipknip {
public:
	Chipknip(int initial_value);
	virtual ~Chipknip();
    int credits;

    bool HasValue(int p);

    void Reduce(int p);
};

#endif /* CHIPKNIP_H_ */
