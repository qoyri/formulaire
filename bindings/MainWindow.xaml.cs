using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

namespace Formulaire
{
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel _viewModel;

        /// <summary>
        /// Represents the main window of the application
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            // Initialize comboBoxGenre
            comboBoxGenre.Items.Add(new Gender
                { Icon = PackIconFontAwesomeKind.MarsSolid, Name = "Homme" });
            comboBoxGenre.Items.Add(new Gender
                { Icon = PackIconFontAwesomeKind.VenusSolid, Name = "Femme" });

            // Adding DataContext for ComboBox
            comboBoxGenre.DataContext = _viewModel.PersonneEnCours;
        }
    }

    public class Personne
    {
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Naissance { get; set; }
        public string Promo { get; set; }
        public string Genre { get; set; }
    }

    public class Gender
    {
        public PackIconFontAwesomeKind Icon { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}