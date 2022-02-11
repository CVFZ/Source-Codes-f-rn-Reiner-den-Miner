include <Arduino.h>
#include <Wire.h>
#define UP 0
#define DOWN 1
#define address B1001101
boolean stringComplete;
String inputString="";
boolean fading = true;
int value;
int direction;
unsigned long delayValue=12;
void setup() {
// put your setup code here, to run once:
pinMode(9,OUTPUT);
Serial.begin(9600);
Wire.begin();
inputString.reserve(200);
}
byte val = 0;
void loop() {
// put your main code here, to run repeatedly:
if (stringComplete) {
Serial.println("");
if (inputString.equals("on")) {
fading=true;
} else if (inputString.equals("off")) {
fading=false;
} else if (inputString.equals("up")) {
delayValue-=1;
} else if (inputString.equals("down")) {
if(delayValue>0) {
delayValue+=1;
}
} else if (inputString.equals("getTemp")) {
Serial.println("Temperatur in Grad Celsius: ");
Wire.beginTransmission(address);
Wire.write(val);
Wire.requestFrom(address, 1);
if (Wire.available()) {
Serial.println(Wire.read());
}
} else {
Serial.println("Dieser Befehl wurde nicht verstanden!");
}

inputString="";
stringComplete=false;
}
if (fading) {
if (value==0) {
direction=UP;
} else if (value==255) {
direction=DOWN;
}
analogWrite(9,value);
delay(delayValue);
switch (direction)
{
case UP: value++; break;
case DOWN: value--; break;
}
}
}
void serialEvent() {
while (Serial.available()) {
char inChar = (char)Serial.read();
if(inChar != '\n' && inChar != '\r'){
inputString += inChar;
} else {
stringComplete = true;
}
}
}
