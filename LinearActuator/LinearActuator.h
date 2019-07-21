/*
	Library to use with progressive automation's linear acutator
	P/N: PA-04-012?
*/
#include "Arduino.h"

#define down_delay 2670 
#define up_delay  2870

class Actuator {
	public:
		Actuator(int r_1, int r_2);	// constructor
		bool bladeDownAndUp(),
			 bladeDown(),
			 bladeUp(),
			 bladeStop();
	private:
		int relay_1, relay_2;
		// more var's for elapsed time
		void setOutput();
};