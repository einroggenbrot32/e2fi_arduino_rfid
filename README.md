# Arduino E2FI Projekt Zusammenfassung | by Narvik & Lukas

# Projektbeschreibung
Webbasiertes Login-/Zeiterfassungssystem mit mobilem RFID-Sensor. 

# Aufbau
Das Projekt besteht aus einer in der Cloud gehosteten RESTApi, deren Endpunkte von einem Mikrocontroller mit RFID-Sensorik angesprochen werden. Die Daten werden in einer bei Amazon gehosteten PostgreSQL-Datenbank gespeichert.
Ein CLI-Tool gibt (ebenfalls über Calls auf die API) diverse Auskünfte über die erfassten Daten. Außerdem lassen sich mittels des CLI-Tools User neue registrieren.

![image](https://github.com/einroggenbrot32/e2fi_arduino_rfid/assets/112704792/ecea34ad-8993-43f2-b521-68f93e018a57)

# Technologien
Die RESTApi ist mit C# in der Dotnet-Umgebung programmiert und wird auf einem Amazon(?)-Server gehostet.
Das CLI-Tool ist in Python realisiert.

Für den Mikrocontroller verwenden wir einen RFID-Sensor von ?????. Der Chip ist vom selben Hersteller.

# Prozessbeschreibung
Bevor Zeiten erfasst werden können, muss mindestens ein User existieren, dem eine RFID zugewiesen wurde.

## User erstellen und RFID zuweisen
Im CLI-Tool kann über den Befehl "????" ein neues User angelegt werden. Dieser erhält eine Personalnummer und einen Namen. Letzterer wird vom Benutzer eingegeben.
Anschließend kann über den Befehl "???? + Personalnummer" eine RFID zugewiesen werden. Der Benutzer wird aufgefordert, den RFID-Chip, den er zuweisen möchte, zu scannen (an den Sensor am Arduino zu halten).
War die Zuweisung erfolgreich, bekommt der Benutzer eine Bestätigung über das CLI-Tool. Außerdem ertönt ein Signal am Arduino und es leuchtet eine gründe LED auf.

## Zeit erfassen
Die Zeiterfassung erfolgt durch einfaches An- und Abstempeln durch scannen des jeweiligen RFID-Chips am Arduino/ Sensor.
Bereits eingeloggte User werden bei erneutem Stempeln ausgeloggt.

## Abfrage von Informationen (Zeiten, Userinfos, etc.)
Über das CLI-Tool können folgende Daten abgefragt werden. Siehe hierzu die Liste an Befehlen weiter unten.
Befehl              Ausgabe


EPK!!!! LUKAS


# API-Endpunkt-URL's
- /zeiterfassung/sessions/getallactive --> alle noch eingeloggten, aktiven nutzer werden angezeigt
- /zeiterfassung/sessions/update --> updatet alle einträge, entfernt ausgeloggte RFID's
- /zeiterfassung/sessions/getall --> listet alle im System vorhanden ID's auf

# Mikrocontroller/ Sensor
HIER EIN BILD DES ARDUINO

# CLI Tool
Liste an Befehlen:
Befehl            Ausgabe
