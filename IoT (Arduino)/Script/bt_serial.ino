#include <SoftwareSerial.h>

#define M1 6
#define M2 7
int BT_RX = 2;
int BT_TX = 3;
int FSR = 4;
int motor_loop = 0;
bool vib = false;
SoftwareSerial BTSerial(BT_RX, BT_TX);
void setup() {
  // put your setup code here, to run once:
  BTSerial.begin(9600);
  Serial.begin(9600);
  pinMode(M1, OUTPUT);
  pinMode(M2, OUTPUT);
  pinMode(FSR, INPUT_PULLUP);
}

void loop() {
  // put your main code here, to run repeatedly:
  int trig = digitalRead(FSR);
  if(trig == 0){
    BTSerial.println("0");
    Serial.println("!");
    vib = true;
    motor_loop = 0;
  }
  if(vib){
    if(motor_loop++ == 0){
      Serial.println("?");
      digitalWrite(M1, HIGH);
      digitalWrite(M2, LOW);
    }else if(motor_loop > 4){
      vib = false;
      digitalWrite(M1, LOW);
      digitalWrite(M2, LOW);
    }
  }
  delay(50);
}
