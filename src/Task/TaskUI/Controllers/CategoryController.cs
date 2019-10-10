using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Core.Interfaces;
using Task.Entites;
using TaskUI.Models.ViewModels;

namespace TaskUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericService<Category> _categorysService;
        private readonly IMapper _mapper;

        public CategoryController(IGenericService<Category> categoryService, IMapper mapper)
        {
            _categorysService = categoryService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var model = _categorysService.GetAll(m => m.IsDeleted == false);
            var vm = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(model);
            return View(vm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }
            var model = _mapper.Map<CategoryViewModel, Category>(categoryViewModel);
            _categorysService.Insert(model);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return View("_NotFound");
            }

            var model = _categorysService.GetById(id);

            if (model is null)
                return View("_NotFound");

            var vm = _mapper.Map<Category, CategoryViewModel>(model);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            var dbModel = _categorysService.GetById(categoryViewModel.Id);
            if (dbModel is null)
                return View("_NotFound");

            _mapper.Map<CategoryViewModel, Category>(categoryViewModel, dbModel);
            _categorysService.Update(dbModel);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = _categorysService.GetById(id);
            if (model is null)
                return View("_NotFound");

            var vm = _mapper.Map<Category, CategoryViewModel>(model);
            return View(vm);
        }

        public ActionResult Delete(int id)
        {
            var model = _categorysService.GetById(id);
            if (model is null)
                return View("_NotFound");

            var vm = _mapper.Map<Category, CategoryViewModel>(model);
            return View(vm);
        }

        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            var category = _categorysService.GetById(id);
            if (category is null)
                return View("_NotFound");

            _categorysService.Delete(category);
            return RedirectToAction("Index");
        }
    }
}