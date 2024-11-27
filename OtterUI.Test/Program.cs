using System.Xml.Serialization;
using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.ZEngineUI;
using ThemModdingHerds.ZEngineUI.Data;

XmlSerializer serializer = new(typeof(Scene));

string input = @"G:\SteamLibrary\steamapps\common\Them's Fightin' Herds\data01\ui-win\temp\UI\Win\Output\BuckCharSelect.gbs";
Scene scene = Scene.OpenRead(input);
scene.Save(input);