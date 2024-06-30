# Arduino E2FI Projekt Zusammenfassung | by Narvik & Lukas

### KOMMENTAR 30.06.2024
Aktuell wird die API noch nicht in der Cloud gehostet, sondern nur lokal auf der Maschine. Die Datenbank ebenso.
Der Arduino wird mit einen WLAN-Hotspot der Maschine verbunden und macht die API-Calls über deren IP.  

# Projektbeschreibung
Webbasiertes Login-/Zeiterfassungssystem mit mobilem RFID-Sensor. 

# Aufbau
Das Projekt besteht aus einer in der Cloud gehosteten RESTApi, deren Endpunkte von einem Mikrocontroller mit RFID-Sensorik angesprochen werden. Die Daten werden in einer bei Amazon gehosteten PostgreSQL-Datenbank gespeichert.
Ein CLI-Tool gibt (ebenfalls über Calls auf die API) diverse Auskünfte über die erfassten Daten. Außerdem lassen sich mittels des CLI-Tools User neue registrieren.

![image](https://github.com/einroggenbrot32/e2fi_arduino_rfid/assets/112704792/1c03e24e-91a7-4acb-a7ec-57c889a01bfc)


# Technologien
Die RESTApi ist mit C# in der Dotnet-Umgebung programmiert und wird auf einem Amazon-Server gehostet.
Das CLI-Tool ist in Python realisiert.

Für den Mikrocontroller verwenden wir einen RFID-Sensor von Amazon. Der Chip ist aus dem selben Paket.

# Prozessbeschreibung
Bevor Zeiten erfasst werden können, muss mindestens ein User existieren, dem eine RFID zugewiesen wurde.
Danach kann ich per Befehl die Daten ausgeben oder auch über erneuten Kontakt mit dem Sensor die Session beenden.

## User erstellen und RFID zuweisen
### GEPLANT:
Im CLI-Tool kann über den Befehl "????" ein neues User angelegt werden. Dieser erhält eine Personalnummer und einen Namen. Letzterer wird vom Benutzer eingegeben.
Anschließend kann über den Befehl "???? + Personalnummer" eine RFID zugewiesen werden. Der Benutzer wird aufgefordert, den RFID-Chip, den er zuweisen möchte, zu scannen (an den Sensor am Arduino zu halten).
War die Zuweisung erfolgreich, bekommt der Benutzer eine Bestätigung über das CLI-Tool.

### STAND JETZT:
User müssen noch manuell auf der Datenbank angelegt werden. Siehe dazu die Befehle Unter ZeiterfassungAPI/SQL.

## Zeit erfassen
Die Zeiterfassung erfolgt durch einfaches scannen(in folgenden Stempeln genannt) des jeweiligen RFID-Chips am Arduino/ Sensor.
Bereits eingeloggte User werden bei erneutem Stempeln ausgeloggt.

## Abfrage von Informationen (Zeiten, Userinfos, etc.)
Über das CLI-Tool können folgende Daten abgefragt werden. Siehe hierzu die Liste an Befehlen weiter unten.
Befehl              Ausgabe
getall              gibt alle Sessions aus, die je erfasst wurden
getactive           gibt alle aktiven Sessions aus (das heißt solche, die eine Start-, aber keine Endzeit haben)

#### Hier sind natürlich viele sinnvolle Erweiterungen denkbar ... vorerst soll das CLI-Tool allerdings lediglich das Konzept demonstrieren.

## API-Endpunkt-URL's
- /zeiterfassung/sessions/getallactive --> alle noch eingeloggten, aktiven Nutzer/Sessions werden angezeigt
- /zeiterfassung/sessions/update --> updatet alle einträge, entfernt ausgeloggte RFID's -> Updatet die Session wenn ein Chip erneut an den Sensor gehalten wurde
- /zeiterfassung/sessions/getall --> listet alle im System vorhanden Session's auf
- /zeiterfassung/users/getUserNameById --> listet alle vorhanden User anhand deren ID auf
- /zeiterfassung/users/getall --> gibt alle User aus - egal ob ein oder ausgeloggt

# Mikrocontroller/ Sensor
![image](https://github.com/einroggenbrot32/e2fi_arduino_rfid/assets/112704792/b5811dc8-a777-4e70-8b17-e7ace0457049)
