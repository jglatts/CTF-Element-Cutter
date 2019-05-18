/*
 *
 * Z Axis CTF Cutter Version 1.0
 * Author: John Glatts
 * Date: 5/17/19
 *
 * Working sketch to go back home after 3 'parts' have been cut
 *
 */
#include <AccelStepper.h>


AccelStepper stepper(AccelStepper::DRIVER, 9, 10);


void setup() {
    stepper.setMaxSpeed(1200);
    stepper.setSpeed(1000);
    stepper.setAcceleration(1000);
    testGroups();
    homeMotor();
}


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


void homeMotor() {
    stepper.setCurrentPosition(0);
    stepper.moveTo(-100000);
    while (stepper.currentPosition() != -100000) // Full speed
        stepper.run();
    stepper.stop(); // Stop as fast as possible: sets new target
}


void loop() {

}
