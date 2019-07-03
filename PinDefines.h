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
#define blade_down_delay 2170 
#define blade_up_delay  2370
#define MM 200  // 50 * 4 --> motor is on 1/4 step


// globals to hold information about the G4 element
double g4_cut_size;
int g4_cut_quantity;


/* 
	Maybe constexpr will be better??
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
constexpr auto blade_down_delay 2400
constexpr auto  blade_up_delay 2600
#define MM 200  // 50 * 4 --> motor is on 1/4 step

*/
