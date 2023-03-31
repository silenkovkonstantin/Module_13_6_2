using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Module_13_6_2
{
    class Program
    {
        static List<string> wordsList = new();
        static HashSet<string> wordsSet = new();
        static Dictionary<string, int> wordsDict = new();

        static void Main(string[] args)
        {
            string text = string.Empty;

            string path = @"C:\Users\home\Downloads\Text1.txt";

            // Открываем файл и читаем построчно его содержимое
            using (StreamReader sr = File.OpenText(path))
            {
                while ((text = sr.ReadLine()) != null) // Пока не кончатся строки - считываем из файла по одной
                {
                    var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray()); // Убираем знаки пунктуации
                    string[] wordsArray = noPunctuationText.Split(); // Разбиваем строку на слова и добавляем  в массив
                    wordsList.AddRange(wordsArray); // Массив добавляем в список
                    wordsList.RemoveAll(c => c == ""); // Удаляем из списка все пустые строки
                }    
            }

            wordsSet = wordsList.ToHashSet(); // Оставляем только уникальные слова

            foreach (var word in wordsSet)
            {
                int count = 0;

                while (wordsList.Remove(word))
                {
                    count++;
                }

                wordsDict.Add(word, count);
            }

            var sortedWordsDict = wordsDict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

            int count1 = 0;

            // Выводим в консоль самые популярные 10 слов
            foreach (var key in sortedWordsDict.Keys)
            {
                Console.WriteLine(key);
                count1++;

                if (count1 == 10)
                    break;
            }
        }
    }
}
