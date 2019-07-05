#include <AccelStepper.h>
#include "PinDefines.h"


// Setup a new instance of AccelStepper
AccelStepper stepper(AccelStepper::DRIVER, STEP_PIN, DIR_PIN);


void setup() {
	Serial.begin(9600);
	pinMode(low_limit, INPUT_PULLUP);
	pinMode(inch_limit, INPUT_PULLUP);
	pinMode(inc_btn, INPUT_PULLUP);
	pinMode(STOP_btn, INPUT_PULLUP);
	pinMode(RST, OUTPUT);
	pinMode(ENA_PIN, OUTPUT);
	pinMode(RELAY_1, OUTPUT);
	pinMode(RELAY_2, OUTPUT);
	digitalWrite(ENA_PIN, HIGH);  // when this goes low --> motor is not enabled

	// Stepper specs
	stepper.setMaxSpeed(1000);
	stepper.setSpeed(1000);
	stepper.setAcceleration(1000);
}


/* Update the cut size of the G4 element */
void updateCutSize(int traces) {
	(traces % 2 == 0) ? g4_cut_size = (traces / 2) + .5 : g4_cut_size = (traces + 1) / 2;
}


/* Update the quantity of G4 elements to cut */
void updateQuantity(int qty) {
	g4_cut_quantity = qty % 200;
}


/* Get incoming serial commands and act accordingly  */
void checkSerial() {
	if (Serial.available() > 0) {
		digitalWrite(ENA_PIN, HIGH);    // make sure motor is on

		int val = Serial.read();

		if ((val < 100) || (val > 200)) {
			// when val < 100 we have been sent the number of traces
			// when val is > 200 we have been sent the quantity
			(val > 200) ? updateQuantity(val) : updateCutSize(val);
		}
		else {
			// A button has been pressed
			switch (val) {
			case 100:   // home motor
				homeMotor();
				break;
			case 101:   // reference cut --> move the board then hit blade down for ref. cut
				makeReferenceCut(1);
				break;
			case 102:   // cut elements
				cutElement(g4_cut_quantity, g4_cut_size);
				break;
			case 103:   // disable the motor
				digitalWrite(ENA_PIN, LOW);
				break;
			case 104:   // enable the motor
				digitalWrite(ENA_PIN, HIGH);
				break;
			case 105:   // move on millimeter
				moveOneMM();
				break;
			case 106:   // move motor back to home --> controlled by index btn
				makeReferenceCut(-1);
				break;
			case 107:   // move the actuator down, then back up
				bladeDownAndUp();
				break;
			case 108:   // move the actuator up
			  //bladeUp(); not used, add something useful
				break;
			case 109:   // one inch move
				moveStepperTo(INCH);
				break;
			case 110:   // .1 inch move
				moveStepperTo(TENTH);
				break;
			case 111:   // .01 inch move
				moveStepperTo(TEN_MIL);
				break;
			case 112:   // .001 inch move
				moveStepperTo(ONE_MIL);
				break;
				// inch moves for the other direction
			case 113:
				moveStepperTo(-INCH);
				break;
			case 114:
				moveStepperTo(-TENTH);
				break;
			case 115:
				moveStepperTo(-TEN_MIL);
				break;
			case 116:
				moveStepperTo(-ONE_MIL);
				break;
			case 117:   // emergency stop, by resetting the arduino
				digitalWrite(RST, LOW);
				break;
			case 118:   // move the table to the new part distance
				moveStepperTo(g4_cut_size);
				break;
			case 119:   // 1/8th of a Mil move -- right
			  // currently not working, tweak or just abandon
				moveStepperTo(0.003);
				break;
			case 120:   // 1/8th of a Mil move -- left
				moveStepperTo(-0.003);
				break;
			case 121:
				calibrateStepperAccuracy();
			default:
				break;
			}
		}
	}
}


/* Move the actuator down, then back up */
void bladeDownAndUp() {
	// this seems to be working and getting rid of weird bug
	// send the blade down
	digitalWrite(RELAY_1, HIGH);
	digitalWrite(RELAY_2, LOW);
	delay(blade_down_delay);

	// stop the actuator
	digitalWrite(RELAY_1, LOW);
	digitalWrite(RELAY_2, LOW);
	delay(2000);    // 2 second wait, hopefully get rid of timing bug

	// back up
	digitalWrite(RELAY_2, HIGH);
	digitalWrite(RELAY_1, LOW);
	delay(blade_up_delay);

	// stop the actuator
	digitalWrite(RELAY_1, LOW);
	digitalWrite(RELAY_2, LOW);
}


/*
 * Move the stepper to the desired location
 *      @param length = distance to travel, in MM
 *
 */
void moveStepperTo(float c_length) {
	// add the extra .25 inch to every move -- test this out!
	//(c_length > 1) ? c_length += 6.35 : c_length -= 6.35;

	// int n_distance = MM * c_length;
	//long n_distance = MM * c_length;
	float n_distance = MM * c_length; // float seems to be working the best

	stepper.moveTo(n_distance);
	while (stepper.currentPosition() != n_distance) // Full speed
		stepper.run();
	stepper.stop();
	stepper.setCurrentPosition(0);
}


/* Make the reference cut on the cutting board. This aligns the tape with blade, ensuring straight cuts */
void makeReferenceCut(int increment) {

	// wait for hard reset
	while (1) {
		while (digitalRead(inc_btn) != HIGH) {
			stepper.moveTo(increment);
			stepper.setSpeed(7000);
			stepper.runSpeedToPosition();
			stepper.setCurrentPosition(0);  // reset position
		}
	}
	stepper.setCurrentPosition(0);
	delay(2000);
}


/* 
* Attempt to calibrate the stepper motor
* Also attempt at sending out serial data to the GUI
*/
void calibrateStepperAccuracy() {
	long half_inch = TENTH * 5;
	
	// because the the GUI is in VB, Serial.println() will work
	// will be costly though
	stepper.setCurrentPosition(0);
	stepper.moveTo(half_inch);
	while (stepper.currentPosition() != half_inch) // Full speed
		stepper.run();
		Serial.write(stepper.currentPosition());	// this may slow down perfomance but check it out anyway
	stepper.stop();
	stepper.setCurrentPosition(0);
	Serial.write("done");
}


/* Home the motor, using limits, and reset the position */
void homeMotor() {
	int homing = -1;

	// reset position
	stepper.setCurrentPosition(0);

	// working check for emergency stop -- clean this up
	while (digitalRead(low_limit)) {
		if (digitalRead(STOP_btn) != HIGH) {
			stepper.stop();
			break;
		}
		else {
			// seek the home limit switch
			stepper.moveTo(homing);
			homing--;  // decrease by 1 for next move if needed
			stepper.setSpeed(2000);
			stepper.runSpeedToPosition(); // run the motor CCW towards the switch
		}
	}
	// reset position and move to home
	stepper.setCurrentPosition(0);
	stepper.moveTo(400);
	while (stepper.currentPosition() != 400) // Full speed
		stepper.run();
	stepper.stop();
	stepper.setCurrentPosition(0);
}


/* Move the table in a series of 1mm moves */
void moveOneMM() {
	// testing precision movement
	stepper.moveTo(MM);
	while (stepper.currentPosition() != MM) // Full speed
		stepper.run();
	stepper.stop();
	stepper.setCurrentPosition(0);
}


/* Used to calculate steps per inch */
void calcMove() {
	int n_steps = 1;

	while (digitalRead(inch_limit)) {
		stepper.moveTo(n_steps);
		n_steps++;
		stepper.setSpeed(750);
		stepper.runSpeedToPosition();
	}
	stepper.stop();
}


/*
 * Cut the elements
 *
 *      @param quantity = number of elements to cut
 *      @param length = the length, in MM, of the G4 element
 *
 *
 */
void cutElement(int quantity, float c_length) {
	float cut_length = MM * c_length;

	for (int i = 0; i < quantity; ++i) {
		stepper.moveTo(cut_length);
		while (stepper.currentPosition() != cut_length) // Full speed
			if (digitalRead(STOP_btn) != HIGH) {
				stepper.stop(); // stop as fast as possible: sets new target
				digitalWrite(ENA_PIN, LOW); // disable the motor
				break;
			}
			else {
				stepper.run();
			}
		stepper.stop(); // stop as fast as possible: sets new target
		bladeDownAndUp();
		stepper.setCurrentPosition(0);
	}
}


void loop() {
	checkSerial();
}
