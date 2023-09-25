using DataLibrary.Context;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Helpers
{
    public class MenuDetailsHelper : IMenuDetailsHelper
    {
        private readonly ApplicationContext _dbContext;

        public MenuDetailsHelper(ApplicationContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<MenuItem> GetMenuDetails()
        {
            return _dbContext.MenuItems.ToList();
        }
        
        public List<ItemDetails> GetItemByCateoryId(int categoryId)
        {
            var item = _dbContext.Items.Where(x => x.CategoryId == categoryId).Select(x => new ItemDetails
            {
                Id = x.Id,
                Name = x.Name,
                ShelfLifeInDays = x.ShelfLifeInDays,
                Description = x.Description
            }).ToList();

            return item;
        }


    }
}
