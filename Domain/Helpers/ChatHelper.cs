using System;

namespace Domain.Helpers
{
    public static class ChatHelper
    {
        public static string GetConversationId(string user1, string user2)
        {
            var ids = new[] { user1, user2 };
            Array.Sort(ids);
            return $"{ids[0]}_{ids[1]}";
        }
    }
}
