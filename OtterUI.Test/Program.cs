using ThemModdingHerds.IO.Binary;
using ThemModdingHerds.OtterUI;

string path = "NewScene1.gbs";
Reader reader = new(path);
Scene scene = reader.ReadOtterUIScene();
Console.WriteLine();