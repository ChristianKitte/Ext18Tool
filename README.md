![cropped-BewerbungHeader1](https://github.com/user-attachments/assets/de3a724b-4002-4e2b-80a4-e9d588bf1930)

## Ext18Tool

Bei dem **Ext18Tool*** handelt es sich um ein sehr kleines, vollständig konfigurierbares Tool, welches dazu dient, ein Programm (**Generator**) 
zu starten und nach dessen Beendigung eine Datei (**Ausgabename**) aus deren Verzeichnis (**Arbeitsordner**) in das aktuelle Verzeichnis von 
**Ext18Tool** zu kopieren. 

Ursprünglich wurde das Programm eingesetzt, um den Zugriff auf durch andere Programme generierte Dateien zu vereinfachen. Das adressierte 
Problem ist hierbei, dass man die Programme selbst nicht kopieren will, eine einfaches Verknüpfen aber nicht dazu führt, die Ausgabe auf 
den Ort der Verknüpfung zu lenken. Der Einsatz des Tools vereinfacht den Workflow in einigen Fällen ein wenig.

Ein interessanter Fakt ist, dass große Teile des Quellcodes tatsächlich von einem LLM (Claude 3.7 Sonet) generiert worden sind. Der initial 
verwendete Prompt war:

```
Erstelle mir eine Konsolenanwendung mit der höchsten in Windows nativ verfügbaren .net Version und c#. Es soll
eine Konfigurationsdatei vorhanden sein, in der ich den Namen einer Anwendung (.exe) als "Generator", eines
Ordner als "Arbeitsordner" sowie eines Dateiname (.txt) als "Ausgabename" hinterlegen kann. Diese Daten werden
von der Konsolenanwendung verwendet.

Funktion der Konsolenanwendung: Nach dem Start, soll die Konsolenanwendung die in der Konfigurationsdatei genannte
Anwendung starten. Diese liegt in dem in der Konfiguration genannten Ordner. Diese Anwendung erzeugt eine Textdatei
mit den in der Konfiguration genannten Namen und beendet sich dann. Die Kosolenanwendung wartet, bis dies geschehen
ist und verschiebt! die erzeugte Datei in das Verzeichnis, in der die Kosnolenanwendung ausgeführt wird. Anschließend
beendet sich die Konsolenanwendung ebenfalls.

Analysiere die Problemstellung und erstelle in einen zweiten Schritt die Anwendung. Stelle bei Unklarhiten Rückfragen.
```

In einem zweiten Schritt wurde das LLM angewiesen, den Code um eine Fehlerabfangung zu erweitern:

```
Ergänze den Programmcode. Es muss sichergestellt werden, das in dem Verzeichnis nicht bereits eine Datei vorhanden
ist. Gebe in diesen Fall die folgende Meldung aus: "Es befindet sich bereits eine Datei in dem Verzeichnis !".
```

Der so ausgegebene Code wurde manuell in einer neu erstellten Konsolenanwendung (.net 9) verwendet und konnte problemlos 
kompiliert werden. Im Nachgang wurden lediglich einige Refaktorings durchgefügt und Ergänzungen eingefügt. Es bleibt
anzumerken, dass der überwiegende Teil des Codes funktional und fehlerfrei war und darüber hinaus übersichtlich und von
der Benamung her sinnvoll. Zudem wurde er sinnvoll dokumentiert.
