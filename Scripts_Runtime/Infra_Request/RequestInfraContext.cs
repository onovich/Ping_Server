using System;
using MortiseFrame.Rill;

namespace Ping.Server.Requests {

    public class RequestInfraContext {

        ServerCore serverCore;
        public ServerCore ServerCore => serverCore;

        SortedList<byte, string> userNames;
        SortedList<byte, byte> userStatus;

        public RequestInfraContext() {
            serverCore = new ServerCore();
            userNames = new SortedList<byte, string>();
            userStatus = new SortedList<byte, byte>(2);
            userStatus.Add(1, 0);
            userStatus.Add(2, 0);
        }

        public void AddUserName(byte index, string name) {
            userNames.Add(index, name);
        }

        public string GetUserName(byte index) {
            return userNames[index];
        }

        public void UserStatus_SetJoinReady(byte index) {
            userStatus[index] |= 1;
        }

        public void UserStatus_SetStartReady(byte index) {
            userStatus[index] |= 2;
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
        }

    }

}