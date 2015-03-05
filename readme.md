# Ping Pong
## Specifikation
Vi ska skapa ett klassiskt Ping Pong spel med två "paddles" (racken, ungefär som tennisrack fast i form av klossar). En "paddle" ska vara till vänster och styras med piltangenterna (upp och ned) och den andra ska vara till höger och styras av datorn, enligt någon simpel AI-styrning (se förslag nedan). Dessa "paddles" kan endast röra sig upp och ned på skärmen. Bollen utgår ifrån mitten och rör sig mot ena sidan. Spelarens uppgift är att blockera bollen med sin "paddle" så att den inte kommer utanför skärmen. Om den trots allt kommer utanför skärmen så förlorar spelaren på den sidan som bollen åkte ut vid, och ett poäng ges till motståndaren. Bollen återställs därefter till sin ursprungsposition i mitten av skärmen.

Därtill ska även en startmeny samt en highscore-lista finnas. Startmenyn ska innehålla knapparna *"Start"*, *"Highscore"*, och *"Quit"*.

## AI förslag
Datorn styr sin paddle upp eller ner för att försöka matcha bollens y-position, först när bollen rör sig mot högra sidan. Om datorn blir för bra, kan en fördröjning på 100-500ms läggas in, så att datorn inte börjar styra sin paddle förrän en viss tid efter bollen har börjat röra sig mot dennes sida.

## Vad behöver göras?
Kolla [GitHub Issues](https://github.com/TEINF12A/PingPong/issues) för projekthantering.

## Klassdiagram
Diagram över *"GameObjects"* arv.
![Klassdiagram](https://raw.githubusercontent.com/TEINF12A/PingPong/master/Game%20object%20class%20diagram.png)
