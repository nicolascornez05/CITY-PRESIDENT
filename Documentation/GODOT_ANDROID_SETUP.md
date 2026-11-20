# Développer CITY-PRESIDENT depuis Android

## Moteur
- Godot 4.x Android Editor
- GDScript uniquement (pas de C# / Mono)
- Rendu : GL Compatibility
- Jeu hors ligne, sans serveur ni abonnement

## Installer l'éditeur
1. Installer Godot Engine depuis Google Play ou depuis https://godotengine.org/download/android/
2. Autoriser l'accès aux fichiers lorsque Android le demande.
3. Télécharger le dépôt GitHub sous forme ZIP et l'extraire dans Documents ou Downloads.
4. Dans Godot Android, choisir Import et sélectionner le dossier qui contient `project.godot`.
5. Ouvrir `scenes/main.tscn` et lancer le projet avec Play.

## Exporter une APK depuis le téléphone
1. Ouvrir Project > Install Android Build Template et installer les modèles correspondant exactement à la version de Godot utilisée.
2. Ouvrir Project > Export.
3. Ajouter un preset Android.
4. Pour un APK de test local, utiliser le modèle APK standard et la signature de débogage.
5. Exporter vers Downloads/CITY-PRESIDENT.apk.
6. Autoriser l'installation depuis cette source si Android le demande.

Pour l'export Android standard depuis l'éditeur Android, il n'est pas nécessaire d'installer manuellement OpenJDK ou Android SDK. Les builds Gradle personnalisés ont des prérequis différents.

## Limites connues
- L'éditeur Android est en accès anticipé et son interface n'est pas optimisée pour les petits écrans.
- Il ne prend pas en charge C#/Mono : le projet utilise GDScript.
- Les modèles d'export doivent correspondre exactement à la version de l'éditeur.
- La première importation et l'APK doivent être testées sur le téléphone cible.

## Première version
La scène principale génère une carte 3D provisoire de Novaria, cinq villes, des bâtiments, une caméra tactile, un tableau de bord économique et une horloge simplifiée.
