﻿using System;
using System.Threading.Tasks;
using WindDataProcessing;

namespace GAMESA_01
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Path to the CSV file with Load Case Time Shares: ");
                // @"C:\Users\Mirek\source\repos\ZpracovaniDat\WindDataProcessing\TestovaciData\PRJ1.csv"
                string loadCasesTimeShareFilePath = @"C:\Users\miroslav.vaculka\source\repos\Mirabass\WindDataProcessor\WindDataProcessing\TestovaciData\SG5x_LDD.csv";//Console.ReadLine();
                Console.WriteLine($"You set: {loadCasesTimeShareFilePath}");
                Console.WriteLine("Path to the Project Directory: ");
                //@"C:\Users\Mirek\source\repos\ZpracovaniDat\WindDataProcessing\TestovaciData\PRJ1"
                string projectDirectoryPath = @"C:\Users\miroslav.vaculka\source\repos\Mirabass\WindDataProcessor\WindDataProcessing\TestovaciData\SG5x_LDD";//Console.ReadLine();
                Console.WriteLine($"You set: {projectDirectoryPath}");
                Console.WriteLine("Path to the Directory where results will be saved: ");
                //@"C:\Users\Mirek\source\repos\ZpracovaniDat\WindDataProcessing\TestovaciData"
                string resultsDirectoryPath = @"C:\Users\miroslav.vaculka\source\repos\Mirabass\WindDataProcessor\WindDataProcessing\TestovaciData";//Console.ReadLine();
                Console.WriteLine($"You set: {resultsDirectoryPath}");
                DataProcessor dataProcessor = new DataProcessor(loadCasesTimeShareFilePath, projectDirectoryPath, resultsDirectoryPath);
                dataProcessor.SourceDataType = Enums.SourceDataType.CSV;
                dataProcessor.SourceDataFirstLine = 19;
                dataProcessor.SourceDataColumn.FX = 2;
                dataProcessor.SourceDataColumn.FY = 3;
                dataProcessor.SourceDataColumn.FZ = 5;
                dataProcessor.SourceDataColumn.MY = 7;
                dataProcessor.SourceDataColumn.MZ = 9;
                dataProcessor.SourceDataColumn.Speed = 11;
                dataProcessor.NumberOfLevels = 144;

                dataProcessor.CP = new CalculationParametersCollection()
                {
                    FgShaft = 243778.5,
                    FgGearbox = 451260,
                    AxialPreload = 0, //,500000
                    n = 10.0 / 3.0,
                    FMB = new BearingParametersColection()
                    {
                        ContactAngle = 19,
                        Z = 52
                    },
                    RMB = new BearingParametersColection()
                    {
                        ContactAngle = 11,
                        Z = 62
                    }
                };

                await dataProcessor.LDDlifesTester();
                //await dataProcessor.Process(); - vytvoří LDD
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.Write(ex.StackTrace);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}