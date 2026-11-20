# Architecture technique — CITY PRESIDENT

## 1. Principes
- Simulation hors ligne, sans compte ni serveur obligatoire.
- Séparation stricte entre simulation, données, rendu 3D et interface.
- Calculs déterministes autant que possible pour faciliter les sauvegardes et les tests.
- Simulation agrégée par groupes statistiques plutôt que simulation individuelle de milliers d'habitants.
- Chargement progressif des villes et quartiers pour limiter la mémoire sur Android.

## 2. Modules Unity

| Module | Responsabilité |
|---|---|
| Core | Cycle de jeu, horloge, configuration et événements internes |
| World | Pays, régions, frontières et données géographiques |
| Cities | Villes, quartiers, parcelles et budgets locaux |
| Economy | PIB, secteurs, prix, inflation, impôts, dette et commerce |
| Government | Président, ministères, lois et politiques |
| Population | Démographie, emploi, revenus, besoins et satisfaction |
| Construction | Placement, coûts, bâtiments, routes et entretien |
| Events | Crises, catastrophes, grèves et événements |
| SaveSystem | Sauvegardes versionnées, migrations et récupération |
| UI | Navigation, tableaux de bord, cartes et commandes tactiles |

## 3. Modèle de simulation
Le temps de jeu est piloté par une horloge centrale. Les systèmes s'abonnent à des échéances explicites : heure, jour, mois ou année. Les calculs coûteux ne doivent pas être exécutés à chaque image.

Les données économiques sont stockées dans des modèles C# indépendants des GameObjects. Les vues 3D affichent ces données mais ne sont jamais leur source de vérité.

## 4. Données locales
- ScriptableObjects : définitions statiques (types de bâtiments, politiques, événements).
- SQLite : parties, villes, population agrégée, économie et historique.
- JSON : paramètres, scénarios et données d'import/export.
- Version de schéma obligatoire pour permettre les migrations.

Les sauvegardes doivent être transactionnelles et conserver une copie de secours avant migration.

## 5. Monde 3D
- Unity 6 LTS avec URP.
- Caméra aérienne orthographique/perspective configurable.
- Contrôles tactiles : déplacement, zoom et rotation.
- LOD, occlusion culling, batching et chargement par secteurs.
- Les bâtiments sont instanciés à partir de données de parcelles.
- Prévoir un mode qualité basse, moyenne et élevée.

## 6. Économie
Les flux financiers doivent être traçables : chaque recette et dépense crée une écriture avec montant, catégorie, origine et date de simulation. Les budgets national et locaux sont distincts. Les transferts entre administrations doivent être comptabilisés des deux côtés sans créer artificiellement de richesse.

Les indicateurs (PIB, inflation, chômage, dette, déficit, pouvoir d'achat) doivent découler de règles documentées et testées, et non de valeurs arbitraires.

## 7. Sauvegarde et temps hors ligne
La progression du temps est contrôlée par le joueur. La pause doit arrêter la simulation. Les vitesses accélérées doivent utiliser des pas de simulation fixes. À la fermeture, une sauvegarde est effectuée ; à la reprise, le jeu restaure l'état sans calculer un temps réel écoulé, sauf si une option dédiée est ajoutée ultérieurement.

## 8. Tests
- Tests unitaires pour formules économiques et budgets.
- Tests de cohérence des transferts financiers.
- Tests de sauvegarde, chargement et migration.
- Tests de performance sur appareils Android modestes.
- Tests de construction et de navigation tactile.
