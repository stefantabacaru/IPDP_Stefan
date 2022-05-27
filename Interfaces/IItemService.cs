using IPDP_Stefan.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPDP_Stefan.Interfaces
{
    public interface IItemService
    {
        public Task<Item> AddItem(Item item);
        public Task DeleteItem(Item item);
        public Task<Item> EditItem(Item item);
        public Task<Item> GetItemById(int id);
        public Task<Item> GetItemByInventoryNumber(string inventoryNumber);
        public Task<List<Item>> GetAllItems();
    }
}
