// See https://aka.ms/new-console-template for more information
using MD1_hs23014;
//kā paraugs ņemts mājas darba apraksta 10. uzdevums
string path = @"C:\Temp\data.xml";
var dm = new SchoolDataManager();
dm.CreateTestData();
Console.WriteLine("Data after CreateTestData:");
Console.WriteLine(dm.Print());
dm.Save(path);

dm.Reset();
Console.WriteLine("\nData after Reset:");
Console.WriteLine(dm.Print());

dm.Load(path);
Console.WriteLine("Data after Load:");
Console.WriteLine(dm.Print());

Console.ReadLine();