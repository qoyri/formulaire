# Formulaire

C'est une base de projet WPF (Windows Presentation Foundation) utilisant le .NET Framework.

## Aperçu

Le projet est composé d'une application avec une interface utilisateur personnalisée, définie en XAML. Il contient plusieurs styles customisés pour différents contrôles tels que `Window`, `TextBox`, `Button`, `DataGrid` et `DataGridColumnHeader`.

## Styles

- `PrimaryColor`, `AccentColor`, `AccentColorRed`, `ControlBackgroundColor`, `TextColor` : Ce sont des `SolidColorBrush` qui représentent les couleurs de base utilisées dans l'application.

- `TextBox Style`, `Button Style`, `DataGrid Style`, `DataGridColumnHeader Style` : Ces styles sont utilisés pour personnaliser l'apparence des contrôles correspondants dans l'application.

- `DeleteButtonStyle` : C'est un style spécifique qui est utilisé pour les boutons de suppression dans l'application. Lorsque la souris est dessus, le fond devient la `AccentColorRed`. Lorsqu'il est pressé, le fond devient `#700F00` (un ton de rouge).

## Lancer l'application

Pour lancer l'application, assurez-vous d'avoir .NET Framework installé sur votre machine. Ouvrez le projet dans JetBrains Rider (ou tout autre IDE compatible .NET) et exécutez l'application.

## Contribuer

Les contributions sont les bienvenues. N'hésitez pas à ouvrir une `issue` ou à soumettre une `pull request`.