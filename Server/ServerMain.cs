using System;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace QVGDL.Server
{
    public class ServerMain : BaseScript
    {
        public ServerMain()
        {
            Debug.WriteLine("Hi from QVGDL.Server!");
        }

        [EventHandler("qvgdl:client_start")]
        public void StartPlay([FromSource]Player player)
        {
            Debug.WriteLine("CLIENT_START");
            //Le joueur vient de commencer à jouer
            TriggerClientEvent(player, "qvgdl:receive_question","Salut! Comment ça va?","Bien", "Bof", "Oui","Non",3);

        }
        [EventHandler("qvgdl:client_broad_notif")]
        public void BroadNotif([FromSource]Player player, string text)
        {
            TriggerClientEvent("qvgdl:send_notif", text);
        }
    }
}