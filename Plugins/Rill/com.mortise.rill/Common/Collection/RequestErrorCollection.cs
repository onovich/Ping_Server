using System.Collections.Generic;

namespace MortiseFrame.Rill {

    public static class RequestErrorCollection {

        public static readonly Dictionary<int, string> ErrorMessages = new Dictionary<int, string> {
            {-1, "Socket Error Occurred."},
            {0, "Operation Completed Successfully."},
            {995, "Operation Aborted."},
            {997, "IO Operation Pending."},
            {10004, "Operation Interrupted."},
            {10013, "Access Denied."},
            {10014, "Bad Address."},
            {10022, "Invalid Argument."},
            {10024, "Too Many Open Sockets."},
            {10035, "Operation Would Block."},
            {10036, "Operation Now In Progress."},
            {10037, "Operation Already In Progress."},
            {10038, "Socket Operation On Non-Socket."},
            {10039, "Destination Address Required."},
            {10040, "Message Too Long."},
            {10041, "Protocol Wrong Type For Socket."},
            {10042, "Bad Protocol Option."},
            {10043, "Protocol Not Supported."},
            {10044, "Socket Type Not Supported."},
            {10045, "Operation Not Supported."},
            {10046, "Protocol Family Not Supported."},
            {10047, "Address Family Not Supported By Protocol Family."},
            {10048, "Address Already In Use."},
            {10049, "Cannot Assign Requested Address."},
            {10050, "Network Is Down."},
            {10051, "Network Is Unreachable."},
            {10052, "Network Dropped Connection On Reset."},
            {10053, "Software Caused Connection Abort."},
            {10054, "Connection Reset By Peer."},
            {10055, "No Buffer Space Available."},
            {10056, "Socket Is Already Connected."},
            {10057, "Socket Is Not Connected."},
            {10058, "Cannot Send After Socket Shutdown."},
            {10060, "Connection Timed Out."},
            {10061, "Connection Refused."},
            {10064, "Host Is Down."},
            {10065, "No Route To Host."},
            {10067, "Too Many Processes."},
            {10091, "Network Subsystem Is Unavailable."},
            {10092, "WINSOCK.DLL Version Out Of Range."},
            {10093, "Successful WSAStartup Not Yet Performed."},
            {10101, "Graceful Shutdown In Progress."},
            {10109, "Class Type Not Found."},
            {11001, "Host Not Found."},
            {11002, "Try Again."},
            {11003, "No Recovery Possible."},
            {11004, "No Data Record Of Requested Type."}
        };

    }

}