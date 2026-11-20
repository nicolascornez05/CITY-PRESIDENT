# Architecture technique — CITY PRESIDENT (Godot)

## 1. Principes
- Jeu hors ligne, sans compte ni serveur obligatoire.
- Moteur Godot 4.x, GDScript et éditeur Android.
- Séparation entre simulation, données, rendu 3D et interface.
- Calculs économiques testables et sauvegardes versionnées.
- Simulation agrégée pour préserver les performances sur Android.
- Rendu GL Compatibility pour une compatibilité mobile plus large.

## 2. Modules logiques

| Module | Responsabilité |
|---|---|
| Core | Cycle de jeu, horloge, configuration et événements |
| World | Pays, régions, frontières et carte |
| Cities | Villes, quartiers, parcelles et budgets locaux |
| Economy | PIB, secteurs, prix, inflation, impôts, dette et commerce |
| Government | Président, ministères, lois et politiques |
| Population | Démographie, emploi, revenus, besoins et satisfaction |
| Construction | Placement, coûts, bâtiments, routes et entretien |
| Events | Crises, catastrophes, grèves et événements |
| SaveSystem | Sauvegardes versionnées, migrations et récupération |
| UI | Navigation, tableaux de bord, cartes et commandes tactiles |

## 3. Modèle de simulation
Le temps de jeu est piloté par une horloge centrale. Les systèmes économiques sont mis à jour à des échéances explicites : jour, mois ou année. Les calculs coûteux ne doivent pas être exécutés à chaque image.

Les données économiques restent indépendantes des nœuds visuels. La carte 3D affiche ces données mais ne doit jamais devenir leur source de vérité.

## 4. Données locales
- Ressources Godot : définitions statiques des bâtiments, politiques et événements.
- JSON ou Resource : données de scénario et modèles de simulation.
- SQLite pourra être ajouté lorsqu'un volume de données justifiera une base locale.
- Version de schéma obligatoire pour les sauvegardes et migrations.

Les sauvegardes doivent conserver une copie de secours avant toute migration.

## 5. Monde 3D
- Godot 4.x avec rendu GL Compatibility.
- Caméra aérienne orthographique configurable.
- Contrôles tactiles : déplacement et zoom par pincement.
- Génération et chargement progressif des quartiers.
- Instanciation des bâtiments à partir des données de parcelles.
- Modes de qualité adaptés aux performances Android.

## 6. Économie
Les flux financiers doivent être traçables : chaque recette et dépense crée une écriture avec montant, catégorie, origine et date de simulation. Les budgets national et locaux sont distincts. Les transferts entre administrations sont comptabilisés des deux côtés sans créer artificiellement de richesse.

Les indicateurs (PIB, inflation, chômage, dette, déficit, pouvoir d'achat) doivent découler de règles documentées et testées.

## 7. Sauvegarde et temps hors ligne
La progression du temps est contrôlée par le joueur. La pause arrête la simulation. Les vitesses accélérées utilisent des pas de simulation fixes. À la fermeture, une sauvegarde est effectuée ; à la reprise, le jeu restaure l'état sans calculer un temps réel écoulé, sauf option dédiée.

## 8. Tests
- Tests unitaires pour formules économiques et budgets.
- Tests de cohérence des transferts financiers.
- Tests de sauvegarde, chargement et migration.
- Tests de performance sur appareils Android.
- Tests de construction et de navigation tactile.
