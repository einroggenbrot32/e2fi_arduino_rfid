#include "MFRC522.h"
#include "WiFiS3.h"
#include "WiFiSSLClient.h"

//*** Anschlüsse definieren
# define SDA 10
# define RST 9

//*** WiFi Konstanten
#define C_SSID "HotspotWebAPI"
#define C_PASS "abcdefgh"

//*** API-Konstanten
#define C_PORT 7024
IPAddress server_ip(192, 168, 137, 1);

//*** RFID-Sensor
MFRC522 mfrc522(SDA, RST);

char ssid[] = C_SSID;        // your network SSID (name)
char pass[] = C_PASS;        // your network password (use for WPA, or use as key for WEP)

int status = WL_IDLE_STATUS;
WiFiSSLClient client;

//*** diverse Funktionen für die WiFi-Verbindung/ Ausgabe von Infos
void printWifiData() {
  // print your board's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
 
  Serial.println(ip);

  // print your MAC address:
  byte mac[6];
  WiFi.macAddress(mac);
  Serial.print("MAC address: ");
  printMacAddress(mac);
}

void printCurrentNet() {
  // print the SSID of the network you're attached to:
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print the MAC address of the router you're attached to:
  byte bssid[6];
  WiFi.BSSID(bssid);
  Serial.print("BSSID: ");
  printMacAddress(bssid);

  // print the received signal strength:
  long rssi = WiFi.RSSI();
  Serial.print("signal strength (RSSI):");
  Serial.println(rssi);

  // print the encryption type:
  byte encryption = WiFi.encryptionType();
  Serial.print("Encryption Type:");
  Serial.println(encryption, HEX);
  Serial.println();
}

void printMacAddress(byte mac[]) {
  for (int i = 0; i < 6; i++) {
    if (i > 0) {
      Serial.print(":");
    }
    if (mac[i] < 16) {
      Serial.print("0");
    }
    Serial.print(mac[i], HEX);
  }
  Serial.println();
}


void setup()
{
  Serial.begin(9600);
  SPI.begin();

  //***
  Serial.println("NARVIKSOFT ZEITERFASSUNG\n");

  //*** Initialisierung des RFID-Empfängers
  mfrc522.PCD_Init();

  //*** WIFI Setup
  if (WiFi.status() == WL_NO_MODULE) {
    Serial.println("Communication with WiFi module failed!");

    while (true);
  }

  //*** versuchen, mit dem Netzwerk zu verbinden:
  while (status != WL_CONNECTED) {
    Serial.print("Verbinden mit WPA SSID: ");
    Serial.println(ssid);

    status = WiFi.begin(ssid, pass);
    delay(10000);
  }

  //*** Verbindung erfolgreich: Infos drucken
  Serial.print("Verbindung erfolgreich: ");
  printCurrentNet();
  printWifiData();
}

void loop()
{
  String WertDEZ;

  //*** So lange scannen, bis RFID-Chip erkannt wurde
  if (!mfrc522.PICC_IsNewCardPresent())
  {
    return;
  }
  if (!mfrc522.PICC_ReadCardSerial())
  {
    return;
  }
 

  //*** RFID in Dezimalwert umwandeln
  for (byte i = 0; i < mfrc522.uid.size; i++)
  {
    // String zusammenbauen
    WertDEZ = WertDEZ + String(mfrc522.uid.uidByte[i], DEC) + " ";
  }

  //*** RFID anzeigen
  Serial.println("Dezimalwert: " + WertDEZ);


  //*** POST .../update an API
  if (client.connect(server_ip, C_PORT)){
    Serial.println("Verbindung mit API hergestellt");
    client.println("POST /zeiterfassung/sessions/update?rfid=" + WertDEZ + " HTTP/1.1");
  } else {
    Serial.println("Verbindung zur API fehlgeschlagen");
  }

  delay(1000)
}

