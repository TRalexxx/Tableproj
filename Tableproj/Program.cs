using System.Diagnostics;
using Tableproj.Controller;
using Tableproj.Model;
using Tableproj.View;

////ExcelClass excel = new ExcelClass("C:\\Users\\trale\\OneDrive\\Desktop\\doc.xlsx");
//ExcelClass excel = new ExcelClass();
//string name = Console.ReadLine();

//excel.SetRowName(name+"1", 2);
//excel.SetRowName(name+"2", 3);
//excel.SetRowName(name+"3", 4);
//excel.SetRowName(name+"4", 5);
//excel.SetRowName(name+"5", 6);
//excel.SetColumnName("y", 1);
//excel.SetColumnName("x1", 2);
//excel.SetColumnName("x2", 3);
//excel.SetColumnName("x3", 4);
//excel.SetColumnName("x4", 5);
//excel.SetColumnName("x5", 6);
//excel.FillRow("5 42 255 14.1 56 78 846 22.1 74", 2);
//excel.FillRow("45 7 15 23 48 11 11.4 26 43 15 2", 3);
//excel.FillRow("4 15 24 64 85 44 1 35 25 64 48 7", 4);
//excel.FillRow("11 34 45 6 87 489 15 35 44 44.1 5", 5);
//excel.SwitchRows(3, 4, 13);
//excel.Show();
////excel.SaveAs("C:\\Users\\trale\\OneDrive\\Desktop\\doc2.xlsx");
////excel.SaveFile("doc.xlsx");
///

BinaryMatrix matrix = new BinaryMatrix(10, 5);
matrix.FillMartixRandom();
ConsoleView.PrintBinaryMatrix(matrix);
Console.WriteLine();



EuclidDistance euclidDistance = new EuclidDistance(10);
euclidDistance.CalculateBinaryMatrixDistance(matrix);
ConsoleView.PrintDoubleMatrix(euclidDistance);
Console.WriteLine();

TransitiveClosure transitive = new TransitiveClosure(euclidDistance, 1);
transitive.CheckTransitiveClosure();
ConsoleView.PrintBinaryMatrix(transitive);

