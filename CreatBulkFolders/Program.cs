using System;
using System.Collections.Generic;
using System.IO;

namespace CreatBulkFolders
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Do you want to create Book wise folders?(Y/N)");
            string option = Console.ReadLine();
            if (option.ToLower() == "y")
            {
                BookWiseFolderCreation();
                return;
            }

            try
            {
                string currentPath = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine("Enter the File Name Containing \"Folder names, Line by Line.\"");
                string fileName = Console.ReadLine();
                Console.WriteLine("Enter the parent folder name/book name (in which all your folders will be created)");
                string parentFolder = Console.ReadLine();

                string textFile = currentPath + fileName;
                string parentDir = currentPath + parentFolder;
                string outputText = "";
                int created = 0, error = 0;
                var problemList = new List<string>();
                if (!File.Exists(textFile))
                {
                    outputText = " The File: " + textFile + " Does Not Exists";
                    Console.WriteLine(outputText);
                }
                else if (Directory.Exists(parentDir))
                {
                    outputText = " The Folder: " + parentDir + " Already Exists in current folder";
                    Console.WriteLine(outputText);
                }
                else
                {
                    Directory.CreateDirectory(parentDir);
                    // Read file using StreamReader. Reads file line by line  
                    using (StreamReader file = new StreamReader(textFile))
                    {

                        string folderName;
                        while ((folderName = file.ReadLine()) != null)
                        {
                            try
                            {
                                //Console.WriteLine(folderName);
                                var folderPath = parentDir + "\\" + folderName;
                                if (folderName.Contains(' '))
                                {
                                    throw new Exception("Contains Spaces");
                                }
                                if (Directory.Exists(folderPath))
                                {
                                    throw new Exception("Duplicate name, Folder already exists");
                                }
                                Directory.CreateDirectory(folderPath);
                                created++;
                            }
                            catch (Exception ex)
                            {
                                outputText += Environment.NewLine + "Folder name: " + folderName +
                                    Environment.NewLine + "ERROR: " + ex.Message;
                                problemList.Add(folderName);
                                error++;
                            }
                        }

                        file.Close();
                        Console.WriteLine($"Folders Created {created} , Errors {error} ");
                    }
                }
                FileStream fs = new FileStream(currentPath + "results_" + Path.GetRandomFileName() + ".txt", FileMode.CreateNew, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(outputText);
                sw.WriteLine($"Folders Created {created} , Errors {error} ");
                if (problemList.Count > 0)
                {
                    sw.WriteLine("Errors occured in following names");
                    foreach (string ddc in problemList)
                    {
                        sw.WriteLine(ddc);
                    }
                }
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }
    
    
        static void BookWiseFolderCreation()
        {
            try
            {
                string currentPath = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine("Enter the File Name Containing \"Folder names, with parent folder name/book  Line by Line.\"");
                string fileName = Console.ReadLine();
                

                string textFile = currentPath + fileName;
                string parentDir = "";
                string outputText = "";
                int created = 0, error = 0, lineNo = 0, errorReadingLine = 0;
                var problemList = new List<string>();
                Dictionary<string, List<string>> hash = new Dictionary<string, List<string>>();
                if (!File.Exists(textFile))
                {
                    outputText = " The File: " + textFile + " Does Not Exists";
                    Console.WriteLine(outputText);
                }                
                else
                {
                    // Read file using StreamReader. Reads file line by line  
                    using (StreamReader file = new StreamReader(textFile))
                    {
                        string line, bookNumber;                        
                        // DDC CODE                        
                        string folderName;
                        while ((line = file.ReadLine()) != null)
                        {
                            try
                            {
                                lineNo++;
                                var splittedLine = line.Split(',');
                                if(splittedLine.Length != 2)
                                {
                                    throw new Exception("In-Correct format at splitting");
                                }
                                folderName = splittedLine[0];
                                bookNumber = splittedLine[1];
                                if (string.IsNullOrWhiteSpace(folderName) || string.IsNullOrWhiteSpace(bookNumber))
                                {
                                    throw new Exception($"In-Correct format folder name or parent folder name empty");
                                }
                                if (!hash.ContainsKey(bookNumber))
                                {
                                    hash[bookNumber] = new List<string>();
                                }
                                hash[bookNumber].Add(folderName);
                            }
                            catch (Exception ex)
                            {
                                outputText += Environment.NewLine + "Line No: " + lineNo + " ERROR: " + ex.Message;
                                errorReadingLine++;
                            }
                        }
                        file.Close();
                        Console.WriteLine($"{lineNo} Lines read, {errorReadingLine} errors occured...");
                    }

                    //creat folders
                    foreach(KeyValuePair<string, List<string>> pair in hash)
                    {
                        string parentFolder = pair.Key;
                        parentDir = currentPath + parentFolder;
                        var allFoldersOfBook = pair.Value;
                        if (Directory.Exists(parentDir))
                        {
                            string txt = " The Parent Folder: " + parentDir + " Already Exists in current folder";
                            outputText += Environment.NewLine + txt;
                            Console.WriteLine(txt);
                        }
                        else
                        {
                            Directory.CreateDirectory(parentDir);
                        }

                        foreach (var folderName in allFoldersOfBook)
                        {
                            try
                            {
                                var folderPath = parentDir + "\\" + folderName;
                                if (folderName.Contains(' '))
                                {
                                    throw new Exception("Contains Spaces");
                                }
                                if (Directory.Exists(folderPath))
                                {
                                    throw new Exception("Duplicate name, Folder already exists");
                                }
                                Directory.CreateDirectory(folderPath);
                                created++;
                            }
                            catch (Exception ex)
                            {
                                outputText += Environment.NewLine + "folder name: " + folderName +
                                    Environment.NewLine + "ERROR: " + ex.Message;
                                problemList.Add(folderName);
                                error++;
                            }
                        }

                    }
                }
                Console.WriteLine($"Folders Created {created} , Errors {error} ");
                Console.WriteLine($"{lineNo} Lines read, {errorReadingLine} errors occured in reading...");
                FileStream fs = new FileStream(currentPath + "results_" + Path.GetRandomFileName() + ".txt", FileMode.CreateNew, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(outputText);
                sw.WriteLine($"{lineNo} Lines read, {errorReadingLine} errors occured in reading...");
                sw.WriteLine($"Folders Created {created} , Errors {error} ");
                if (problemList.Count > 0)
                {
                    sw.WriteLine("Errors occured in following DDC");
                    Console.WriteLine("Errors occured in following DDC");
                    foreach (string ddc in problemList)
                    {
                        sw.WriteLine(ddc);
                        Console.WriteLine(ddc);
                    }
                }
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Console.WriteLine("press any key to exit");
            Console.ReadKey();
        }
    }
}
