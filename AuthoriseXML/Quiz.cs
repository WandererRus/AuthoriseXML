using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoriseXML
{
    internal class Quiz
    {
        double Score = 0;
        public string Name { get; set; }

        List<string> questions = new List<string>() 
        {   
            "Что такое ООП?", 
            "Сколько бит в байте?", 
            "Вы гениальный программист?", 
            "Сдадите ли вы экзамен?" 
        };

        List<string[]> variantsQuestions = new List<string[]>() 
        { 
            new string[4] { "Лучшее что со мной происходило", "Не признаю ООП", "Не знаю", "Объектно-ориентированное программирование" }, 
            new string[4] { "8", "9", "12", "2" }, 
            new string[4] { "да", "не знаю", "нет", "мама меня все равно любит" }, 
            new string[4] { "да", "нет", "возможно", "да нет, возможно" } 
        };

        List<bool[]> rightAnswers = new List<bool[]>()
        {
            new bool[4] { false,false,false,true },
            new bool[4] { true,false,false,false },
            new bool[4] { true,false,false,true },
            new bool[4] { true,false,false,false }
        };

        List<int> ScoreAnswers = new List<int>()
        {
            12,12,12,12
        };

        public Quiz(string name)
        {
            Name = name;
        }
        
    }
}
