using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using bindings;
using Formulaire;

public class MainViewModel : ViewModelBase
{
    private ObservableCollection<Personne> _personnes;
    private Personne _personneeEnCours;
    private ICommand _addPersonCommand;
    private ICommand _deletePersonCommand;

    public MainViewModel()
    {
        _personnes = new ObservableCollection<Personne>();
        LoadDataFromFile();

        _personneeEnCours = new Personne();

        _addPersonCommand = new RelayCommand(param => AddPerson(),
            param => CanAddPerson());

        _deletePersonCommand = new RelayCommand(param => RemovePerson((Personne)param),
            param => _personnes.Contains((Personne)param));
    }

    public ObservableCollection<Personne> Personnes
    {
        get { return _personnes; }
        set
        {
            _personnes = value;
            OnPropertyChanged(nameof(Personnes));
        }
    }

    public Personne PersonneEnCours
    {
        get { return _personneeEnCours; }
        set
        {
            _personneeEnCours = value;
            OnPropertyChanged(nameof(PersonneEnCours));
        }
    }

    public ICommand AddPersonCommand
    {
        get { return _addPersonCommand; }
    }

    public ICommand DeletePersonCommand
    {
        get { return _deletePersonCommand; }
    }

    private void LoadDataFromFile()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "save.txt");
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] elements = line.Split(',');
                    if (elements.Length == 5)
                    {
                        Personne p = new Personne
                        {
                            Prenom = elements[0],
                            Nom = elements[1],
                            Naissance = elements[2],
                            Promo = elements[3],
                            Genre = elements[4]
                        };
                        _personnes.Add(p);
                    }
                }
            }
        }
    }

    private bool CanAddPerson()
    {
        return _personneeEnCours != null &&
               !string.IsNullOrEmpty(_personneeEnCours.Prenom) &&
               !string.IsNullOrEmpty(_personneeEnCours.Nom) &&
               !string.IsNullOrEmpty(_personneeEnCours.Naissance) &&
               !string.IsNullOrEmpty(_personneeEnCours.Promo) &&
               !string.IsNullOrEmpty(_personneeEnCours.Genre);
    }

    private void AddPerson()
    {
        _personnes.Add(_personneeEnCours);
        SaveDataToFile();
        _personneeEnCours = new Personne(); // Reset PersonneEnCours for new entries
    }

    private void RemovePerson(Personne person)
    {
        if (_personnes.Contains(person))
        {
            _personnes.Remove(person);
            SaveDataToFile();
        }
    }

    private void SaveDataToFile()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "save.txt");
        using (StreamWriter
               writer = new StreamWriter(path, false)) // 'false' overwrites the file if it already exists
        {
            foreach (var person in _personnes)
            {
                var text = $"{person.Prenom},{person.Nom},{person.Naissance},{person.Promo},{person.Genre}";
                writer.WriteLine(text);
            }
        }
    }
}