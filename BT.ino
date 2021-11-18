// If use Windows's app "Buletooth Serial Terminal"
// When send a message, please use "send button", don't press "Enter"
// Or it will send a same message 2 times!

// BT || Arduino
// EN -> 9
// TX -> 0
// RX -> 1
// Vcc -> 5v
// GND -> GND

#include  <SoftwareSerial.h>
int BT_RX = 1;
int BT_TX = 0;
SoftwareSerial BTSerial(BT_TX,BT_RX); // RX | TX
void setup()
{
  Serial.begin(9600);
  Serial.println("Enter AT commands:");
  // AT+UART? Display community mode baud rate
  // AT mode baud rate is 38400
  // Arduino write code baud rate is 115200
  BTSerial.begin(9600);  // HC-05 default speed in AT command more
}
void loop()
{
  if (BTSerial.available()){
    String indata = BTSerial.readString();
    String mode = getValue(indata,';',0);
    String data = getValue(indata,';',1);
    if (mode == "send"){
      Serial.println("---send---");
      Serial.println(data);
    }
    else if (mode == "test"){
      Serial.println("---test---");
    }
  }
}

String getValue(String data, char separator, int index)
{
  int found = 0;
  int strIndex[] = { 0, -1 };
  int maxIndex = data.length() - 1;

  for (int i = 0; i <= maxIndex && found <= index; i++) {
      if (data.charAt(i) == separator || i == maxIndex) {
          found++;
          strIndex[0] = strIndex[1] + 1;
          strIndex[1] = (i == maxIndex) ? i+1 : i;
      }
  }
  return found > index ? data.substring(strIndex[0], strIndex[1]) : "";
}
