using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using CitizenFX.Core;

using Newtonsoft.Json;
namespace QVGDL.Server
{
    public class Questions
    {
        private readonly string _actualFolder = "resources/[net]/QVGDL/Server";
        private QuestionType[] _quest;
        public Questions()
        {
            var txt = File.ReadAllLines(_actualFolder+"/questions.json");
            Debug.WriteLine($"Lecture du fichier questions.json avec {txt.Length}lignes");
            _quest = JsonConvert.DeserializeObject<QuestionType[]>(string.Join("\n", txt));
            Debug.WriteLine($"question: {_quest.First().Question}");
            //READ FILE

        }
        public QuestionType RandomQuestion()
        {
            Random random = new Random();
            return _quest[random.Next(0, _quest.Length)];
        }
    }
    public class QuestionType
    {
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("reponses")]
        public List<string> Reponses;
        public int BonneReponse { get; set; }
    }
}
