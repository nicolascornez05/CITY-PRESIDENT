# CITY PRESIDENT

Jeu Android de simulation politique, économique et urbaine en 3D.

## Vision
Le joueur dirige un pays composé de plusieurs régions et villes. Il gère le budget national, les ministères, les infrastructures, la population, les services publics et les crises économiques.

## Décisions de conception
- Plateforme : Android
- Moteur : Unity 6 LTS / C#
- Rendu : 3D avec URP
- Mode : hors ligne
- Économie : réaliste et interdépendante
- Difficulté : réaliste
- Langue initiale : français
- Compilation : GitHub Actions à la demande
- Distribution de test : APK via GitHub Actions Artifacts

## État du dépôt
Ce dépôt contient le socle documentaire et la configuration initiale de compilation. Le projet Unity jouable, ses scènes, ses ressources 3D et ses scripts de jeu restent à créer. Aucune compilation APK ne peut aboutir tant que le projet Unity complet n'a pas été ajouté.

## Structure prévue
```
Assets/
  Scenes/
  Scripts/
    Core/
    Economy/
    Government/
    World/
    Cities/
    Population/
    Construction/
    Events/
    SaveSystem/
    UI/
  Prefabs/
  Materials/
  Models/
  Textures/
  Audio/
Packages/
ProjectSettings/
Documentation/
.github/workflows/
```

## Compilation Android
Le workflow `.github/workflows/android-build.yml` est déclenché manuellement depuis l'onglet **Actions**. Il utilise GameCI et une licence Unity Personal fournie sous forme du secret GitHub `UNITY_LICENSE`.

Ne jamais committer de licence Unity, mot de passe, clé de signature Android ou autre secret.

## Documentation
- [Architecture technique](Documentation/ARCHITECTURE.md)
- [Feuille de route](Documentation/ROADMAP.md)

## Licence
La licence du code et des ressources sera définie avant la première distribution publique. Les ressources tierces conserveront leurs licences respectives.
