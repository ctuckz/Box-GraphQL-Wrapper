using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxGraphQLWrapper.Interfaces
{
    public interface IItemContentService
    {
        Task<string> GetItemDownloadUri(string itemId);
    }
}
