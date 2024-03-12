#include <Servo.h>


Servo Door1; //Hall
Servo Door2; //Kitchen    
int ledPin1 = 10; //Channel 4-Light-kitchen
int ledPin2 = 11; //Channel 3-Light-hall
int ledPin3 = 12; //Channel 2-Light-room
int fan = 13; //Channel 1-Fan-room&hall
int data;
int sensorValue;

#define Threshold 400

#define MQ2pin 0

void setup() {
  Serial.begin(9600);
  pinMode(ledPin1, OUTPUT);
  pinMode(ledPin2, OUTPUT);
  pinMode(ledPin3, OUTPUT);
  digitalWrite(ledPin1, HIGH);
  digitalWrite(ledPin3, HIGH);
  digitalWrite(ledPin2, HIGH);
  Door1.attach(3);
  Door2.attach(5);
  Door1.write(90);
  Door2.write(90);
  pinMode(fan, OUTPUT);
  delay(20000);   
  
}

void loop() {
  hall();
  kitchen();
  bedroom();
  data=Serial.read();
}

void hall(){
  if (data == 130) digitalWrite(ledPin2, HIGH);
  else if (data == 131) digitalWrite(ledPin2, LOW);
  else if (data == 120) digitalWrite(fan, HIGH);
  else if (data == 121) digitalWrite(fan, LOW);
  else if (data == 110) Door1.write(90);
  else if (data == 111) Door1.write(180);
}


void kitchen(){
  sensorValue = analogRead(MQ2pin);
  Serial.println(sensorValue);
  //if(sensorValue > Threshold) Serial.println(sensorValue); 
  switch(data){
    case 210:
      Door2.write(90); 
      break;
    case 211:
      Door2.write(180);
      break;
    case 230:
      digitalWrite(ledPin1, HIGH);
      break;
    case 231:
      digitalWrite(ledPin1, LOW);
      break;
  }
}

void bedroom(){
  switch(data){
    case 320:
      digitalWrite(12, HIGH); 
      break;
    case 321:
      digitalWrite(12, LOW);
      break;
    case 330:
      digitalWrite(9, LOW);
      break;
    case 331:
      digitalWrite(9, HIGH);
      break;
  }
}

