# Règles économiques — socle de simulation

## Objectif
Définir des règles cohérentes et vérifiables pour la première économie jouable. Les formules sont volontairement simples au départ et pourront être enrichies sans casser les sauvegardes.

## Comptabilité
Tous les montants sont stockés en unités monétaires entières (centimes) ou en valeurs décimales contrôlées. L'interface peut afficher des montants arrondis en milliers, millions ou milliards.

Une écriture financière possède un montant positif pour une recette et négatif pour une dépense. Les totaux sont calculés à partir des écritures, pas saisis indépendamment.

## Budget
- Solde de période = recettes de période − dépenses de période.
- Solde cumulé = solde précédent + solde de période.
- Dette = dette précédente + nouveaux emprunts − remboursements de principal.
- Les intérêts sont une dépense distincte.
- Un transfert entre administrations est une dépense pour l'administration qui paie et une recette pour celle qui reçoit.

## PIB
Le PIB représente la valeur ajoutée produite sur le territoire pendant une période. Il ne doit pas être assimilé au budget de l'État ni à la somme des ventes brutes.

Le prototype peut utiliser des secteurs agrégés (agriculture, industrie, construction, services) et une valeur ajoutée par secteur.

## Emploi
La population active est composée des personnes en emploi et des personnes au chômage qui recherchent un emploi.
Taux de chômage = chômeurs / population active × 100.
Les personnes inactives ne sont pas comptées dans le dénominateur.

## Inflation
L'inflation mesure la variation d'un indice de prix entre deux périodes :
Inflation = (indice actuel / indice précédent − 1) × 100.
Les changements de fiscalité indirecte et les chocs d'offre peuvent influencer les prix, avec des délais.

## Fiscalité
Les recettes fiscales dépendent d'une assiette taxable et d'un taux effectif. Elles ne doivent pas être calculées uniquement en multipliant le taux par le PIB total.

## Garde-fous
- Refuser les dépenses qui dépassent les crédits disponibles, sauf mécanisme explicite d'emprunt.
- Vérifier les valeurs négatives ou non finies.
- Journaliser toutes les opérations financières.
- Tester les formules avec des cas simples connus.
- Ne pas arrondir les valeurs intermédiaires à chaque étape.
- Distinguer données nominales et données réelles lorsque l'inflation est implémentée.
