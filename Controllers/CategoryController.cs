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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly ContextDb _context;
        public CategoryController(ICategoryService categoryService, ContextDb context)
        {
            this.categoryService = categoryService;
            _context = context;
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            var existingCategory = _context.Category.Find(category.Id);
            if (existingCategory != null) return BadRequest("This category already exists.");
            await categoryService.AddCategory(category).ConfigureAwait(false);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + category.Id,
                category);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await categoryService.GetCategoryById(id).ConfigureAwait(false);
            if (category != null)
            {
                await categoryService.DeleteCategory(category).ConfigureAwait(false);
                return Ok();
            }return NotFound();
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditCategory(Category category)
        {
            var existingCategory = await categoryService.GetCategoryById(category.Id).ConfigureAwait(false);
            if (existingCategory != null)
            {
                category.Id= existingCategory.Id;
                await categoryService.EditCategory(category).ConfigureAwait(false);
                return Ok();
            }return NotFound();
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await categoryService.GetCategoryById(id).ConfigureAwait(false);
            if(category!= null)
            {
                return Ok(category);
            }return NotFound($"can not find category with id:{id}");
        }
        [HttpGet]
        [Route("get/byName/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            try
            {
                var category = await categoryService.GetCategoryByName(name).ConfigureAwait(false);
                if (category != null)
                {
                    return Ok(category.Name);
                }
                return NotFound($"can not find category with name:{name}");
            }
            catch(Exception e)
            {
                return NotFound($"can not find category with name:{name}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await categoryService.GetAllCategories().ConfigureAwait(false));
        }

    }
}
