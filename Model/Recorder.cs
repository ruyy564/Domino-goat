using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// Класс для записи рекордов
    /// </summary>
    public class Recorder
    {
        /// <summary>
        /// Путь к файлу с рекордами
        /// </summary>
        private static string _path = "../../records.txt";

        /// <summary>
        /// Getter и Setter для поля path
        /// </summary>
        public static string Path { get => _path; set => _path = value; }

        /// <summary>
        /// Метод записывает рекорд пользователя во внешний файл
        /// </summary>
        public static void AddRecord(int parUserRecord, string parUserName)
        {
            File.AppendAllText(_path, $"{parUserRecord} - \"{parUserName}\"" + "\r\n");
        }

        /// <summary>
        /// Метод сортировки массива рекордов по убыванию
        /// </summary>
        /// <param name="parArray">Несортированный массив</param>
        /// <returns>Сортированный массив</returns>
        private static string[] SortArray(string[] parArray)
        {
            string[] result = new string[parArray.Length];
            parArray.CopyTo(result, 0);
            string[] firstString;
            string[] secondString;
            string tmp;

            for (int k = 0; k < result.Length - 1; k++)
            {
                for (int i = 0; i < result.Length - 1; i++)
                {
                    firstString = result[i].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    secondString = result[i + 1].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Convert.ToInt32(secondString[0].Trim(' ')) > Convert.ToInt32(firstString[0].Trim(' ')))
                    {
                        tmp = result[i];
                        result[i] = result[i + 1];
                        result[i + 1] = tmp;
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// Метод считывает рекорды из файла и возвращает их в отсортированном виде
        /// </summary>
        public static List<string> GetListRecords()
        {
            string[] outRecords = File.ReadAllLines(Path);

            return SortArray(outRecords).ToList();
        }
    }
}
