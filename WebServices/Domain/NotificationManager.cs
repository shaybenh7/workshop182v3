using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServices.Controllers;
using WebServices.DAL;
using WebServices.Domain;

namespace wsep182.Domain
{
    class NotificationManager
    { 
        private static NotificationManager instance;
        private PendingMessagesDB PMDB;
        private NotificationManager()
        {
            PMDB=new PendingMessagesDB(configuration.DB_MODE);
        }

        public static NotificationManager getInstance()
        {
            if (instance == null)
                instance = new NotificationManager();
            return instance;
        }
        public static void restartInstance()
        {
            instance = new NotificationManager();
        }
        public PendingMessagesDB getPendingMessagesDB()
        {
            return PMDB;
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
                PMDB.Add(new Tuple<string, string>(userName, message));
                CurrentPendingMessages.AddLast(message);
            }
            else
            {
                PMDB.Add(new Tuple<string, string>(userName, message));
                CurrentPendingMessages = new LinkedList<String>();
                CurrentPendingMessages.AddLast(message);
                WebSocketController.PendingMessages.Add(userName, CurrentPendingMessages);
            }
            return false;
        }


    }
}
