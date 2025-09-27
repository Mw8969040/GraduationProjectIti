using itism.bll.ViewModels;
using itism.dal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.bll.Services.Interface
{
    public interface ICourseService
    {
        IEnumerable<CourseVM> GetAll();
        PageResults<CourseVM> GetAllPaged(string? searchName, Category? category, int page, int pageSize);
        void Create(CreateCourseVM vm);
        void Update(UpdateCourseVM vm);
        void Delete(int id);
        UpdateCourseVM? GetForUpdate(int id);
        bool IsNameUnique(string name, int? excludeId = null);
        IEnumerable<SelectListItem> GetInstructorsSelectList();
    }
}


