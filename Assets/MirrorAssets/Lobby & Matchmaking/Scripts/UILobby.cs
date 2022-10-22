using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MirrorBasics
{
    public class UILobby : MonoBehaviour
    {
        public static UILobby instance;
        
        

        [Header("Host Join")]
        [SerializeField] private InputField joinMatchInput;

        [SerializeField] private List<Selectable> lobbySelectables = new List<Selectable>();
         
         
        private bool searching = false;

        [Header("Lobby")]
        [SerializeField] private Transform UIPlayerParent;

        [SerializeField] private GameObject UIPlayerPrefab;
        [SerializeField] private Text matchIDText;
        [SerializeField] public Text playerCountText;
        //[SerializeField] private GameObject beginGameButton;

        private GameObject localPlayerLobbyUI;

        private void Start()
        {
            instance = this;
        }

        public void SetStartButtonActive(bool active)
        {
           // beginGameButton.SetActive(active);
        }

        public void HostPublic()
        {
            lobbySelectables.ForEach(x => x.interactable = false);

            Player.localPlayer.HostGame(true);
        }

        public void HostPrivate()
        {
            lobbySelectables.ForEach(x => x.interactable = false);

            Player.localPlayer.HostGame(false);
        }

        public void HostSuccess(bool success, string matchID)
        {
            if (success)
            {
                 
                UIManager.instance.ActivePanelByID(10);
                if (localPlayerLobbyUI != null) Destroy(localPlayerLobbyUI);
                //localPlayerLobbyUI = SpawnPlayerUIPrefab(Player.localPlayer);
                matchIDText.text = matchID;
            }
            else
            {
                lobbySelectables.ForEach(x => x.interactable = true);
            }
        }

        public void Join()
        {
            lobbySelectables.ForEach(x => x.interactable = false);

            Player.localPlayer.JoinGame(joinMatchInput.text.ToUpper());
        }

        public void JoinSuccess(bool success, string matchID)
        {
            if (success)
            {
                UIManager.instance.ActivePanelByID(10);

                if (localPlayerLobbyUI != null) Destroy(localPlayerLobbyUI);
                //localPlayerLobbyUI = SpawnPlayerUIPrefab(Player.localPlayer);
                matchIDText.text = matchID;
            }
            else
            {
                lobbySelectables.ForEach(x => x.interactable = true);
            }
        }

        public void DisconnectGame()
        {
            if (localPlayerLobbyUI != null) Destroy(localPlayerLobbyUI);
            Player.localPlayer.DisconnectGame();

             
            lobbySelectables.ForEach(x => x.interactable = true);
        }

        public GameObject SpawnPlayerUIPrefab(Player player)
        {
            GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);
            newUIPlayer.GetComponent<UIPlayer>().SetPlayer(player);
            newUIPlayer.transform.SetSiblingIndex(player.playerIndex - 1);

            return newUIPlayer;
        }

        public void BeginGame()
        {
            Player.localPlayer.BeginGame();
        }

        public void SearchGame()
        {
            StartCoroutine(Searching());
        }

        public void CancelSearchGame()
        {
            searching = false;
        }

        public void SearchGameSuccess(bool success, string matchID)
        {
            if (success)
            {
               
                searching = false;
                JoinSuccess(success, matchID);
            }
            else
            { 
                searching = false;
                HostPublic();
            }
        }

        private IEnumerator Searching()
        {
            
            searching = true;

            float searchInterval = 1;
            float currentTime = 1;

            while (searching)
            {
                if (currentTime > 0)
                {
                    currentTime -= Time.deltaTime;
                }
                else
                {
                    currentTime = searchInterval;
                    Player.localPlayer.SearchGame();
                }
                yield return null;
            }
           
        }
    }
}