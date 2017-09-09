using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2.Models;

namespace BoxGraphQLWrapper.Interfaces
{
    public interface IFolderService
    {
        Task<BoxFolder> GetFolderById(string id);
    }
}
