using System;

namespace AuthService.Users
{
    public class ItemNotFoundException:Exception
    {
        public ItemNotFoundException(object itemId, string itemName):base($"{itemName} with Id={itemId} not found."){}        
    }
}