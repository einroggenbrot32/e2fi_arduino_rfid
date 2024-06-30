import requests
import urllib3
import json
import os
import re

STARTUP = \
"=== NARVIKSOFT ZEITERFASSUNG 1.0 ===========================\n\n"\

HELP = \
"commands: \n"\
"  getactive: prints all active sessions\n" \
"  getall: prints all sessions\n"\
"  help: print help\n"

VERIFY = False
API_BASE_URL = "https://localhost:7024/zeiterfassung/"

clear = lambda: os.system('cls')

def regex_escape_fixed_string(string):
    "escape fixed string for regex"
    if type(string) == bytes:
        return re.sub(rb"[][(){}?*+.^$]", lambda m: b"\\" + m.group(), string)
    return re.sub(r"[][(){}?*+.^$]", lambda m: "\\" + m.group(), string)

assert (
    regex_escape_fixed_string("a[b]c(d)e{f}g?h*i+j.k^l$m") ==
    'a\\[b\\]c\\(d\\)e\\{f\\}g\\?h\\*i\\+j\\.k\\^l\\$m'
)

class Struct:
    def __init__(self, **entries):
        self.__dict__.update(entries)

def is_timestamp(s):
    regexp = regex_escape_fixed_string('\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{5}')
    if re.match(s, regexp):
        return True
    else:
        return False


def format_time(timestamp):
    result = timestamp
    if is_timestamp(timestamp):
        result = timestamp[8:10] + "-" + timestamp[5:7] + "-" + timestamp[0:4] + " "                                    # Datum
        result += timestamp[11:19]                                                                                      # Zeit
    return result

def _get_active():
    api_url = API_BASE_URL + 'sessions/getallactive'
    response = requests.get(api_url, verify=VERIFY)
    result = {}
    if response.status_code == 200:
        result = response.json()
    return result

def _get_all_sessions():
    api_url = API_BASE_URL + 'sessions/getall'
    response = requests.get(api_url, verify=VERIFY)
    result = {}
    if response.status_code == 200:
        result = response.json()
    return result

def _get_username(id):
    api_url = API_BASE_URL + "users/getUserNameById?id=" + str(id)
    response = requests.get(api_url, verify=VERIFY)
    return response.text

def display_session(session):
    s = Struct(**session)

    text_to_print = "User Id: " + str(s.userId) + "\t" \
                    + "Name: " + _get_username(s.userId) + "\t" \
                    + "Active Since: " + format_time(str(s.startzeit)) + "\t" \
                    + "Endezeit: " + format_time(str(s.endzeit))

    print(text_to_print)


# weitere denkbare abfragen
#  - Statistiken zu einzelnen Usern
#  - neue User registrieren


####### PROGRAMM ##############

# während den Tests die blöden SSL-Warnungen deaktivieren
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

print(STARTUP)
print(HELP)


# APP LOOP
run = True
while(run):
    cmd = input()
    if (cmd == "getactive"):
        sessions = _get_active()
        for s in sessions:
            display_session(s)
    if (cmd == "getall"):
        sessions = _get_all_sessions()
        for s in sessions:
            display_session(s)
    if (cmd == "help"):
        print(HELP)
    if (cmd == "cls"):
        clear()
        print(STARTUP)

