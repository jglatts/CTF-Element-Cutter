import controlP5.*; //import ControlP5 library
import processing.serial.*;

Serial port;

ControlP5 cp5; //create ControlP5 object


PFont font;
PFont title_font;

void setup(){ //same as arduino program

    size(1700, 700);    //window size, (width, height)

    printArray(Serial.list());   //prints all available serial ports

    port = new Serial(this, "COM6", 9600);  //i have connected arduino to com3, it would be different in linux and mac os

    //lets add buton to empty window

    cp5 = new ControlP5(this);
    font = createFont("calibri light bold", 20);    // custom fonts for buttons and title
    title_font = createFont("calibri light bold", 65);
    
    cp5.addButton("Home")     //"red" is the name of button
            .setPosition(100, 100)  //x and y coordinates of upper left corner of button
            .setSize(245, 70)      //(width, height)
            .setFont(font)
            ;

    cp5.addButton("Reference")     //"red" is the name of button
            .setPosition(100, 200)  //x and y coordinates of upper left corner of button
            .setSize(245, 70)      //(width, height)
            .setFont(font)
            ;
    
    cp5.addButton("Move_One_MilliMeter")     //"red" is the name of button
            .setPosition(100, 300)  //x and y coordinates of upper left corner of button
            .setSize(245, 70)      //(width, height)
            .setFont(font)
            ;

    cp5.addButton("Cut_Element")     //"red" is the name of button
            .setPosition(100, 400)  //x and y coordinates of upper left corner of button
            .setSize(245, 70)      //(width, height)
            .setFont(font)
            ;

    cp5.addButton("Disable_Motor")     //"yellow" is the name of button
            .setPosition(100, 500)  //x and y coordinates of upper left corner of button
            .setSize(245, 70)      //(width, height)
            .setFont(font)
            ;
            
    cp5.addButton("Enable_Motor")     //"yellow" is the name of button
            .setPosition(100, 600)  //x and y coordinates of upper left corner of button
            .setSize(245, 70)      //(width, height)
            .setFont(font)
            ;
            
   // text field stuff
    cp5.addTextlabel("label")
       .setText("Enter Element Info")
       .setPosition(450,100)
       .setFont(createFont("Georgia",35))
      ;
     
  cp5.addTextfield("Pitch")
     .setText("0.5") 
     .setPosition(500,170)
     .setSize(200,40)
     .setFont(font)
     .setColor(color(255,0,0))
     ;
                 
  cp5.addTextfield("Traces")
     .setPosition(500,270)
     .setSize(200,40)
     .setFont(createFont("arial",20))
     .setFocus(true)
     .setAutoClear(false)
     ;

  cp5.addTextfield("Quantity")
     .setText("1")
     .setPosition(500,370)
     .setSize(200,40)
     .setFont(createFont("arial",20))
     .setAutoClear(false)
     ;
     
  cp5.addButton("Send_Traces")
     .setPosition(710,270)
     .setSize(170,30)
     .setFont(font)
     .getCaptionLabel().align(ControlP5.CENTER, ControlP5.CENTER)
     ;     
}


void draw(){  //same as loop in arduino

    background(168, 165, 170); // background color of window (r, g, b) or (0 to 255)

    //lets give title to our window
    fill(198, 37, 75);               //text color (r, g, b)
    textFont(title_font);  
    text("CTF Element Cutter", 80, 60);  // ("text", x coordinate, y coordinat)
}

//lets add some functions to our buttons
//so whe you press any button, it sends perticular char over serial port

void Home(){
    // when sending values that are not traces --> use 100+ because we will never have that many traces
    // then have a check for values >= 100
    port.write(100);
}


void Reference() {
    port.write(101);
}

void Cut_Element() {
    port.write(102);
}


void Disable_Motor(){
    port.write(103);
}

void Enable_Motor(){
    port.write(104);
}

void Move_One_MilliMeter() {
    port.write(105);
}


// change this to send_traces
void Send_Traces() {
  // automatically receives results from controller input
  // this will be the traces field
  //println("a textfield event for controller 'input' : "+theText);
  // this is working --> but send a number
  Integer traces = Integer.parseInt(cp5.get(Textfield.class,"Traces").getText());
  port.write(traces);
}
