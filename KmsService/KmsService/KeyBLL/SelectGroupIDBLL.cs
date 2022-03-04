using KmsService.DAL;

namespace KmsService.KeyBLL
{
    public class SelectGroupIDBLL
    {
        public string SelectGroupID(string userID)
        {
            UserGroupDAL userGroup = new UserGroupDAL();
            return userGroup.SelectChatID(userID);
        }
    }
}