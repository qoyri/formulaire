using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Formulaire
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Personne> Personnes { get; set; }
        private Personne personneEnCours;

        public MainWindow()
        {
            InitializeComponent();
            Personnes = new ObservableCollection<Personne>();
            Task.Run(() => LoadDataFromFile()).Wait();
            datagridInfos.ItemsSource = Personnes;

            // Initialize personneEnCours
            ResetPersonneEnCours();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            // Check that personneEnCours has valid values before adding it to the collection
            if (personneEnCours != null &&
                !string.IsNullOrEmpty(personneEnCours.Prenom) &&
                !string.IsNullOrEmpty(personneEnCours.Nom) &&
                !string.IsNullOrEmpty(personneEnCours.Naissance) &&
                !string.IsNullOrEmpty(personneEnCours.Promo))
            {
                // Add the current person to the collection
                Personnes.Add(personneEnCours);
                Task.Run(() => SaveDataToFile());

                // Reset personneEnCours for new entries
                ResetPersonneEnCours();
            }
            else
            {
                MessageBox.Show("Please fill all the information before adding a new person.");
            }
        }

        private void ResetPersonneEnCours()
        {
            personneEnCours = new Personne();
            this.DataContext = personneEnCours;
        }

        private async Task LoadDataFromFile()
        {
            var stream = getPath();
            {
                if (stream == null)
                {
                    return; // The stream can be null if the resource is not found.
                }

                using (StreamReader MyFile = new StreamReader(stream))
                {
                    string TxtContent;
                    do
                    {
                        TxtContent = await MyFile.ReadLineAsync();
                        if (TxtContent != null)
                        {
                            string[] elements = TxtContent.Split(',');
                            if (elements.Length == 4)
                            {
                                var newPerson = new Personne()
                                {
                                    Prenom = elements[0],
                                    Nom = elements[1],
                                    Naissance = elements[2],
                                    Promo = elements[3]
                                };
                                Personnes.Add(newPerson);
                            }
                        }
                    } while (TxtContent != null);
                }
            }
        }

        private string getPath()
        {
            string path = System.AppDomain.CurrentDomain
                .BaseDirectory; // obtient le chemin du répertoire de l'application courante
            return System.IO.Path.Combine(path, "save.txt"); // combine le chemin avec le nom du fichier
        }

        private async Task SaveDataToFile()
        {
            var path = getPath();
            try
            {
                using (StreamWriter
                       writer = new StreamWriter(path, false)) // 'false' pour écraser le fichier s'il existe déjà
                {
                    foreach (var person in Personnes)
                    {
                        var ligne = $"{person.Prenom},{person.Nom},{person.Naissance},{person.Promo}";
                        await writer.WriteLineAsync(ligne);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error saving data: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public async void RemovePersonne(Personne personne)
        {
            Personnes.Remove(personne);
            await SaveDataToFile();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var personne = (Personne)button.DataContext;
            RemovePersonne(personne);
        }
    }
    
    public class Personne
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Naissance { get; set; }
        public string Promo { get; set; }
    }
}