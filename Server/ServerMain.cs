using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace QVGDL.Server
{
    public class ServerMain : BaseScript
    {
        public Dictionary<Player, Jeux> PlayerMap = new Dictionary<Player, Jeux>();
        private Questions Questions;
        public ServerMain()
        {
            Debug.WriteLine("Hi from QVGDL.Server! the server side");
            Questions = new Questions();
        }

        [EventHandler("qvgdl:ask_start")]
        public void StartPlay([FromSource]Player player)
        {
            //Le joueur demande à jouer, on vérifie qu'il n'a jamais joué
            
            Debug.WriteLine("CLIENT_START");
            //Le joueur vient de commencer à jouer
            QuestionType rndQ = Questions.RandomQuestion();
            TriggerClientEvent(player, "qvgdl:receive_question", rndQ.Question, rndQ.Reponses[0], rndQ.Reponses[1], rndQ.Reponses[2], rndQ.Reponses[3], rndQ.BonneReponse);

        }
        [EventHandler("qvgdl:client_broad_notif")]
        public void BroadNotif([FromSource]Player player, string text)
        {
            TriggerClientEvent("qvgdl:send_notif", text);
        }
    }
    public class Jeux
    {
        public int Score { get; set; } = 0;
        public int NombreDeQuestion = 0;
        public int QuestionActuelle = 0;
    }
}