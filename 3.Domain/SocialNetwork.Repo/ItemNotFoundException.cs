using System;

namespace SocialNetwork.Repo
{
    public class ItemNotFoundException:Exception
    {
        public ItemNotFoundException(object itemId, string itemName):base($"{itemName} with Id={itemId} not found."){}        
    }
}