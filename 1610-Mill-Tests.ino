/*
 *
 * Z Axis CTF Cutter Version 1.0
 * Using 1610 CNC Kit
 *
 * Author: John Glatts
 * Date: 5/24/19
 *
 * POS DIR = Towards Motor
 * NEG DIR = Away from Motor
 *
 * Need to calc. steps per inch
 *  - I.E -> how many steps do go one inch
 *  Current test
 *      - use the dirty way and try to record individual steps
 *      - First test:
 *          - 2653 steps = 1 inch
 *
 */
#include <AccelStepper.h>


#define low_limit 11
#define inch_limit 12


AccelStepper stepper(AccelStepper::DRIVER, 9, 10);


int increment = -1;


void setup() {
    Serial.begin(9600);
    pinMode(low_limit, INPUT_PULLUP);
    pinMode(inch_limit, INPUT_PULLUP);
    // lower speed to use with smallMove()
    stepper.setMaxSpeed(1000);
    stepper.setSpeed(750);
    stepper.setAcceleration(1000);
    homeMotor();
    moveOneInch();
    homeMotor();
}


/* Pattern test */
void testGroups() {
    for (int i = 0; i < 3; ++i) {
        stepper.moveTo(50000);
        while (stepper.currentPosition() != 50000) // Full speed
            stepper.run();
        stepper.stop(); // Stop as fast as possible: sets new target
        delay(500);
        // blade will come down now
        stepper.setCurrentPosition(0);  // reset the position and move on to another part
    }
}


/* Another pattern test */
void testPattern() {
    for (int i = 0; i < 4; ++i) {
        // testing precision movement
        stepper.moveTo(-650);
        while (stepper.currentPosition() != -650) // Full speed
            stepper.run();
        stepper.stop(); // Stop as fast as possible: sets new target
        delay(250); // quarter second wait -- blade will come down
        stepper.setCurrentPosition(0);  // reset the position and move on to another part
    }
}


/* Home the motor, using limits, and reset the position */
void homeMotor() {
    int homing = 1;

    while (digitalRead(low_limit)) {
        stepper.moveTo(homing);
        homing++;  // decrease by 1 for next move if needed
        stepper.setSpeed(750);
        stepper.runSpeedToPosition(); // run the motor CCW towards the switch
    }

    stepper.setCurrentPosition(0);
    stepper.moveTo(-100);

    // move the stepper a bit away
    while (stepper.currentPosition() != -100) // Full speed
        stepper.run();
    stepper.stop();
    stepper.setCurrentPosition(0);
    delay(1000);  // set up the caliber
}


/* Move the table in a series of 1 inch moves */
void moveOneInch() {
    // only 2 groups
    for (int i = 0; i < 2; ++i) {
        // testing precision movement
        stepper.moveTo(-2653);
        while (stepper.currentPosition() != -2653) // Full speed
            stepper.run();
        stepper.stop(); // Stop as fast as possible: sets new target
        delay(250); // quarter second wait -- blade will come down
        stepper.setCurrentPosition(0);  // reset the position and move on to another part
    }

}

/* Used to calculate steps per inch */
void calcInchMove() {
    // set-up a limit switch 1" away
    // count steps and then further refine
    int n_steps = -1;

    while (digitalRead(inch_limit))  {
        stepper.moveTo(n_steps);
        n_steps--;  // decrease by 1 for next move if needed
        stepper.setSpeed(750);
        stepper.runSpeedToPosition(); // run the motor CCW towards the switch
        Serial.println(n_steps);    // debug the steps and find steps per inch
    }
    stepper.stop();
}


void loop() {
}
