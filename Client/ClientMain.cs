using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;

using LemonUI;
using LemonUI.Menus;

using static CitizenFX.Core.Native.API;

namespace QVGDL.Client
{
    public class ClientMain : BaseScript
    {
        private readonly ObjectPool _Pool = new ObjectPool();
        private bool _isPlaying = false;
        private int _Score = 0;
        private int _TotalQuestion = 0;
        private string _Question;
        private List<string> _Replies = new List<string>();
        private int _GoodReply = 0;
        public ClientMain()
        {
            Debug.WriteLine("Hi from QVGDL.Client!");
        }

        [Tick]
        public Task OnTick()
        {
            if (_isPlaying)
            {
                SetTextFont(4);
                SetTextScale(0.3f, 0.3f);
                SetTextProportional(false);
                SetTextColour(211, 0, 0, 255);
                SetTextDropshadow(0, 0, 0, 0, 255);
                SetTextEdge(1, 0, 0, 0, 255);
                SetTextDropShadow();
                SetTextOutline();
                SetTextCentre(false);
                SetTextJustification(0);
                SetTextEntry("STRING");
                AddTextComponentString($"Vous avez un score de {_Score} / 20");
                int x = 0, y = 0;
                GetScreenActiveResolution(ref x, ref y);
                DrawText(0.93f,0.98f);
            }
            _Pool.Process();
            return Task.FromResult(0);
        }
        [Command("qvgdl")]
        public void Start_Menu()
        {
            if (_isPlaying)
            {
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[QVGDL]", $"Vous ne jouez plus!" }
                });
                _isPlaying = false;
            }
            else
            {
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[QVGDL]", $"Que la fête comemnce!" }
                });
                _isPlaying = true;

            }
            TriggerServerEvent("qvgdl:ask_start");
        }
        [EventHandler("qvgdl:respond_start")]
        public void Respond_Start(int nb_question)
        {

        }
        [EventHandler("qvgdl:receive_question")]
        public void ReceiveDatas(string question,string rep1, string rep2, string rep3, string rep4, int bonneRep)
        {
            Debug.WriteLine("Received Receive_Question");
            NativeMenu menu = new NativeMenu("Qui Veux Gagner De L'Argent", question);
            var item1 = new NativeItem("Réponse 1", rep1, "~f~>>>");
            var item2 = new NativeItem("Réponse 2", rep2, "~f~>>>");
            var item3 = new NativeItem("Réponse 3", rep3, "~f~>>>");
            var item4 = new NativeItem("Réponse 4", rep4, "~f~>>>");
            // WHAT A CODEEEEEEEEEEE LOOOOL
            item1.Activated += (sender, e) =>
            {
                if(bonneRep == 0)
                {
                    menu.Visible = false;
                    TriggerServerEvent("qvgdl:client_broad_notif", $"{GetPlayerName(PlayerId())} a gagné!");
                }
            };
            item2.Activated += (sender, e) =>
            {
                if (bonneRep == 1)
                {
                    menu.Visible = false;
                    TriggerServerEvent("qvgdl:client_broad_notif", $"{GetPlayerName(PlayerId())} a gagné!");
                }
            };
            item3.Activated += (sender, e) =>
            {
                if (bonneRep == 2)
                {
                    menu.Visible = false;
                    TriggerServerEvent("qvgdl:client_broad_notif", $"{GetPlayerName(PlayerId())} a gagné!");
                }
            };
            item4.Activated += (sender, e) =>
            {
                if (bonneRep == 3)
                {
                    menu.Visible = false;
                    TriggerServerEvent("qvgdl:client_broad_notif", $"{GetPlayerName(PlayerId())} a gagné!");
                }
            };
            menu.Add(item1);
            menu.Add(item2);
            menu.Add(item3);
            menu.Add(item4);
            _Pool.Add(menu);
            menu.Visible = true;
            //SendNotification($"Le vainqueur est: {GetPlayerName(PlayerId())}");
            //Format Data
            //QUESTION;rep 1; rep2; rep 3; rep 4; int bonne rep(0-3)
        }

        public void SendNotification(string text)
        {
            BeginTextCommandThefeedPost("STRING");
            AddTextComponentString(text);
            EndTextCommandThefeedPostTicker(true, true);
        }
        [EventHandler("qvgdl:send_notif")]
        public void SendNotif_Server(string text)
        {
            SendNotification(text);
        }
    }
    public class ClientGame
    {
        public int TotalQuestion { get; set; } = 0;

    }
}