using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI;

namespace ZEngineUIEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Scene? CurrentScene { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnMenuOpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Filter = "OtterUI .gbs file|*.gbs"
            };
            bool? result = dialog.ShowDialog(this);
            if(result == true)
            {
                Reader reader = new(dialog.FileName);
                Scene scene = reader.ReadZEngineUIScene();
                LoadScene(scene);
            }
        }
        private void LoadScene(Scene scene)
        {
            CurrentScene = scene;
            SceneStructure.Items.Clear();
            TreeViewItem item = new()
            {
                Header = scene,
                DataContext = scene
            };
            IEnumerable<ControlDataItem> views = from view in scene.Views select ControlDataItem.CreateViewItem(view);
            foreach (var view in views)
            {
                item.Items.Add(view);
            }
            SceneStructure.Items.Add(item);
        }

        private void OnSceneStructureSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = (TreeViewItem)SceneStructure.SelectedItem;
            Inspector.Children.Clear();
            if(item is ControlDataItem controlData)
            {
                Inspector.Children.Add(ControlDataItem.CreatePropertiesTree(controlData.ControlData));
            }
        }
    }
}