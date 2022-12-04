using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly ItemDBContext itemdbContext;

        public ItemsController(ItemDBContext itemdbContext)
        {
            this.itemdbContext = itemdbContext;

        }

        [HttpGet]
        [Route("/api/Items/AllItems")]
        public async Task<IActionResult> GetAllItems()
        {
            return Ok(await itemdbContext.Items.ToListAsync());
        }

        [HttpGet]
        [Route("/api/Items/GetItemById/{id:Guid}")]
        [ActionName("GetItemInfo")]
        public async Task<IActionResult> GetItemInfo([FromRoute] Guid id)
        {
            var item = await itemdbContext.Items.FindAsync(id);
            if (item == null)   
            return NotFound();
            return Ok(item);
        }


        [HttpPost]
        [Route("/api/Items/AddItem")]
        public async Task<IActionResult> AddItem(Items item)
        {
            item.ItemID = Guid.NewGuid();
           
            await itemdbContext.Items.AddAsync(item);
            await itemdbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemInfo),new {id = item.ItemID}, item);

        }

        [HttpPut]
        [Route("/api/Items/UpdateItem/{id:Guid}")]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid id, [FromBody] Items updateItem)
        {
  
            var existingItem = await itemdbContext.Items.FindAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItem.Name;
            existingItem.Quantity = updateItem.Quantity;
            existingItem.Price = updateItem.Price;

            await itemdbContext.SaveChangesAsync();
            return Ok(existingItem);

        }

        [HttpDelete]
        [Route("/api/Items/DeleteItem/{id:Guid}")]

        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            var existingItem = await itemdbContext.Items.FindAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            itemdbContext.Items.Remove(existingItem);
            await itemdbContext.SaveChangesAsync();
            return Ok();
        }


    }

}
