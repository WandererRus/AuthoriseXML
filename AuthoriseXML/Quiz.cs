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


        public double StartQuiz()
        {
            int index = 0;
            foreach (string quest in questions)
            {

                Console.WriteLine(quest);
                int count = 1;
                foreach (string variant in variantsQuestions[index])
                {
                    Console.WriteLine(count + "." + variant);
                    count++;
                }
                bool scoreAdding = false;
                Console.WriteLine("Введите правильный ответ цифрой варианта. Если ответов несколько, то через запятую.");
                string answer = Console.ReadLine();
                if (answer.Contains(','))
                {
                    string[] answers = answer.Split(",");
                    
                    foreach (string ans in answers)
                    {
                        if (rightAnswers[index][Int32.Parse(ans) - 1] == true)
                        {
                            scoreAdding = true;
                        }
                        else
                        {
                            scoreAdding = false;
                            break;
                        }
                    }
                    if (scoreAdding)
                    {
                        Score += ScoreAnswers[index];
                    }

                }
                else
                {
                    if (rightAnswers[index][Int32.Parse(answer) - 1] == true)
                    {
                        scoreAdding = true;
                    }
                    if (scoreAdding)
                    {
                        Score += ScoreAnswers[index];
                    }
                }
                index++;
            }
            return Score;
        }
    }
}
