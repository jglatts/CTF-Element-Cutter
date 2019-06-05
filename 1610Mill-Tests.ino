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
 *  Current test
 *      - First test:
 *          - 2653 steps = 1 inch
 *      - Second test:
 *          - 2540 steps = 1 inch --> this seems to be working good
 *
 * 6/3/19 New Setup
 *  - Use MM instead of inches
 *  - Half to be careful when selecting new steps/rev speed, i.e half-step
 *  - Have a macro setup just for MM's and pass in the length in MM
 *      - 200 steps = 4mm
 *      - 1mm = 50 steps
 *
 *  - The best results come from 1/16th stepping. The stepper moves slower,
 *    but is able to reach the target more accurately
 *
 */
#include <AccelStepper.h>


// Pin defines
#define STEP_PIN 9
#define DIR_PIN 10
#define low_limit 11
#define inch_limit 12


// Macros for step indexing
#define MM 800  // 50 * 16 --> motor is on 1/16 step


// Setup a new instance of AccelStepper
AccelStepper stepper(AccelStepper::DRIVER, STEP_PIN, DIR_PIN);


void setup() {
    Serial.begin(9600);
    pinMode(low_limit, INPUT_PULLUP);
    pinMode(inch_limit, INPUT_PULLUP);
    // lower speed to use with smallMove()
    stepper.setMaxSpeed(1000);
    stepper.setSpeed(1000);
    stepper.setAcceleration(1000);
    homeMotor();
    //cutElement(1, 4);  // 4mm move, measuring with calipers
    //homeMotor();
}


/* Home the motor, using limits, and reset the position */
void homeMotor() {
    int homing = -1;

    while (digitalRead(low_limit)) {
        stepper.moveTo(homing);
        homing--;  // decrease by 1 for next move if needed
        stepper.setSpeed(5000);
        stepper.runSpeedToPosition(); // run the motor CCW towards the switch
    }
    stepper.setCurrentPosition(0);

    // move the stepper a bit away
    stepper.moveTo(700);
    while (stepper.currentPosition() != 400) // Full speed
        stepper.run();
    stepper.stop();
    stepper.setCurrentPosition(0);
    delay(1000);  // set up the caliber
}


/* Move the table in a series of 1mm moves */
void moveOneMM() {
    // testing precision movement
    stepper.moveTo(MM);
    while (stepper.currentPosition() != MM) // Full speed
        stepper.run();
    stepper.stop(); // stop as fast as possible: sets new target
    delay(1000); // second wait -- blade will come down
    stepper.setCurrentPosition(0);  // reset the position and move on to another part
}


/* Used to calculate steps per inch */
void calcMove() {
    // set-up a limit switch 1" away
    // count steps and then further refine
    int n_steps = 1;

    while (digitalRead(inch_limit))  {
        stepper.moveTo(n_steps);
        n_steps++;  // decrease by 1 for next move if needed
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
 *      @param length = the length, in MM, of the G4 element
 *
 *
 */
void cutElement(int quantity, float length) {
    int cut_length = (int)MM * length;

    for (int i = 0; i < quantity; ++i) {
        // debug
        Serial.print("The amount of steps is : ");
        Serial.print(cut_length);
        Serial.println("");
        stepper.moveTo(cut_length);
        while (stepper.currentPosition() != cut_length) // Full speed
            stepper.run();
        stepper.stop(); // stop as fast as possible: sets new target
        delay(1000); // wait for one second -- blade will come down
        stepper.setCurrentPosition(0);
    }
}


void loop() {
}
