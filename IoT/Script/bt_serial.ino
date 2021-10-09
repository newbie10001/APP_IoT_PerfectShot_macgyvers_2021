#include <SoftwareSerial.h>

#define M1 6
#define M2 7
#define BT_RX 2
#define BT_TX 3
#define FSR 4
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
    vib = true;
    motor_loop = 0;
    //BTSerial.write("0");
    //Serial.println(trig);
  }
  if(vib){
    if(motor_loop++ == 0){
      digitalWrite(M1, HIGH);
      digitalWrite(M2, HIGH);
    }else if(motor_loop > 500){
      vib = false;
      motor_loop = 0;
      digitalWrite(M1, LOW);
      digitalWrite(M2, LOW);
    }
  }
  delay(10);
}
