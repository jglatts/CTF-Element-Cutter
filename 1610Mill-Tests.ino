/*
 *
 * Z Axis CTF Cutter Version 1.0
 * Using 1610 CNC Kit
 *
 * Author: John Glatts
 * Date: 5/22/19
 *
 * Working sketch to go back home after 3 'parts' have been cut
 * POS DIR = Towards Motor
 * NEG DIR = Away from Motor
 *
 * Need to calc. steps per inch
 *  - I.E -> how many steps do go one inch
 *  Current test
 *      - use the dirty way and try to record individual steps
 *      - First test:
 *          - 2653 steps = 1 inch
 *      - Second test:
 *          - 2540 steps = 1 inch --> this seems to be working good
 *
 */
#include <AccelStepper.h>


#define low_limit 11
#define inch_limit 12
#define STEP_PIN 9
#define DIR_PIN 10


// macros for step indexingg
#define INCH 2540


AccelStepper stepper(AccelStepper::DRIVER, STEP_PIN, DIR_PIN);


int increment = -1;


void setup() {
    Serial.begin(9600);
    pinMode(low_limit, INPUT_PULLUP);
    pinMode(inch_limit, INPUT_PULLUP);
    // lower speed to use with smallMove()
    stepper.setMaxSpeed(1000);
    stepper.setSpeed(1000);
    stepper.setAcceleration(1000);
    cutElement(1, .04); // testing a 1mm move, which is .04 inches  
    //homeMotor();
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
        stepper.setSpeed(700);
        stepper.runSpeedToPosition(); // run the motor CCW towards the switch
    }
    stepper.setCurrentPosition(0);

    // move the stepper a bit away
    stepper.moveTo(-100);
    while (stepper.currentPosition() != -100) // Full speed
        stepper.run();
    stepper.stop();
    stepper.setCurrentPosition(0);
    delay(1000);  // set up the caliber
}


/* Move the table in a series of 1 inch moves */
void moveOneInch() {
    // testing precision movement
    stepper.moveTo(-INCH);
    while (stepper.currentPosition() != -INCH) // Full speed
        stepper.run();
    stepper.stop(); // stop as fast as possible: sets new target
    delay(1000); // second wait -- blade will come down
    stepper.setCurrentPosition(0);  // reset the position and move on to another part
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

/*
 * Cut the elements
 *
 *      @param quantity = number of elements to cut
 *      @param length = the length, in inches, of the G4 element
 *
 *
 */
void cutElement(int quantity, float length) {
    int cut_length = (int)INCH * length;

    for (int i = 0; i < quantity; ++i) {
        // debug
        Serial.print("The amount of steps is : ");
        Serial.print(cut_length);
        Serial.println("");
        stepper.moveTo(-cut_length);
        while (stepper.currentPosition() != -cut_length) // Full speed
            stepper.run();
        stepper.stop(); // stop as fast as possible: sets new target
        delay(1000); // second wait -- blade will come down
        stepper.setCurrentPosition(0);
    }
}

void loop() {
}
