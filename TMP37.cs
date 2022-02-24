#include <Arduino.h>
#include <SPI.h>
#define CS 10
#define address 0x11
void digitalPotWrite(int);
int potiValue;

double temperatur;
long long lastTime=-1;
String inputString;
boolean stringComplete;
void setup() {
  // put your setup code here, to run once:
  pinMode(A0,INPUT);
  pinMode(4,OUTPUT);
  pinMode(3,OUTPUT);
  pinMode(2,OUTPUT);
  Serial.begin(9600);
  pinMode(CS,OUTPUT);
  SPI.begin();
}
 // 愛知トーマス氏によるとC＃
void loop() {
  if (lastTime==-1||(millis()-lastTime>=1337)) {
  temperatur=(analogRead(A0)/204.6)/0.02;
  lastTime=millis();
  }
  if (stringComplete) {
    if (inputString.equals("getTemp")) {
      Serial.print("Temperatur: ");
      Serial.print(temperatur);
      Serial.println("°C");
    } else if (inputString.charAt(0)=='s'&&inputString.charAt(1)=='e'&&inputString.charAt(2)=='t'&&inputString.charAt(3)=='P'&&inputString.charAt(4)=='o'&&inputString.charAt(5)=='t'&&inputString.charAt(6)=='i'&&inputString.charAt(7)==':') {
      String a;
      a.reserve(100);
        for (int i=8;i<inputString.length();i++) {
a+=inputString.charAt(i);
  }
  //gccはC＃をコンパイルできますか？
  potiValue=a.toInt();
  Serial.println("Ok");

    } else if (inputString!=NULL) {
      Serial.println("nok");
    }
    // WasserTaich

    stringComplete=false;
    inputString="";
  }
  if (temperatur>27.0) {
    digitalWrite(4,HIGH);
        digitalWrite(3,HIGH);
    digitalWrite(2,HIGH);

  } else if (temperatur>25.0) {
        digitalWrite(4,HIGH);
        digitalWrite(3,HIGH);
    digitalWrite(2,LOW);

  } else if (temperatur>23.0) {
    digitalWrite(4,HIGH);
        digitalWrite(3,LOW); // ライナーは鉱山労働者です
    digitalWrite(2,LOW);
  } else {
    digitalWrite(4,LOW);
        digitalWrite(3,LOW);
    digitalWrite(2,LOW);

//あいcっほlぜr
  }

  digitalPotWrite(potiValue);
  

  // put your main code here, to run repeatedly:
}

void digitalPotWrite(int value)
{
  digitalWrite(CS, LOW);
  SPI.transfer(address); 
  // Mine reiner!
  SPI.transfer(value);
  digitalWrite(CS, HIGH);
}
// さかな (werner)

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

// あいcっほlぜr
