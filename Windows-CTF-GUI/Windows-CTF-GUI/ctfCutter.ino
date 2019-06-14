/*
 *
 * Z Axis CTF Cutter Version 1.0
 * Using 1610 CNC Kit
 *
 * Author: John Glatts
 * Date: 5/22/19
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
 *  - Refactor the emergency stop logic sequences
 *
 *  - Get some sort of GUI working --> wait for camera then add a raspberry pi
 *      - Arudino GUI seems to be working fine
 *
 *  - Seems that the best e-stop will be resetting the Arduino itself
 *
 *  !!!!!!!!!!!!!!!!!!!!!!!!!!  *  !!!!!!!!!!!!!!!!!!!!!!!!!!
 *  - Change the element sizes to floats
 *  !!!!!!!!!!!!!!!!!!!!!!!!!!  *  !!!!!!!!!!!!!!!!!!!!!!!!!!
 *
 */
#include <AccelStepper.h>


// Pin defines
// the board is a mega2560
#define RST 3
#define RELAY_1 4
#define RELAY_2 5
#define ENA_PIN 6
#define STOP_btn 7
#define inc_btn 8
#define STEP_PIN 9
#define DIR_PIN 10
#define low_limit 11
#define inch_limit 12
#define motor_relay 13


// Macros for step indexing
#define MM 200  // 50 * 4 --> motor is on 1/2 step


// Setup a new instance of AccelStepper
AccelStepper stepper(AccelStepper::DRIVER, STEP_PIN, DIR_PIN);


// global var that will hold the number of traces from GUI
float g4_cut_size;
int g4_cut_quantity;


void setup() {
    Serial.begin(9600);
    pinMode(low_limit, INPUT_PULLUP);
    pinMode(inch_limit, INPUT_PULLUP);
    pinMode(inc_btn, INPUT_PULLUP);
    pinMode(motor_relay, INPUT_PULLUP);
    pinMode(STOP_btn, INPUT_PULLUP);
    pinMode(RST, OUTPUT);
    pinMode(ENA_PIN, OUTPUT);
    pinMode(RELAY_1, OUTPUT);
    pinMode(RELAY_2, OUTPUT);
    digitalWrite(ENA_PIN, HIGH);  // when this goes low --> motor is not enabled
    //digitalWrite(RST, HIGH);      // when this goes low --> arudino will be reset

    // Stepper specs
    stepper.setMaxSpeed(1000);
    stepper.setSpeed(1000);
    stepper.setAcceleration(1000);
}


/* Get incoming serial commands and act accordingly  */
void checkSerial() {
    if (Serial.available() > 0) {  //id data is available to read
        digitalWrite(ENA_PIN, HIGH);    // make sure motor is on
        int val = Serial.read();

        // when val < 100 we have been sent the number of traces
        // when val is > 200 we have been sent the quantity
        // when the quantity value comes in, just use the % operator to get the true quantity
        if ( (val < 100) || (val > 200) ) {
            (val < 100) ? g4_cut_size = (val + 1) / 2 : g4_cut_quantity = val % 200;
        } else {
            // A button has been pressed
            // swap all the number values
            // with symbolic constants
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
                    bladeDown();
                    break;
                case 108:   // move the actuator up
                    bladeUp();
                    break;
                case 109:   // one inch move
                    moveStepperTo(25.4);
                    break;
                case 110:   // .1 inch move
                    moveStepperTo(2.54);
                    break;
                case 111:   // .01 inch move
                    moveStepperTo(0.254);
                    break;
                case 112:   // .001 inch move
                    moveStepperTo(0.0254);
                    break;
                    // inch moves for the other direction
                case 113:
                    moveStepperTo(-25.4);
                    break;
                case 114:
                    moveStepperTo(-2.54);
                    break;
                case 115:
                    moveStepperTo(-0.254);
                    break;
                case 116:
                    moveStepperTo(-0.0254);
                    break;
                case 117:   // emergency stop, by resetting the arduino
                    digitalWrite(RST, LOW);
                    break;
                default:
                    break;
            }
        }
    }
}


/* Move the actuator down */
void bladeDown() {
    digitalWrite(RELAY_1, HIGH);
    digitalWrite(RELAY_2, LOW);
    delay(1140);
    bladeUp();      // come back up
}


/* Move the actuator up */
void bladeUp() {
    digitalWrite(RELAY_1, LOW);
    digitalWrite(RELAY_2, HIGH);
    delay(1500);
    stopActuator();
}


/* Stop all movement off the actuator */
void stopActuator() {
    digitalWrite(RELAY_1, LOW);
    digitalWrite(RELAY_2, LOW);
}


/*
 * Move the stepper to the desired location
 *      @param length = distance to travel, in MM
 *
 */
void moveStepperTo(float length) {
    // add equation to get the mm size in steps/mm
    int n_distance = MM * length;

    stepper.moveTo(n_distance);
    while (stepper.currentPosition() != n_distance) // Full speed
        stepper.run();
    stepper.stop();
    stepper.setCurrentPosition(0);
}


/* Make the reference cut on the cutting board. This aligns the tape with blade, ensuring straight cuts */
void makeReferenceCut(int increment) {

    // only move when the motor is not active
    // this shouldn't need a e-stop because the user is controlling how far to move the motor
    while (digitalRead(motor_relay)) {
        while (digitalRead(inc_btn) != HIGH) {
            stepper.moveTo(increment);
            stepper.setSpeed(3000);
            stepper.runSpeedToPosition();
            stepper.setCurrentPosition(0);  // reset position
        }
    }
    stepper.setCurrentPosition(0);
    delay(2000);
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
        } else {
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
    // reset position
    stepper.setCurrentPosition(0);
}


/* Move the table in a series of 1mm moves */
void moveOneMM() {
    // testing precision movement
    stepper.moveTo(MM);
    while (stepper.currentPosition() != MM) // Full speed
        stepper.run();
    stepper.stop(); // stop as fast as possible: sets new target
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
    float cut_length = MM * length;

    // emergency stop is working -- but clean this up
    for (int i = 0; i < quantity; ++i) {
        stepper.moveTo(cut_length);
        while (stepper.currentPosition() != cut_length) // Full speed
            if (digitalRead(STOP_btn) != HIGH) {
                stepper.stop(); // stop as fast as possible: sets new target
                digitalWrite(ENA_PIN, LOW); // disable the motor
                break;
            } else {
                stepper.run();
            }
        stepper.stop(); // stop as fast as possible: sets new target
        // add something in the future, to manually fire the blade
        // this method will not work the best in practice
        bladeDown();
        bladeUp();
        stepper.setCurrentPosition(0);
    }
}


void loop() {
    // testing GUI
    checkSerial();
}
