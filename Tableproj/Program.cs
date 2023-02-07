using Tableproj.Model;

ExcelClass excel = new ExcelClass("C:\\Users\\trale\\OneDrive\\Desktop\\doc.xlsx");
//string name = Console.ReadLine();

//excel.SetRowName(name, 2);
//excel.SetRowName(name, 3);
//excel.SetRowName(name, 4);
//excel.SetRowName(name, 5);
//excel.SetRowName(name, 6);
//excel.SetColumnName(name, 1);
//excel.SetColumnName(name, 2);
//excel.SetColumnName(name, 3);
//excel.SetColumnName(name, 4);
//excel.SetColumnName(name, 5);
//excel.SetColumnName(name, 6);
excel.FillRow("5 42 255 14.1 56 78 846 22.1 74", 4);
excel.Show();
excel.SaveAs("C:\\Users\\trale\\OneDrive\\Desktop\\doc2.xlsx");
//excel.SaveFile("doc.xlsx");