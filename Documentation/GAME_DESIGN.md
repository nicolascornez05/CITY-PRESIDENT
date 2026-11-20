# CITY PRESIDENT — Conception du jeu

## Vision
CITY PRESIDENT est un jeu solo de simulation politique, économique et territoriale. Le joueur dirige un pays entier composé de régions et de villes. Le défi consiste à développer le pays sans déséquilibrer ses finances, ses services publics ou la qualité de vie.

Le jeu est entièrement jouable hors ligne. Les données de simulation sont locales et la progression dépend du temps de jeu choisi par le joueur.

## Principes de conception
1. Les décisions doivent avoir des conséquences mesurables.
2. Les finances publiques doivent être traçables et cohérentes.
3. Les villes doivent avoir des identités et des besoins différents.
4. Le joueur doit comprendre pourquoi un indicateur monte ou baisse.
5. La simulation doit rester lisible et fluide sur Android.
6. Les données de jeu ne doivent jamais dépendre des objets visuels 3D.

## Boucle de jeu principale
1. Consulter le tableau de bord national.
2. Identifier les problèmes prioritaires dans les régions et les villes.
3. Examiner les recettes, les dépenses et les indicateurs.
4. Décider d'une politique, d'un investissement ou d'une construction.
5. Laisser passer le temps de simulation.
6. Observer les conséquences économiques et sociales.
7. Ajuster le budget et les priorités.

## Structure territoriale
- Pays : gouvernement, budget national, lois et statistiques nationales.
- Région : coordination territoriale et répartition des investissements.
- Ville : population, budget local, services et urbanisme.
- Quartier : densité, zonage et besoins locaux.
- Parcelle : emplacement d'une construction.

Les budgets des collectivités sont séparés. Les transferts de l'État vers les régions ou villes sont enregistrés comme dépenses nationales et recettes locales, sans créer de richesse artificielle au niveau consolidé.

## Scénario de départ recommandé
Le premier scénario de développement est un pays fictif nommé provisoirement **Novaria**.
- 1 capitale et 4 villes secondaires dans le prototype.
- 3 régions.
- Une économie déjà active mais présentant des besoins d'investissement.
- Un budget initial paramétrable.
- Une population agrégée par ville, sans simulation individuelle de chaque habitant.

Le nom, le drapeau, les villes et les valeurs initiales devront rester modifiables avant la création d'une nouvelle partie.

## Indicateurs nationaux
- Population totale.
- Produit intérieur brut (PIB).
- Croissance réelle.
- Inflation.
- Taux de chômage.
- Recettes publiques.
- Dépenses publiques.
- Solde budgétaire.
- Dette publique.
- Satisfaction moyenne.

Chaque indicateur doit avoir une définition documentée. Les chiffres affichés dans l'interface doivent provenir des données simulées, jamais de valeurs de démonstration codées en dur.

## Système budgétaire
Chaque mouvement financier est une écriture comprenant :
- Identifiant unique.
- Date de simulation.
- Administration concernée.
- Catégorie.
- Montant signé.
- Description et origine.

Les recettes et dépenses sont calculées sur des périodes. Le solde budgétaire est égal aux recettes moins les dépenses. La dette évolue selon les emprunts et remboursements effectivement enregistrés.

Les budgets national et locaux ne peuvent pas dépenser au-delà des règles de financement définies. Les transferts entre administrations doivent être identifiables et éliminés lors du calcul des comptes publics consolidés.

## Villes et spécialisation
Chaque ville possède des caractéristiques distinctes :
- Capitale administrative.
- Ville industrielle.
- Ville portuaire et commerciale.
- Ville universitaire.
- Ville agricole.

Ces spécialisations influencent les emplois, les besoins en infrastructures et les revenus, mais ne doivent pas enfermer une ville dans un seul rôle.

## Construction
La première version utilisera un système de parcelles simplifié :
- Sélection d'une parcelle disponible.
- Choix d'un bâtiment compatible avec le zonage.
- Vérification du coût de construction et du budget.
- Déduction du coût.
- Ajout du bâtiment et de ses coûts d'entretien.
- Mise à jour visuelle de la ville.

Les routes, réseaux et contraintes de terrain seront ajoutés progressivement.

## Politique
Le président peut nommer les ministres, proposer des lois, fixer les taux d'imposition et lancer des programmes d'investissement. Les décisions peuvent modifier les recettes, les dépenses, l'emploi, les prix ou la satisfaction avec des délais d'effet documentés.

Les élections et la popularité seront ajoutées après validation de la boucle économique de base.

## Événements
Les événements sont déclenchés par des conditions de simulation ou par des scénarios. Chaque événement doit présenter :
- Un contexte compréhensible.
- Plusieurs décisions possibles lorsque pertinent.
- Un coût ou un effet explicite.
- Une durée et des conséquences.
- Une trace dans l'historique du pays.

## Hors périmètre du premier prototype
- Multijoueur.
- Connexion et comptes en ligne.
- Personnages individuels simulés.
- Diplomatie complexe.
- Guerre et armée détaillée.
- Économie mondiale complète.
- Modélisation 3D intérieure des bâtiments.

## Critères d'acceptation du prototype
- Le joueur peut créer une nouvelle partie.
- Une carte 3D de prototype affiche plusieurs villes.
- Le joueur peut sélectionner une ville.
- Le tableau de bord affiche les données de cette ville et du pays.
- Le temps peut être mis en pause et accéléré.
- Une action économique modifie les données et le journal financier.
- La partie peut être sauvegardée puis rechargée.
- Les données restent cohérentes après fermeture et redémarrage.
