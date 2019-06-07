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
 *
 */
#include <AccelStepper.h>


// Pin defines
#define buzzer 5
#define ENA_PIN 6
#define STOP_btn 7
#define inc_btn 8
#define STEP_PIN 9
#define DIR_PIN 10
#define low_limit 11
#define inch_limit 12
#define motor_relay 13


// Macros for step indexing
#define MM 200  // 50 * 4 --> motor is on 1/4 step


// Setup a new instance of AccelStepper
AccelStepper stepper(AccelStepper::DRIVER, STEP_PIN, DIR_PIN);


// global var that will hold the number of traces from GUI
int cut_size;


void setup() {
    Serial.begin(9600);
    pinMode(low_limit, INPUT_PULLUP);
    pinMode(inch_limit, INPUT_PULLUP);
    pinMode(inc_btn, INPUT_PULLUP);
    pinMode(motor_relay, INPUT_PULLUP);
    pinMode(STOP_btn, INPUT_PULLUP);
    pinMode(buzzer, OUTPUT);
    pinMode(ENA_PIN, OUTPUT);
    digitalWrite(ENA_PIN, HIGH);  // when this goes low --> motor is not enabled
    digitalWrite(buzzer, LOW);

    // Stepper specs
    stepper.setMaxSpeed(1000);
    stepper.setSpeed(1000);
    stepper.setAcceleration(1000);

    // Cutting Sequence
    /*
    homeMotor();
    makeReferenceCut();
    cutElement(3, 8);
    homeMotor();
    */

}


/* Testing the GUI -- sends commands but does not execute them */
void checkSerial() {
    if (Serial.available() > 0) {  //id data is available to read

        int val = Serial.read();

        if (val < 100) {
            // Number of traces has been sent via serial
            cut_size = (val + 1) / 2;
        } else {
            // A button has been pressed
            switch (val) {
                case 100:
                    digitalWrite(ENA_PIN, HIGH);
                    homeMotor();
                    break;
                case 101:
                    digitalWrite(ENA_PIN, HIGH);
                    makeReferenceCut();
                    break;
                case 102:
                    digitalWrite(ENA_PIN, HIGH);
                    cutElement(1, cut_size);
                    break;
                case 103:
                    digitalWrite(ENA_PIN, LOW);
                    break;
                case 104:
                    digitalWrite(ENA_PIN, HIGH);
                    break;
            }
        }
    }
}


/* Make the reference cut on the cutting board. This aligns the tape with blade, ensuring straight cuts */
void makeReferenceCut() {
    int increment = 1;

    // only move when the motor is not active
    // this shouldn't need a e-stop because the user is controlling how far to move the motor
    while (digitalRead(motor_relay)) {
        while (digitalRead(inc_btn) != HIGH) {
            stepper.moveTo(increment);
            // if the 'increment' value gets too high,
            // the motor will start turning the other direction
            increment++;  // decrease by 1 for next move if needed
            stepper.setSpeed(2000);
            stepper.runSpeedToPosition(); // run the motor CCW towards the switch
        }
    }
    stepper.setCurrentPosition(0);
    Serial.println("Cutting G4!");
    delay(2000);
}


/* Home the motor, using limits, and reset the position */
void homeMotor() {
    int homing = -1;
    bool is_home;

    // reset position
    stepper.setCurrentPosition(0);

    // working check for emergency stop -- clean this up
    while (digitalRead(low_limit)) {
        if (digitalRead(STOP_btn) != HIGH) {
            stepper.stop();
            is_home = false;
            break;
        } else {
            // seek the home limit switch
            stepper.moveTo(homing);
            homing--;  // decrease by 1 for next move if needed
            stepper.setSpeed(1500);
            stepper.runSpeedToPosition(); // run the motor CCW towards the switch
            is_home = true;
        }
    }
    // reset position
    stepper.setCurrentPosition(0);
    if (is_home) {
        Serial.println("Motor is Home");
        Serial.println("");
        stepper.moveTo(200);
        while (stepper.currentPosition() != 200) // Full speed
            stepper.run();
        stepper.stop();
        stepper.setCurrentPosition(0);

    } else {
        Serial.println("Emergency Stop Activated");
        Serial.println("");
    }
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
void cutElement(int quantity, int length) {
    int cut_length = (int)MM * length;

    // emergency stop is working -- but clean this up
    for (int i = 0; i < quantity; ++i) {
        Serial.print("The amount of steps is : ");
        Serial.print(cut_length);
        Serial.println("");
        stepper.moveTo(cut_length);
        while (stepper.currentPosition() != cut_length) // Full speed
            if (digitalRead(STOP_btn) != HIGH) {
                stepper.stop(); // stop as fast as possible: sets new target
                Serial.println("Stopping the Cutting Sequence");
                digitalWrite(ENA_PIN, LOW); // disable the motor
                break;
            } else {
                stepper.run();
            }
        stepper.stop(); // stop as fast as possible: sets new target
        delay(1000); // wait for one second -- blade will come down
        stepper.setCurrentPosition(0);
    }
}


void loop() {
    // testing GUI
    checkSerial();
}
