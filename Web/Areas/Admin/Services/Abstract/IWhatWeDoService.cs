using Web.Areas.Admin.ViewModels.WhatWeDo;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IWhatWeDoService
    {
        Task<WhatWeDoIndexVM> GetAsync();
        Task<bool> CreateAsync(WhatWeDoCreateVM model);
        Task<WhatWeDoUpdateVM> GetUpdateModelAsync();
        Task<bool> UpdateAsync(WhatWeDoUpdateVM model);
        Task DeleteAsync();
    }
}
