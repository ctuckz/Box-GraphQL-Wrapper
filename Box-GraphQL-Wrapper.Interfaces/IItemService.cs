using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2.Models;

namespace BoxGraphQLWrapper.Interfaces
{
    public interface IItemService
    {
        Task<List<BoxItem>> GetItems(string parentFolderId);
    }
}
