using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;



namespace Translator
{
    
    class Word
    {
        const string RUSWord = "йцукенгшщзхъфывапролджэячсмитьёбюъЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮЁ";
        const string ENGWord = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        public string TextWord { get; set; }

        public Word(string textword)
        {
            this.TextWord = textword;
        }
        
        public string DefinitionWord(string textword)
        {
            int RuCount = 0;
            int EngCount = 0;

            for (int i = 0; i < textword.Length; i++)
            {
                for (int j = 0; j < RUSWord.Length; j++) if (textword[i] == RUSWord[j]) RuCount++;
                for (int j = 0; j < ENGWord.Length; j++) if (ENGWord[j] == textword[i]) EngCount++;
            }
            if (RuCount == textword.Length) return "Rus";
            if (EngCount == textword.Length) return "Eng";
            return null;
        }
    }
    class EngWord : Word
    {
        public string engword { get; set; }

        public EngWord(string textword=null, string engword=null) : base(textword)
        {
            this.engword = engword;
        }


    }
    class EngRus
    {
        public string Word { get; set; } // Английское слово
        public string WordTranslate { get; set; } // Перевод на русский английского слова
        public List<string> AllWordTranslate { get; set; } // Похожие слова
    }
    
    class Program
    {
        const string RUSWord = "йцукенгшщзхъфывапролджэячсмитьёбюъЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮЁ";
        const string Word = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        static string RusOrEng(string word) // Метод проверяем слово Анг или Рус
        {
            int RuCount = 0;
            int EngCount = 0;

            for (int i = 0; i < word.Length; i++)
            {
                for (int j = 0; j < RUSWord.Length; j++) if (word[i] == RUSWord[j]) RuCount++;
                for (int j = 0; j < Word.Length; j++) if (word[i] == Word[j]) EngCount++;
            }
            if (RuCount == word.Length) return "Rus";
            if (EngCount == word.Length) return "Eng";
            return null;
        }
        static void TranslatorWord()
        {
            int RuCount = 0;
            int EngCount = 0;
            int LengthTranWord = 0;

            Console.Write("Введите перевод: ");
            string TranslatorWord = Console.ReadLine();
            
            if (TranslatorWord.Length >= 3) LengthTranWord = TranslatorWord.Length;

            for (int i = 0; i < TranslatorWord.Length; i++)
            {
                for (int j = 0; j < RUSWord.Length; j++)
                {
                    if (TranslatorWord[i] == RUSWord[j]) RuCount++;
                }
                for (int j = 0; j < Word.Length; j++) if (TranslatorWord[i] == Word[j]) EngCount++;
            }
            List<EngRus> engRus;
            
            string json = File.ReadAllText("EngRus.json");
            engRus = JsonSerializer.Deserialize<List<EngRus>>(json);
            

            // все буквы определяем какое слово
            if (RuCount == TranslatorWord.Length && TranslatorWord.Length != 0)
            {
                foreach (var item in engRus)
                {

                    
                    for (int i = 0; i <= item.Word.Length - LengthTranWord; i++)
                    {
                        if (TranslatorWord == item.Word.Substring(i, LengthTranWord))
                        {
                            Console.WriteLine($"{item.Word} - {item.WordTranslate}");
                            Console.WriteLine("Похожие слова: ");
                            for (int j = 0; j < item.AllWordTranslate.Count-1; j++)
                                Console.WriteLine(item.AllWordTranslate[j]);
                        }
                        
                    }

                    for (int i = 0; i <= item.WordTranslate.Length - LengthTranWord; i++)
                        if (TranslatorWord == item.WordTranslate.Substring(i, LengthTranWord))
                        {
                            Console.WriteLine($"{item.WordTranslate} - {item.Word}");
                            Console.WriteLine("Похожие слова - ");
                            for (int j = 0; j < item.AllWordTranslate.Count - 1; j++)
                               Console.WriteLine(item.AllWordTranslate[j]);
                        }
                }
            }

            if (EngCount == TranslatorWord.Length && TranslatorWord.Length != 0)
            {
                foreach (var item in engRus)
                {
                    for (int i = 0; i <= item.Word.Length - LengthTranWord; i++)
                        if (TranslatorWord == item.Word.Substring(i, LengthTranWord))
                        {
                            Console.WriteLine($"{item.Word} - {item.WordTranslate}");
                            Console.WriteLine("Похожие слова: ");
                            for (int j = 0; j < item.AllWordTranslate.Count - 1; j++)
                                Console.WriteLine(item.AllWordTranslate[j]);
                        }
                    
                    for (int i = 0; i <= item.WordTranslate.Length - LengthTranWord; i++)
                        if (TranslatorWord == item.WordTranslate.Substring(i, LengthTranWord))
                        {
                            Console.WriteLine($"{item.WordTranslate} - {item.Word}");
                            Console.WriteLine("Похожие слова: ");
                            for (int j = 0; j < item.AllWordTranslate.Count - 1; j++)
                                Console.WriteLine(item.AllWordTranslate[j]);
                        }

                }

            }
        }
        static void AddWordTranslator()
        {
            Console.Write("Введите слово для добавления(англ. или рус.): ");
            string AddWord = Console.ReadLine();

            // Добавляем русское or English слово с переводом все буквы русские
            if (RusOrEng(AddWord) == "Rus" || RusOrEng(AddWord) == "Eng") 
            {
                Console.Write($"{AddWord} - ");
                string EngAddWord = Console.ReadLine();
                List<string> OtherWord = new List<string>();
                
                while (true)
                { 
                Console.Write("Дбавить еще слова к переводу? (Y/N) ");
                
                    var YN = Console.ReadKey().Key;
                    Console.WriteLine();
                    if (YN == ConsoleKey.Y)
                    {
                        Console.Write($"{AddWord} - {EngAddWord} - ");
                        string AddOtherWord = Console.ReadLine();
                        OtherWord.Add(AddOtherWord);
                    }
                    else break;

                }
               
                List<EngRus> engRus=new List<EngRus>();
                string json = File.ReadAllText("EngRus.json");
                engRus = JsonSerializer.Deserialize<List<EngRus>>(json);
                //Добавляем в List
                engRus.Add(new EngRus() { Word = AddWord, WordTranslate = EngAddWord, AllWordTranslate= OtherWord });
                // Записываем в файл
                json = JsonSerializer.Serialize(engRus);
                File.WriteAllText("EngRus.json", json);

                
            }
            
        }
        static void SearchWordSub()
        {
            Console.Write("Введите слово для поиска(англ. или рус.): ");
            string SearchWord = Console.ReadLine();

            int LengthSearchWord = 3; // Длина поиска
            if (SearchWord.Length >= 3) LengthSearchWord = SearchWord.Length;

            List<EngRus> engRus = new List<EngRus>();
            string json = File.ReadAllText("EngRus.json");
            engRus = JsonSerializer.Deserialize<List<EngRus>>(json);

            foreach (var item in engRus)
            {
                if (RusOrEng(SearchWord) == "Rus")
                {
                    for (int i = 0; i <= item.WordTranslate.Length - LengthSearchWord; i++)
                    {
                        if (SearchWord == item.WordTranslate.Substring(i, LengthSearchWord))
                        {
                            Console.WriteLine(item.WordTranslate);
                            break;
                        }
                    }

                    for (int i = 0; i <= item.Word.Length - LengthSearchWord; i++)
                    {
                        if (SearchWord == item.Word.Substring(i, LengthSearchWord))
                        {
                            Console.WriteLine(item.Word);
                            break;
                        }
                    }

                }

                if (RusOrEng(SearchWord) == "Eng")
                {

                    for (int i = 0; i <= item.WordTranslate.Length - LengthSearchWord; i++)
                    {
                        if (SearchWord == item.WordTranslate.Substring(i, LengthSearchWord))
                        {
                            Console.WriteLine(item.WordTranslate);
                            break;
                        }
                    }

                    for (int i = 0; i <= item.Word.Length - LengthSearchWord; i++)
                    {
                        if (SearchWord == item.Word.Substring(i, LengthSearchWord))
                        {
                            Console.WriteLine(item.Word);
                            break;
                        }
                    }
                }
            }

        }
        static void ChangeWordSub ()
        {
            Console.Write("Введите слово для изменения(англ. или рус.): ");
            string EditWord = Console.ReadLine();
        List<EngRus> engRus = new List<EngRus>();
        string json = File.ReadAllText("EngRus.json");

            if (RusOrEng(EditWord) == "Rus")
            {
                engRus = JsonSerializer.Deserialize<List<EngRus>>(json);
                foreach (var item in engRus)
                { 
                  if(item.WordTranslate == EditWord)
                    {
                        Console.Write($"{EditWord} - (введите изм. слово) ");
                        EditWord = Console.ReadLine();
                        Console.WriteLine($"Изменили слово {item.WordTranslate} на {EditWord}");
                        item.WordTranslate = EditWord;
                    }

                  if (item.Word == EditWord)
                    {
                        Console.Write($"{EditWord} - (введите изм. слово) ");
                        EditWord = Console.ReadLine();
                        Console.WriteLine($"Изменили слово {item.WordTranslate} на {EditWord}");
                        item.Word = EditWord;
                    }

                }
            }

            if (RusOrEng(EditWord) == "Eng")
{
    engRus = JsonSerializer.Deserialize<List<EngRus>>(json);
    foreach (var item in engRus)
    {
        if (item.WordTranslate == EditWord)
        {
            Console.Write($"{EditWord} - (введите изм. слово) ");
            EditWord = Console.ReadLine();
            Console.WriteLine($"Изменили слово {item.WordTranslate} на {EditWord}");
            item.WordTranslate = EditWord;
        }

        if (item.Word == EditWord)
        {
            Console.Write($"{EditWord} - (введите изм. слово) ");
            EditWord = Console.ReadLine();
            Console.WriteLine($"Изменили слово {item.Word} на {EditWord}");
            item.Word = EditWord;
        }

    }
}

json = JsonSerializer.Serialize(engRus);
File.WriteAllText("EngRus.json", json);
        }
        static void DeleteWordSub()
        {
            Console.Write("Введите слово для удаления(англ. или рус.): ");
            string RemoveWord = Console.ReadLine();
            List<EngRus> engRus = new List<EngRus>();

            string json = File.ReadAllText("EngRus.json");
            engRus = JsonSerializer.Deserialize<List<EngRus>>(json);

            foreach (var item in engRus)
            {

                if (RusOrEng(RemoveWord) == "Rus")
                {
                    if (item.Word == RemoveWord || item.WordTranslate == RemoveWord)
                    { engRus.Remove(item); break; }
                }
                if (RusOrEng(RemoveWord) == "Eng")
                {
                    if (item.Word == RemoveWord || item.WordTranslate == RemoveWord)
                    { engRus.Remove(item); break; }
                }
            }

            json = JsonSerializer.Serialize(engRus);
            File.WriteAllText("EngRus.json", json);
        }
        static void menu()
        {
            Console.WriteLine("=============Меню=============");
            Console.WriteLine("1. Переводчик Англо-Русский");
            Console.WriteLine("2. Добавить слово в словарь");
            Console.WriteLine("3. Изменить слово в словаре");
            Console.WriteLine("4. Удалить слово в словаре ");
            Console.WriteLine("5. Искать слово в словаре ");
        }

        static void Main(string[] args)
        {
            EngWord name = new EngWord();
            Console.WriteLine(name.TextWord);
            Console.WriteLine(name.DefinitionWord("имя"));
                       
            
            while (true)
            {
                menu();
                string PushButton = "Нажмите номер для продолжения в меню(1-5):";
                Console.Write(PushButton);
                var input = Console.ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.Clear();
                            TranslatorWord();
                            while (true)
                            {
                                Console.Write("Продолжить перевод? (Y/N): ");
                                var YN = Console.ReadKey();
                                Console.WriteLine();
                                if (YN.Key == ConsoleKey.Y) TranslatorWord(); else break;
                            }
                            Console.Clear();
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            Console.Clear();
                            AddWordTranslator();
                            while (true)
                            {
                                Console.Write("Продолжить добавления перевода? (Y/N): ");
                                var YN = Console.ReadKey();
                                Console.WriteLine();
                                if (YN.Key == ConsoleKey.Y) AddWordTranslator(); else break;
                            }
                            break; 
                        }
                    case ConsoleKey.D3:
                        {
                            Console.Clear();
                            ChangeWordSub();
                            while (true)
                            {
                                Console.Write("Продолжить измения слова? (Y/N): ");
                                var YN = Console.ReadKey();
                                Console.WriteLine();
                                if (YN.Key == ConsoleKey.Y) ChangeWordSub(); else break;
                            }
                            break;
                        }
                    case ConsoleKey.D4:
                        {
                            Console.Clear();
                            DeleteWordSub();
                            while (true)
                            {
                                Console.Write("Продолжить удаления слова? (Y/N): ");
                                var YN = Console.ReadKey();
                                Console.WriteLine();
                                if (YN.Key == ConsoleKey.Y) DeleteWordSub(); else break;
                            }
                            break;
                        }
                    case ConsoleKey.D5: 
                        {
                            Console.Clear();
                            SearchWordSub();
                            while (true)
                            {
                                Console.Write("Продолжить поиска перевода? (Y/N): ");
                                var YN = Console.ReadKey();
                                Console.WriteLine();
                                if (YN.Key == ConsoleKey.Y) SearchWordSub(); else break;
                            }
                            break;
                        }

                    default: break;
                }
                if (input.Key == ConsoleKey.Escape) break; else Console.Clear();

            }

        }
    }
}
