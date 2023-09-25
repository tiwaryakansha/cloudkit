using DataLibrary.Models;

namespace DataLibrary.Helpers
{
    public interface IMenuDetailsHelper
    {
        List<MenuItem> GetMenuDetails();
        List<ItemDetails> GetItemByCateoryId(int categoryId);
    }
}