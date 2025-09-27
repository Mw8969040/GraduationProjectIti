using itism.bll.Services.Interface;
using itism.bll.ViewModels;
using itism.dal.Models;
using itism.dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.bll.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Create(CreateCourseVM vm)
        {
            try
            {
                var course = new Course
                {
                    Name = vm.Name,
                    Category = vm.Category!.Value,
                    InstructorId = vm.InstructorId
                };
                unitOfWork.Courses.Add(course);
                var result = unitOfWork.Complete();
                
                // Debug: Check if data was saved
                Console.WriteLine($"Course created successfully. Rows affected: {result}");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error creating course: {ex.Message}");
                throw new Exception($"Error creating course: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            unitOfWork.Courses.Delete(id);
            unitOfWork.Complete();
        }

        public IEnumerable<CourseVM> GetAll()
        {
            var courses = unitOfWork.Courses.GetAllWithInstructor();
            Console.WriteLine($"Retrieved {courses.Count()} courses from database");
            
            return courses.Select(c => new CourseVM
            {
                Id = c.Id,
                Name = c.Name ?? string.Empty,
                Category = c.Category,
                InstructorId = c.InstructorId,
                InstructorName = c.Instructor != null ? c.Instructor.Name : null
            });
        }

        public PageResults<CourseVM> GetAllPaged(string? searchName, Category? category, int page, int pageSize)
        {
            var courses = GetAll();

            if (!string.IsNullOrWhiteSpace(searchName))
                courses = courses.Where(c => c.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));

            if (category.HasValue)
                courses = courses.Where(c => c.Category == category.Value);

            var paged = courses.Skip((page - 1) * pageSize).Take(pageSize);

            return new PageResults<CourseVM>
            {
                Items = paged,
                Page = page,
                PageSize = pageSize,
                TotalCount = courses.Count()
            };
        }

        public UpdateCourseVM? GetForUpdate(int id)
        {
            var c = unitOfWork.Courses.GetById(id);
            if (c == null) return null;
            return new UpdateCourseVM
            {
                Id = c.Id,
                Name = c.Name,
                Category = c.Category,
                InstructorId = c.InstructorId
            };
        }

        public bool IsNameUnique(string name, int? excludeId = null)
        {
            return unitOfWork.Courses.IsNameUnique(name, excludeId);
        }

        public void Update(UpdateCourseVM vm)
        {
            var course = new Course
            {
                Id = vm.Id,
                Name = vm.Name,
                Category = vm.Category!.Value,
                InstructorId = vm.InstructorId
            };
            unitOfWork.Courses.Update(course);
            unitOfWork.Complete();
        }

        public IEnumerable<SelectListItem> GetInstructorsSelectList()
        {
            return unitOfWork.Users.GetUsersSelectList(UserRole.Instructor);
        }
    }
}


