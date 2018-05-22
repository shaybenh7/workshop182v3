using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.Controllers;

namespace wsep182.Domain
{
    class NotificationManager
    { 
        private static NotificationManager instance;

        private NotificationManager()
        {
        }

        public static NotificationManager getInstance()
        {
            if (instance == null)
                instance = new NotificationManager();
            return instance;
        }

        public Boolean notifyUser(String userName, String message)
        {
            String hash = HashArchive.getInstance().getHashByUserName(userName);
            if (hash != null)
            {
                WebSocketController.sendMessageToClient(hash, message);
                return true;
            }
            LinkedList<String> CurrentPendingMessages;
            WebSocketController.PendingMessages.TryGetValue(userName, out CurrentPendingMessages);
            if(CurrentPendingMessages != null)
            {
                CurrentPendingMessages.AddLast(message);
            }
            else
            {
                CurrentPendingMessages = new LinkedList<String>();
                CurrentPendingMessages.AddLast(message);
                WebSocketController.PendingMessages.Add(userName, CurrentPendingMessages);
            }
            return false;
        }


    }
}
