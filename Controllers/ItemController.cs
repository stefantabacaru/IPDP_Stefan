using IPDP_Stefan.Context;
using IPDP_Stefan.Interfaces;
using IPDP_Stefan.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IPDP_Stefan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService itemService;
        private readonly ContextDb _context;
        public ItemController(IItemService itemService, ContextDb context)
        {
            this.itemService = itemService;
            _context = context;
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> AddItem(Item item)
        {
            var existingItem = _context.Item.Find(item.Id);
            if (existingItem != null) return BadRequest("This Item already exists.");
            await itemService.AddItem(item).ConfigureAwait(false);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + item.Id,
                item);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await itemService.GetItemById(id).ConfigureAwait(false);
            if (item != null)
            {
                await itemService.DeleteItem(item).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditItem(Item item)
        {
            var existingItem = await itemService.GetItemById(item.Id).ConfigureAwait(false);
            if (existingItem != null)
            {
                item.Id = existingItem.Id;
                await itemService.EditItem(item).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await itemService.GetItemById(id).ConfigureAwait(false);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound($"can not find Item with id:{id}");
        }
        [HttpGet]
        [Route("get/byInventoryNumber/{inventoryNumber}")]
        public async Task<IActionResult> GetItemByInventoryNumber(string inventoryNumber)
        {
            try
            {
                var item = await itemService.GetItemByInventoryNumber(inventoryNumber).ConfigureAwait(false);
                if (item != null)
                {
                    return Ok(item.InventoryNumber);
                }
                return NotFound($"can not find Item with name:{inventoryNumber}");
            }
            catch (Exception e)
            {
                return NotFound($"can not find Item with name:{inventoryNumber}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllItems()
        {
            return Ok(await itemService.GetAllItems().ConfigureAwait(false));
        }

    }
}
