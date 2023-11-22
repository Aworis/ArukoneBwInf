# Arukone - Aufgabe 1 des 42. Bundeswettbewerbs für Informatik

## Lösungsidee
Um garantiert ein lösbares Arukone-Rätsel zu erhalten, war die Lösungsidee, das Arukone-Rätsel zunächst leer auf einem zweidimensionalen Koordinatensystem mit entsprechender Größe abzubilden.

Damit anschließend die einzelnen Zahlenpaare ohne Überschneidungen der Linienzüge verbunden werden können, müssen die unterschiedlichen Zahlenpaare nacheinander in Form von Zahlenketten in das Arukone-Rätsel gesetzt werden.

Hierfür wählt man zunächst ein leeres Feld im Arukone-Rätsel aus und platziert das erste Kettenglied. Danach wird von diesem Feld in horizontaler oder vertikaler Richtung ein weiteres freies Feld für das nächste Kettenglied ausgewählt und ebenfalls belegt. Der Vorgang wird so lange fortgesetzt, bis eine Zahlenkette von ausreichender Länge gesetzt wurde.

Wird eine Zahlenkette mit einer bestimmten Länge gesetzt, belegt sie somit die Felder zwischen erstem und letztem Kettenglied bzw. dem Zahlenpaar. Diese Felder können nicht erneut von einer anderen Zahlenkette belegt werden und bleiben für einen möglichen Linienzug frei.

Im nächsten Schritt wird eine weitere Zahlenkette auf den übrigen Feldern platziert. Dieser Vorgang wird so lange wiederholt, bis die Mindestanzahl an Zahlenketten erreicht ist. Dadurch entsteht ein fertig gelöstes Arukone-Rätsel.

Anschließend entfernt man pro Zahlenkette alle Kettenglieder zwischen dem ersten und dem letzten Kettenglied und erhält dadurch das ungelöste Arukone-Rätsel.

## Umsetzung
Das Arukone-Programm wird in C# nach dem Paradigma der objektorientierten Programmierung entworfen und als Konsolenanwendung implementiert. Dementsprechend ist der Code in Klassen und Methoden organisiert.

## Nutzereingabe
Da die Gittergröße eine Mindestgröße von ≥ 4 Feldern haben soll und der Arukone-Checker auf www.arukone.bwinf.de/arukone Gittergrößen bis eine Größe von 30 Feldern akzeptiert, wird der Nutzer über die Konsole aufgefordert, eine beliebige Zahl zwischen 4 und 30 einzugeben. In der AskForBoardsize()-Methode erfolgt hierfür die Validierung und die Eingabe wird bei Erfolg in der InputBoardsize-Property der UserInput-Klasse gespeichert.

## Initialisierung von Objekten
Als nächstes wird in der Main()-Methode ein ArukoneController initialisiert, der wiederum über den Konstruktor eine Reihe von weiteren Objekten initialisiert:

- randomGenerator: Ein Objekt der Klasse Random, das zum Generieren von zufälligen X- und Y-Koordinaten, vertikalen und horizontalen Richtungen und Längen von Zahlenketten genutzt wird.
- arukoneBoard: Über dieses Objekt wird im Konstruktor das Arukone-Rätsel in Form von zwei zweidimensionalen int-Arrays initialisiert. Ein ungelöstes Arukone-Rätsel mit den Zahlenpaaren und ein gelöstes Arukone-Rätsel mit den Zahlenketten und den damit hervorgehenden möglichen Linienzügen.
- arukoneCalculator: Mit diesem Objekt wird sichergestellt, dass die Anzahl der Zahlenketten berechnet wird. Sie beträgt n/2, wenn n = Gittergröße. Ist n/2 keine Ganzzahl, wird sie aufgerundet, damit die geforderte Mindestzahl der Zahlenpaare gesetzt wird.

## Erzeugen eines Arukone-Rätsels
Im nächsten Schritt wird über die Methode CreateSolvableGame() ein lösbares Arukone-Rätsels erstellt, das in zwei Arrays abgebildet wird: SolvedGame und UnsolvedGame. In SolvedGame werden alle Kettenglieder gespeichert und in UnsolvedGame lediglich das erste und letzte Glied einer Kette. Dadurch hat man im Anschluss ein ungelöstes Rätsel und die passend generierte Lösung.

Hierfür wird über eine for-Schleife pro Zahlenkette je einmal PlaceChainLinks() aufgerufen und in einem zufälligen freien Feld das erste Kettenglied in die zweidimensionalen Arrays geschrieben. Danach setzt die Methode weitere Kettenglieder in zufällig angrenzende freie Felder des SolvedGame Arrays, bis alle Kettenglieder gesetzt oder keine angrenzenden freien Felder verfügbar sind. Damit die Methode nicht endlos weiterläuft, bricht sie bei Bedarf nach 1500 Versuchen ab und gibt die Länge der Zahlenkette zurück. Die letzten Einträge einer Zahlenkette werden darüber hinaus auch in das UnsolvedGame Array geschrieben.

Sind alle Kettenglieder nach einer Iteration gesetzt, prüft CreateSolvableGame(), ob die Kette eine Mindestlänge von 3 Feldern aufweisen. Ist dies nicht der Fall, werden die Felder der Arrays UnsolvedGame und SolvedGame und der Zähler i der for-Schleife zurückgesetzt, damit die erste Iteration erneut durchläuft. Dies kann so oft erfolgen, bis die maximale Anzahl an Versuchen erreicht ist. Auf diese Weise ist die Wahrscheinlichkeit hoch, dass ein fertiges Rätsel erzeugt wird.

## Erstellung der Textdateien
Sobald ein Arukone-Rätsel erstellt wurde, sorgt die UserOutput-Klasse dafür, dass die UnsolvedGame und SolvedGame Arrays passend formatiert in Textdateien geschrieben und diese im Ordner ‚ArukoneOutput‘ gespeichert werden. Die Formatierung entspricht dabei dem Eingabeformat des Arukone-Checkers.

## Beispiele
Es folgen Beispiele, die vom Programm als Textausgabe mit und ohne Lösung generiert wurden. Zur Verdeutlichung wurden in der generierten Lösung die Zahlenketten farblich hervorgehoben. Die Lösung des Arukone-Checkers dient als Vergleich.

|Arukone 4x4     |                         |                         |
|----------------|-------------------------|-------------------------|
|![image](https://github.com/Aworis/ArukoneBwInf/blob/main/img/textausgabe4x4.jpg)|![image](https://github.com/Aworis/ArukoneBwInf/blob/main/img/generiert4x4t.jpg)|![image](https://github.com/Aworis/ArukoneBwInf/blob/main/img/checker4x4.jpg)|
|Textausgabe 4x4 |Generierte Lösung 4x4    |Arukone-Checker 4x4      |

|Arukone 15x15     |                         |                         |
|------------------|-------------------------|-------------------------|
|![image](https://github.com/Aworis/ArukoneBwInf/blob/main/img/textausgabe14x14.jpg)|![image](https://github.com/Aworis/ArukoneBwInf/blob/main/img/generiert14x14.jpg)|![image](https://github.com/Aworis/ArukoneBwInf/blob/main/img/checker.jpg)|
|Textausgabe 15x15 |Generierte Lösung 15x15  |Arukone-Checker 15x15    |

|Arukone-Checker ungelöst     |                         |
|-----------------------------|-------------------------|
|![image](https://github.com/Aworis/ArukoneBwInf/blob/main/img/generiert8x8.jpg)|![image](https://github.com/Aworis/ArukoneBwInf/blob/main/img/ungeloest8x8.jpg)|
|Generierte Lösung 8x8|Arukone-Checker ungelöst 8x8|
