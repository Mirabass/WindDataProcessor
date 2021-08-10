﻿using System;
using System.Collections.Generic;
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
                Dictionary<int, Tuple<string, string, string>> pathSettings = new Dictionary<int, Tuple<string, string, string>>
                {
                    { 1, new Tuple<string, string, string>
                        (
                            @"C:\Users\miroslav.vaculka\source\repos\Mirabass\WindDataProcessor\WindDataProcessing\TestovaciData\SG5x_LDD.csv",
                            @"C:\Users\miroslav.vaculka\source\repos\Mirabass\WindDataProcessor\WindDataProcessing\TestovaciData\SG5x_LDD",
                            @"C:\Users\miroslav.vaculka\source\repos\Mirabass\WindDataProcessor\WindDataProcessing\TestovaciData"
                        )},
                    { 2, new Tuple<string, string, string>
                        (
                            @"\\brn-fs-01\DATA _ZKL\Data\ZKL VaV\ZKL_dokumenty\PROJEKTY\Spanelsko\Gamesa - loziska hlavniho hridele\SG5x\OneDrive_2020-04-17\PRJ-6076\testRates.csv",
                            @"\\brn-fs-01\DATA _ZKL\Data\ZKL VaV\ZKL_dokumenty\PROJEKTY\Spanelsko\Gamesa - loziska hlavniho hridele\SG5x\OneDrive_2020-04-17\PRJ-6076\Test",
                            @"\\brn-fs-01\DATA _ZKL\Data\ZKL VaV\ZKL_dokumenty\PROJEKTY\Spanelsko\Gamesa - loziska hlavniho hridele\SG5x\OneDrive_2020-04-17\PRJ-6076"
                        )},
                    { 3, new Tuple<string, string, string>
                        (
                            @"\\brn-fs-01\DATA _ZKL\Data\ZKL VaV\ZKL_dokumenty\PROJEKTY\Spanelsko\Gamesa - loziska hlavniho hridele\SG5x\OneDrive_2020-04-17\PRJ-6076\PRJ-6076_rates.csv",
                            @"\\brn-fs-01\DATA _ZKL\Data\ZKL VaV\ZKL_dokumenty\PROJEKTY\Spanelsko\Gamesa - loziska hlavniho hridele\SG5x\OneDrive_2020-04-17\PRJ-6076\PRJ-6076_TIMESERIES",
                            @"\\brn-fs-01\DATA _ZKL\Data\ZKL VaV\ZKL_dokumenty\PROJEKTY\Spanelsko\Gamesa - loziska hlavniho hridele\SG5x\OneDrive_2020-04-17\PRJ-6076"
                        )}
                };
                const int choosedSettings = 3;
                string loadCasesTimeShareFilePath = pathSettings[choosedSettings].Item1;
                Console.WriteLine($"You set: {loadCasesTimeShareFilePath}");
                Console.WriteLine("Path to the Project Directory: ");
                string projectDirectoryPath = pathSettings[choosedSettings].Item2;
                Console.WriteLine($"You set: {projectDirectoryPath}");
                Console.WriteLine("Path to the Directory where results will be saved: ");
                string resultsDirectoryPath = pathSettings[choosedSettings].Item3;
                Console.WriteLine($"You set: {resultsDirectoryPath}");
                DataProcessor dataProcessor = new DataProcessor(loadCasesTimeShareFilePath, projectDirectoryPath, resultsDirectoryPath);
                dataProcessor.SourceDataType = Enums.SourceDataType.CSV;
                dataProcessor.SourceDataFirstLine = 19;
                dataProcessor.SourceDataColumn.FX = 2;//2
                dataProcessor.SourceDataColumn.FY = 3;//3
                dataProcessor.SourceDataColumn.FZ = 5;//5
                dataProcessor.SourceDataColumn.MY = 7;//7
                dataProcessor.SourceDataColumn.MZ = 9;//9
                dataProcessor.SourceDataColumn.Speed = 11;//11
                dataProcessor.NumberOfLevels = 144;

                dataProcessor.CP = new CalculationParametersCollection()
                {
                    FgShaft = 243778.5,
                    FgGearbox = 451260,
                    AxialPreload = 500000,
                    n = 10.0 / 3.0,
                    StiffnesCoefficient_a = 16809759.7,
                    StiffnesCoefficient_b = 7650757.1,
                    StiffnesCoefficient_c = 516149.5,
                    StiffnesCoefficient_d = 7652307.4,
                    StiffnesCoefficient_e = -4633025.6,
                    StiffnesCoefficient_f = 495385.3,
                    FMB = new BearingParametersColection()
                    {
                        ContactAngle = 19,
                        Z = 52,
                        Arm_a = 361.6
                    },
                    RMB = new BearingParametersColection()
                    {
                        ContactAngle = 14,
                        Z = 62,
                        Arm_a = 250.25
                    }
                };

                await dataProcessor.BearingReactions();
                //await dataProcessor.LDDlifesTester(); //- Vývoj
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