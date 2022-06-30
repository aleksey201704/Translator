using System;
using System.Text;

namespace Translator
{
    class Program
    {
        const string RUSWord = "йцукенгшщзхъфывапролджэячсмитьёбюъЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮЁ";
        const string ENGWord = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";

        static void Main(string[] args)
        {
            int RuCount=0;
            int EngCount = 0;
            
            
            Console.Write("Введите перевод: ");
            string TranslatorWord = Console.ReadLine();
            for (int i = 0; i < TranslatorWord.Length; i++)
            {
                for (int j = 0; j < RUSWord.Length; j++)
                {
                    if (TranslatorWord[i] == RUSWord[j]) RuCount++;
                }
                for (int j = 0; j < ENGWord.Length; j++) if (TranslatorWord[i] == ENGWord[j]) EngCount++;
            }
            
                // все буквы определяем какое слово
                if (RuCount == TranslatorWord.Length && TranslatorWord.Length != 0)
                { 

                    
                } 
                
            //if (EngCount == TranslatorWord.Length && TranslatorWord.Length != 0) break;

            
           
            


        }
    }
}
