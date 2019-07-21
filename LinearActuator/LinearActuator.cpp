#include "Arduino.h"
#include "LinearActuator.h"


// set-up constructor 
Actuator::Actuator(int r_1, int r_2) {
	relay_1 = r_1;
	relay_2 = r_2;
	setOutput();
}


// may not need the constructer here
void Actuator::setOutput() {
	pinMode(relay_1, OUTPUT);
	pinMode(relay_2, OUTPUT);
}


/*
	Send the blade down and up 
	Returns True if blade went down all the way
	False if anything else
*/
bool Actuator::bladeDownAndUp() {
	digitalWrite(relay_1, HIGH);
	digitalWrite(relay_2, LOW);
	delay(down_delay);
	// stop the actuator
	digitalWrite(relay_1, LOW);
	digitalWrite(relay_2, LOW);
	delay(3000);    // 3 seconds off, cut that damn diving board
	// back up
	digitalWrite(relay_2, HIGH);
	digitalWrite(relay_1, LOW);
	delay(up_delay);
	// stop the actuator
	digitalWrite(relay_1, LOW);
	digitalWrite(relay_2, LOW);
	// make sure we made it 
	// add another limit-switch maybe?
	// goal is to not have to add new hardware
	return true;
}


bool Actuator::bladeDown() {
	digitalWrite(relay_1, HIGH);
	digitalWrite(relay_2, LOW);
	delay(down_delay);
	// stop the actuator
	digitalWrite(relay_1, LOW);
	digitalWrite(relay_2, LOW);
	return true;
}


bool Actuator::bladeUp() {
	digitalWrite(relay_2, HIGH);
	digitalWrite(relay_1, LOW);
	delay(up_delay);
	// stop the actuator
	digitalWrite(relay_1, LOW);
	digitalWrite(relay_2, LOW);
	return true;
}


bool Actuator::bladeStop() {
	// stop the actuator
	digitalWrite(relay_1, LOW);
	digitalWrite(relay_2, LOW);
	return true;
}
