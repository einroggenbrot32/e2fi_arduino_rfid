#include "MFRC522.h"
#include "WiFiS3.h"
#include <ArduinoHttpClient.h>


// Anschlüsse definieren
# define SDA 10
# define RST 9

// Wifi-Server/ CLient
#define C_SSID "HotspotWebApi"
#define C_PASS "abcdefgh"

// API-Konstanten
#define C_API_BASE_URL "https://192.168.137.1:7024/zeiterfassung/"
#define C_PORT 7042

// RFID-Empfänger benennen, Pins zuordnen
MFRC522 mfrc522(SDA, RST);

char ssid[] = C_SSID;        // your network SSID (name)
char pass[] = C_PASS;        // your network password (use for WPA, or use as key for WEP)

// WiFI-Server und Client
int status = WL_IDLE_STATUS;

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

  // Initialisierung des RFID-Empfängers
  mfrc522.PCD_Init();
  Serial.println("NARVIKSOFT ZEITERFASSUNG");

  // WIFI Setup
   // check for the WiFi module:
  if (WiFi.status() == WL_NO_MODULE) {
    Serial.println("Communication with WiFi module failed!");
    // don't continue
    while (true);
  }

  String fv = WiFi.firmwareVersion();
  if (fv < WIFI_FIRMWARE_LATEST_VERSION) {
    Serial.println("Please upgrade the firmware");
  }

  // attempt to connect to WiFi network:
  while (status != WL_CONNECTED) {
    Serial.print("Attempting to connect to WPA SSID: ");
    Serial.println(ssid);
    // Connect to WPA/WPA2 network:
    status = WiFi.begin(ssid, pass);

    // wait 10 seconds for connection:
    delay(10000);
  }

  // you're connected now, so print out the data:
  Serial.print("You're connected to the network");
  printCurrentNet();
  printWifiData();

  // Versuch, einen API Call abzusetzen

    

}

void loop()
{
 while(1==1)
 {

 } 
 //

}


/*
  // Post-request an die API

   Serial.println("making POST request");
  String contentType = "application/x-www-form-urlencoded";
  String postData = "rfid=12345678";

  client.post("https://192.168.137.1:7024/zeiterfassung/sessions/update?rfid=1");

  // read the status code and body of the response
  int statusCode = client.responseStatusCode();
  String response = client.responseBody();

  Serial.print("Status code: ");
  Serial.println(statusCode);
  Serial.print("Response: ");
  Serial.println(response);
*/

  // kurze Pause, damit nur ein Wert gelesen wird
  delay(1000);
}

