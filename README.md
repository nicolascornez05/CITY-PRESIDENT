# CITY PRESIDENT

Jeu Android hors ligne de simulation politique, économique et urbaine en 3D.

## Moteur et plateforme
- Moteur : Godot 4.x
- Langage : GDScript
- Édition et export : Godot Android Editor
- Rendu : GL Compatibility
- Mode : hors ligne
- Économie : réaliste et interdépendante
- Difficulté : réaliste
- Langue initiale : français
- Cible : Android

## Développement directement sur Android
Le projet est désormais conçu pour être ouvert, modifié, lancé et exporté depuis l'éditeur Godot Android, sans licence Unity, sans ordinateur et sans abonnement.

Guide : [Développer et exporter depuis Android](Documentation/GODOT_ANDROID_SETUP.md).

## Prototype actuel
- Carte 3D provisoire de Novaria.
- Cinq villes représentées.
- Bâtiments générés par code.
- Déplacement tactile et zoom par pincement.
- Tableau de bord : budget national, dette, PIB, chômage et inflation.
- Simulation accélérée et pause.
- Première économie locale hors ligne.

## Structure
```
project.godot
scenes/
  main.tscn
scripts/
  main.gd
  economy.gd
Documentation/
.github/
```

## Important
Le projet Godot a été créé dans le dépôt, mais il n'a pas encore été ouvert ni exécuté dans l'éditeur Godot. La première importation doit confirmer la compatibilité des scripts et de la scène. Aucune APK n'a encore été produite.

## Documentation
- [Configuration Godot Android](Documentation/GODOT_ANDROID_SETUP.md)
- [Conception du jeu](Documentation/GAME_DESIGN.md)
- [Règles économiques](Documentation/ECONOMY_RULES.md)
- [Feuille de route](Documentation/ROADMAP.md)
