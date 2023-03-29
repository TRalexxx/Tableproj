using System.Diagnostics;
using Tableproj.Controller;
using Tableproj.Model;
using Tableproj.View;

ExcelController<double> ex = new ExcelController<double>("H:\\test.xlsx");
DataTable<double> dataTable = new DataTable<double>(ex.ReadExcel(150,4));
double t = dataTable[1, 5];
ConsoleView.PrintTable(dataTable);
Console.WriteLine();
EuclideanDistance distance = new EuclideanDistance();
//IClustering clustering = new TransitiveClustering(1, distance);
//ClusteringResult clusters = clustering.GetClusters(dataTable);

//Console.WriteLine(clusters.ToString());
//Console.ReadKey();

IClustering clustering = new KMeansClustering(4, distance);
ClusteringResult clusters = clustering.GetClusters(dataTable);
Console.WriteLine(clusters.ToString());
