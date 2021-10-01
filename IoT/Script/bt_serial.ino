#include <SoftwareSerial.h>

int vib_motor = 7;
int BT_RX = 2;
int BT_TX = 3;
int FSR = A0;
SoftwareSerial BTSerial(BT_RX, BT_TX);
void setup() {
  // put your setup code here, to run once:
  BTSerial.begin(9600);
  pinMode(vib_motor, OUTPUT);
  pinMode(FSR, INPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(BTSerial.available()){
    float trig = analogRead(FSR);
    if(trig > 500){
      BTSerial.write("0");
      digitalWrite(vib_motor, HIGH);
      delay(300);
      digitalWrite(vib_motor, LOW);
    }
  }
}