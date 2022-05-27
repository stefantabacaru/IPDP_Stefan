using IPDP_Stefan.Context;
using IPDP_Stefan.Interfaces;
using IPDP_Stefan.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPDP_Stefan.Services
{
    public class ItemService : IItemService
    {
        private Context.ContextDb _context;

        public ItemService(ContextDb context)
        {
            _context = context;
        }
        public async Task<Item> AddItem(Item item)
        {
            _context.Item.Add(item);
            _context.SaveChanges();
            return item;
        }
        public async Task DeleteItem(Item item)
        {
            _context.Item.Remove(item);
            _context.SaveChanges();
        }
        public async Task<Item> EditItem(Item item)
        {
            var existingItem = _context.Item.Find(item.Id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Description = item.Description;
                existingItem.Category = item.Category;
                existingItem.ModifiedAt = item.ModifiedAt;
                existingItem.Location = item.Location;
                existingItem.InventoryNumber = item.InventoryNumber;
                existingItem.CreationDate = item.CreationDate;

                _context.Item.Update(existingItem);
                _context.SaveChanges();
            }
            return item;
        }

        public async Task<Item> GetItemById(int id)
        {
            return _context.Item.SingleOrDefault(x => x.Id == id);
        }
        public async Task<Item> GetItemByInventoryNumber(string inventoryNumber)
        {
            return _context.Item.SingleOrDefault(x => x.InventoryNumber == inventoryNumber);
        }
        public async Task<List<Item>> GetAllItems()
        {
            return _context.Item.ToList();
        }
    }
}
