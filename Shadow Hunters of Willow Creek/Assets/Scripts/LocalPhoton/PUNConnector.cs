using System;
using System.Collections.Generic;
using System.Text;
using CharacterSelector.UI;
using MainMenu.UI;
using MainMenu.UI.Settings;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocalPhoton
{
    /*
     * MonobehaviourPunCallbacks is a class that implements all the callbacks that we can use to interact with Photon
     * https://doc-api.photonengine.com/en/PUN/current/class_photon_1_1_pun_1_1_mono_behaviour_pun_callbacks.html
     * 
     * It contains all the methods that we can override to interact with Photon
     */

    /// <summary>
    /// Class in charge of connecting to Photon
    /// </summary>
    public class PUNConnector : MonoBehaviourPunCallbacks
    {
        public override void OnEnable()
        {
            // UI manager events
            UIManager.Instance.OnCreateRoomEvent += CreateRoom;
            UIManager.Instance.OnLeaveRoomEvent += LeaveRoom;
            UIManager.Instance.OnSubmitNicknameEvent += SubmitNickname;

            // PunRoomButtonInfo events
            PUNRoomButtonInfo.OnJoinRoomEvent += JoinRoom;

            DontDestroyOnLoad(gameObject);

            // Call the base OnEnable method. It's important to call the base method because it initializes the Photon callbacks
            base.OnEnable();
        }

        public override void OnDisable()
        {
            UIManager.Instance.OnCreateRoomEvent -= CreateRoom;
            UIManager.Instance.OnLeaveRoomEvent -= LeaveRoom;
            UIManager.Instance.OnSubmitNicknameEvent -= SubmitNickname;

            PUNRoomButtonInfo.OnJoinRoomEvent -= JoinRoom;

            // Call the base OnDisable method. It's important to call the base method because it de-initializes the Photon callbacks
            base.OnDisable();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            UIManager.Instance.CanvasSetup();
            UIManager.Instance.SetLoadingCanvasGroup(true, "Connecting to Photon network...");

            /*
             * ConnectUsingSettings is a method that connects to the Photon server using the settings that we have set
             * in the PhotonServerSettings; this exists in the Menu Window -> Photon Unity Networking -> PUN Wizard
             */

            if (!PhotonNetwork.IsConnected)
            {
                Debug.Log($"*** PUNConnector: Connecting to Photon netowrk...");
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        /*
         * Remember that Photon connects to the PUN network, then to the lobbies that contain rooms, each room is a
         * different game session that we can join to play.
         * 
         * PUN Network > Lobbies > Rooms
         */

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene = true;

            Debug.LogFormat($"*** PUNConnector: Connected to the Photon network!");
            UIManager.Instance.SetLoadingCanvasGroup(true, "Connected to game server! Joining lobby...");
            Debug.LogFormat($"*** PUNConnector: Joining lobby...");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.LogFormat($"*** PUNConnector: Joined lobby!");
            string playerName = PlayerData.LoadPlayerData();
            if (!String.IsNullOrEmpty(playerName))
            {
                PhotonNetwork.NickName = playerName;
                UIManager.Instance.SetLoadingCanvasGroup(false);
                UIManager.Instance.SetMainMenuCanvasGroup(true);
            }
            else
            {
                Debug.Log("No player data found!");
                UIManager.Instance.SetLoadingCanvasGroup(false);
                UIManager.Instance.SetPlayerNameCanvasGroup(true);
            }
        }

        /// <summary>
        /// Submit the player nickname
        /// </summary>
        /// <param name="nickname"></param>
        public void SubmitNickname(string nickname)
        {
            if (String.IsNullOrEmpty(nickname))
            {
                Debug.LogFormat($"*** PUNConnector: Nickname is empty!");
                UIManager.Instance.SetErrorCanvasGroup(true, "Nickname is empty!");
                return;
            }

            PlayerData.SavePlayerData(nickname);

            PhotonNetwork.NickName = nickname;
            Debug.LogFormat($"*** PUNConnector: Player nickname set to {PhotonNetwork.NickName}!");

            UIManager.Instance.SetPlayerNameCanvasGroup(false);
            UIManager.Instance.SetMainMenuCanvasGroup(true);
        }

        /// <summary>
        /// Used to create a room when the player clicks the "Create Room" button, Action is created in the UIManager
        /// and we subscribe to it in the OnEnable method
        /// </summary>
        /// <param name="roomName"></param>
        private void CreateRoom(string roomName)
        {
            Debug.LogFormat($"*** PUNConnector: Creating room {roomName}...");
            UIManager.Instance.SetLoadingCanvasGroup(true, "Creating room...");

            RoomOptions roomOptions = new()
            {
                IsOpen = true,
                IsVisible = true,
                MaxPlayers = 4,
                BroadcastPropsChangeToAll = true
            };
            PhotonNetwork.CreateRoom(roomName, roomOptions);

            //UIManager.Instance.ClearRoomNameInputField();
        }

        public override void OnJoinedRoom()
        {
            Debug.LogFormat($"*** PUNConnector: Joined room {PhotonNetwork.CurrentRoom.Name}!");

            SceneManager.LoadSceneAsync("Character Select");

            PlayerCreator.Instance.CreatePlayersInRoom(PhotonNetwork.PlayerList);

            // If the player is the master client, show the start game button
            if (PhotonNetwork.IsMasterClient)
            {
                UIManagerCharacterSelector.Instance.ButtonStartGame.SetActive(true);
            }
            else
            {
                UIManagerCharacterSelector.Instance.ButtonStartGame.SetActive(false);
            }
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            Debug.LogFormat($"*** PUNConnector: Player {newPlayer.NickName} entered the room!");
            PlayerCreator.Instance.AddPlayer(newPlayer);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            Debug.LogFormat($"*** PUNConnector: Player [{otherPlayer.NickName}] left the room!");
            PlayerCreator.Instance.RemovePlayer(otherPlayer);
        }

        public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
        {
            Debug.LogFormat($"*** PUNConnector: Player [{newMasterClient.NickName}] is the new master client!");
            if (PhotonNetwork.IsMasterClient)
            {
                UIManagerCharacterSelector.Instance.ButtonStartGame.SetActive(true);
            }
            else
            {
                UIManagerCharacterSelector.Instance.ButtonStartGame.SetActive(false);
            }
        }

        /// <summary>
        /// Used to leave a room when the player clicks the "Leave Room" button, Action is created in the UIManager
        /// </summary>
        private void LeaveRoom()
        {
            /*
             * When leaving a room, the LoadBalancingClient will disconnect from the Game Server and return to the Master Server.
             * This wraps up multiple internal actions
             * 
             * https://doc-api.photoengine.com/en/PUN/current/class_photon_1_pun_1_1_mono_behaviour_pun_callbacks.html#a5109a4e0cc11ef64fe8f22370abe5cb9
             */

            Debug.LogFormat($"*** PUNConnector: Leaving room [{PhotonNetwork.CurrentRoom.Name}]...");
            UIManager.Instance.SetLoadingCanvasGroup(true, "Leaving room...");
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            UIManager.Instance.SetCreateRoomCanvasGroup(false);
            UIManager.Instance.SetLoadingCanvasGroup(false);
            UIManager.Instance.SetMainMenuCanvasGroup(true);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            // https://medium.com/@chandrashekharsingh25/string-vs-stringbuilder-in-c-choosing-the-right-tool-for-efficient-string-manipulation-6beca8ca6450
            Debug.LogFormat($"*** PUNConnector: Create room failed! [{message}]");

            StringBuilder sb = new(" Failed to create room " + message);

            UIManager.Instance.SetLoadingCanvasGroup(false);
            UIManager.Instance.SetErrorCanvasGroup(true, sb.ToString());
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            /*
             * This function is called in the Background every time there's a change in the rooms info
             * Initially it returns all the rooms in the lobby, then it send the ones that have changed
             */
            Debug.LogFormat("*** PUNConnector: Room list updated!");
            RoomCreator.Instance.PopulateRoomList(roomList);

            foreach (RoomInfo roomInfo in roomList)
            {
                Debug.LogFormat($"*** PUNConnector: Room [{roomInfo.Name}]");
            }
        }

        /// <summary>
        /// Event to be called when the user clicks the "Join Room" button
        /// </summary>
        /// <param name="roomInfo"></param>
        private void JoinRoom(RoomInfo roomInfo)
        {
            Debug.LogFormat($"*** PUNConnector: Joining room [{roomInfo.Name}]...");
            UIManager.Instance.SetLoadingCanvasGroup(true, "Joining room...");
            PhotonNetwork.JoinRoom(roomInfo.Name);
            SceneManager.LoadScene("Character Select");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogFormat($"*** PUNConnector: Join room failed! [{message}]");

            StringBuilder sb = new(" Failed to join room " + message);

            UIManager.Instance.SetLoadingCanvasGroup(false);
            UIManager.Instance.SetErrorCanvasGroup(true, sb.ToString());
        }

        /// <summary>
        /// Quit the game
        /// </summary>
        public void QuitGame()
        {
            /*
             * This is a way to quit the game, it's different in the editor and in the build thanks to the preprocessor conditional compilation
             * 
             * https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
             */

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}