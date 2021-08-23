using Microsoft.Office.Interop.Word;
using NamedPipeWrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace IRobotAlinaFake_ConsoleAppNetCore
{
    static class Program
    {
        private static NamedPipeClient<DataMessage> ocrClient;
        private static string attacmentFileName;
        private static byte[] attacmentContent;
        private static string ocrResult;
        private static bool isExcelFileMode = false;

        public static void Main()
        {       
            string folder = @"C:\Test";
            string folderOffice = Path.Combine(folder, "Office");
            string folderImage = Path.Combine(folder, "_Image");
            string folderLastErrors = Path.Combine(folder, "_LastErrors");
            string folderOneFile = Path.Combine(folder, "_OneFile");
            string oneFileName = Path.Combine(folderOneFile, "1.RPR.0120.23UBZ.0.KM.LC0001_C01.pdf");
            string excelFile = @"C:\Test\_xls\Контур.Закупки_10.04.2021.xlsx";
            string folderCableJournal = Path.Combine(folder, "_CableJournal");


            Console.WriteLine($"Задайте режим работы программы:" +
                $"\n1 - OCR тест набора PDF файлов" +
                $"\n2 - тест набора DOC(X) файлов" +
                $"\n3 - тест набора XML(S) файлов" +
                $"\n4 - тест набора файлов изображений" +
                $"\n5 - тест набора LastErrors" +
                $"\n6 - один файл ({oneFileName})" +
                $"\n7 - тест реконфигурации Excel-файла" +
                $"\n8 - тест Кабельный журнал" +
                $"\nостальное - выход");

            IEnumerable<string> files = null;
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    files = Directory.EnumerateFiles(folder, "*.*")
                        .Where(s => s.ToLowerInvariant().EndsWith(".pdf") || s.ToLowerInvariant().EndsWith(".bmp") || s.ToLowerInvariant().EndsWith(".png") || 
                            s.ToLowerInvariant().EndsWith(".jpeg") || s.ToLowerInvariant().EndsWith(".jpg") || s.ToLowerInvariant().EndsWith(".tif") || s.ToLowerInvariant().EndsWith(".tiff")).OrderBy(s => s);
                    break;

                case ConsoleKey.D2:
                    files = Directory.EnumerateFiles(folderOffice, "*.*")
                        .Where(s => s.ToLowerInvariant().EndsWith(".doc") || s.ToLowerInvariant().EndsWith(".docx")).OrderBy(s => s);                                            
                    break;

                case ConsoleKey.D3:
                    files = Directory.EnumerateFiles(folderOffice, "*.*")
                        .Where(s => s.ToLowerInvariant().EndsWith(".xls") || s.ToLowerInvariant().EndsWith(".xlsx")).OrderBy(s => s);
                    break;

                case ConsoleKey.D4:
                    files = Directory.EnumerateFiles(folderImage, "*.*").Where(s => !s.ToLowerInvariant().EndsWith(".txt")).OrderBy(s => s);
                    break;

                case ConsoleKey.D5:
                    files = Directory.EnumerateFiles(folderLastErrors, "*.*").Where(s => !s.ToLowerInvariant().EndsWith(".txt")).OrderBy(s => s);
                    break;

                case ConsoleKey.D6:                   
                    files = new List<string>() { { oneFileName } };
                    break;

                case ConsoleKey.D7:
                    files = new List<string>() { { excelFile } };
                    isExcelFileMode = true;
                    break;

                case ConsoleKey.D8:
                    files = Directory.EnumerateFiles(folderCableJournal, "*.*")
                        .Where(s => s.ToLowerInvariant().EndsWith(".jpg") || s.ToLowerInvariant().EndsWith(".jpeg")).OrderBy(s => s);
                    break;

                default:
                    return;
            }

            foreach (string pathFile in files.ToList())
            {
                Console.WriteLine($"\nFile: {pathFile}");

                ocrResult = string.Empty;

                try
                {
                    attacmentFileName = Path.GetFileName(pathFile);
                    attacmentContent = File.ReadAllBytes(pathFile);

                    ocrClient = new NamedPipeClient<DataMessage>(Constants.PIPE_NAME, applicationName: "IRobotAlinaFake") { AutoReconnect = false };
                    ocrClient.ServerMessage += OnServerMessage;                    
                    ocrClient.Error += OnError;
                    ocrClient.Start();

                    if (key == ConsoleKey.D6)
                        ocrClient.WaitForDisconnection();
                    else
                        ocrClient.WaitForDisconnection(TimeSpan.FromMinutes(5));

                    if (!isExcelFileMode)
                    {
                        var newFileName = Path.GetFileNameWithoutExtension(pathFile) + "-text.txt";
                        var newFilePath = Path.Combine(Path.GetDirectoryName(pathFile), newFileName);
                        File.WriteAllText(newFilePath, ocrResult);
                        Console.WriteLine($"Текст длиной {ocrResult?.Length ?? 0} сохранен в {newFilePath}");
                    }                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                finally
                {
                    ocrClient?.Stop();                    
                }
            }

            Console.WriteLine("\nТест завершен.");
            Console.ReadKey();            
        }

        private static void OnServerMessage(NamedPipeConnection<DataMessage, DataMessage> connection, DataMessage message)
        {
            try
            {                
                Console.WriteLine($"Получено сообщение: {DataMessageSettings.TypeStr[message.type]}");
                
                if (message.type == DataMessageSettings.MessageType.Init)
                {
                    DataMessageSettings.MessageType newType;

                    if (isExcelFileMode)
                    {
                        newType = DataMessageSettings.MessageType.Exl_Request;

                        connection.PushMessage(new Exl_DataMessage
                        {
                            type = newType,                            
                            fileName = attacmentFileName,
                            content = attacmentContent
                        });
                    }
                    else
                    {
                        newType = DataMessageSettings.MessageType.TE_Request;

                        connection.PushMessage(new TE_DataMessage
                        {
                            type = newType,
                            fileName = attacmentFileName,
                            content = attacmentContent
                        });
                    }

                    Console.WriteLine($"Отправлено сообщение: {DataMessageSettings.TypeStr[newType]}");
                }
                else
                {
                    //if (!(message is TE_DataMessage te_message))
                    //    throw new Exception($"Неизвестный класс сообщения, [{message.GetType().Name}].");

                    if (
                        message.type == DataMessageSettings.MessageType.TE_Rejection ||
                        message.type == DataMessageSettings.MessageType.TE_Response || 
                        message.type == DataMessageSettings.MessageType.Exl_Response || 
                        message.type == DataMessageSettings.MessageType.Error
                    )
                    {
                        if (message.type == DataMessageSettings.MessageType.TE_Rejection)
                        {
                            Thread.Sleep(new Random().Next(1000, 5000));
                            message.type = DataMessageSettings.MessageType.TE_Request;
                            connection.PushMessage(message);
                        }

                        if (message.type == DataMessageSettings.MessageType.TE_Response)
                            ocrResult = ((TE_DataMessage)message).serviceResult;

                        if (message.type == DataMessageSettings.MessageType.Exl_Response)
                        {                            
                            string preparedFileName = Path.Combine(@"C:\Test\_xls\_resultPrepared", string.Concat(Path.GetFileNameWithoutExtension(attacmentFileName), "_Prepared", ".xlsx"));
                            File.WriteAllBytes(preparedFileName, ((Exl_DataMessage)message).content);
                        }

                        ocrClient?.Stop();
                    }
                    else
                    {
                        throw new Exception("Неизвестный тип сообщения.");                        
                    }                    
                }
            }            
            catch (Exception ex)
            {                
                OnError(ex);
            }            
        }

        private static void OnError(Exception ex)
        {
            ocrClient?.Stop();
            Console.WriteLine($"ОШИБКА: {ex.Message}");
        }        
    }
}

