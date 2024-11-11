using LocalPhoton;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu.UI
{
    /// <summary>
    /// Class in charge of creating rooms and populating the room list.
    /// </summary>
    public class RoomCreator : MonoBehaviour
    {
        [SerializeField] private PUNRoomButtonInfo _roomButtonPrefab;
        [SerializeField] private RectTransform _roomListParent;
        private readonly Dictionary<string, RoomInfo> _roomList = new();

        // Singletoning the RoomCreator
        private static RoomCreator _instance;
        public static RoomCreator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<RoomCreator>();
                }

                if (_instance == null)
                {
                    GameObject go = new("RoomCreator");
                    go.AddComponent<RoomCreator>();
                    _instance = go.GetComponent<RoomCreator>();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Populates the room list with the given list of rooms.
        /// </summary>
        /// <param name="roomListInfo"></param>
        public void PopulateRoomList(List<RoomInfo> roomListInfo)
        {
            foreach (RoomInfo room in roomListInfo)
            {
                if (room.RemovedFromList)
                {
                    _roomList.Remove(room.Name);
                }
                else
                {
                    _roomList[room.Name] = room;
                }
            }

            foreach (Transform child in _roomListParent)
            {
                Destroy(child.gameObject);
            }

            foreach (RoomInfo roomInfo in _roomList.Values)
            {
                if (roomInfo.PlayerCount != roomInfo.MaxPlayers)
                {
                    Debug.Log($"*** Room in lobby {roomInfo.Name}");
                    PUNRoomButtonInfo roomButton = Instantiate(_roomButtonPrefab, _roomListParent);
                    roomButton.SetButtonInfo(roomInfo);
                    roomButton.transform.parent = _roomListParent;
                    roomButton.transform.localScale = Vector3.one;
                    roomButton.transform.localRotation = Quaternion.identity;
                }
            }
        }
    }
}