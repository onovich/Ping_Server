using System;
using MortiseFrame.Rill;

namespace Ping.Server.Requests {

    public class RequestInfraContext {

        ServerCore serverCore;
        public ServerCore ServerCore => serverCore;

        SortedList<byte, string> userNames;
        SortedList<byte, byte> userStatus;

        string[] userNamesArray;
        public string[] UserNamesArray => userNamesArray;

        byte[] userStatusArray;
        public byte[] UserStatusArray => userStatusArray;

        public RequestInfraContext() {
            serverCore = new ServerCore();
            userNames = new SortedList<byte, string>();
            userStatus = new SortedList<byte, byte>(2);
            userStatus.Add(0, 0);
            userStatus.Add(1, 0);
        }

        public void AddUserName(byte index, string name) {
            userNames.Add(index, name);
            userNamesArray = userNames.Values.ToArray();
        }

        public void UserStatus_SetJoinReady(byte index) {
            userStatus[index] |= 1;
            userStatusArray = userStatus.Values.ToArray();
        }

        public void UserStatus_SetStartReady(byte index) {
            userStatus[index] |= 2;
            userStatusArray = userStatus.Values.ToArray();
        }

        public bool UserStatus_IsJoinReady(byte index) {
            return (userStatus[index] & 1) == 1;
        }

        public bool UserStatus_IsStartReady(byte index) {
            return (userStatus[index] & 2) == 2;
        }

        public void Clear() {
            userNames.Clear();
            userStatus.Clear();
            Array.Clear(userNamesArray, 0, userNamesArray.Length);
            Array.Clear(userStatusArray, 0, userStatusArray.Length);
        }

    }

}