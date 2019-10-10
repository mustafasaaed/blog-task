using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Core.Interfaces;
using Task.Entites;
using TaskUI.Helpers;
using TaskUI.Models.ViewModels;

namespace TaskUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        private readonly IGenericService<Post> _postsService;
        private readonly IGenericService<Category> _categoryService;
        private readonly IMapper _mapper;

        public PostsController(IGenericService<Post> postService,
            IGenericService<Category> categoryService,
            IMapper mapper)
        {
            _postsService = postService;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = _postsService.GetAll(m => m.IsDeleted == false,null, "Category");
            var vm = _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(model);
            return View(vm);
        }

        public ActionResult Create()
        {
            var categories = _categoryService.GetAll(c => c.IsDeleted == false);
            var vm = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);
            ViewBag.Categories = vm;
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(postViewModel);
            }

            if (postViewModel.CategoryId == 0)
            {
                ModelState.AddModelError("Category", "Please Choose category");
                return View(postViewModel);
            }

            var category = _categoryService.GetById(postViewModel.CategoryId);
            var model = _mapper.Map<PostViewModel, Post>(postViewModel);

            string path = Server.MapPath("~/Photos/");
            var photo = ImagesHelper.GetPhotosUrl(postViewModel.Photo, path);
            model.ImageUrl = photo;
            model.Category = category;
            _postsService.Insert(model);
            return RedirectToAction("Index");
        }



        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return View("_NotFound");
            }

            var model = _postsService.GetById(id);

            if (model is null)
                return View("_NotFound");

            var categories = _categoryService.GetAll(c => c.IsDeleted == false);
            var categoryViewModels = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);
            ViewBag.Categories = categoryViewModels;

            var vm = _mapper.Map<Post, PostViewModel>(model);
            vm.ImageUrl = model.ImageUrl;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(postViewModel);
            }

            var dbModel = _postsService.GetById(postViewModel.Id);
            if (dbModel is null)
                return View("_NotFound");


            if (postViewModel.CategoryId == 0)
            {
                ModelState.AddModelError("Category", "Please Choose category");
                return View(postViewModel);
            }

            var category = _categoryService.GetById(postViewModel.CategoryId);

            _mapper.Map<PostViewModel, Post>(postViewModel, dbModel);
            string path = Server.MapPath("~/Photos/");
            var photo = ImagesHelper.GetPhotosUrl(postViewModel.Photo, path);
            dbModel.ImageUrl = photo;
            dbModel.Category = category;
            _postsService.Update(dbModel);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = _postsService.GetById(id);
            if (model is null)
                return View("_NotFound");

            var vm = _mapper.Map<Post, PostViewModel>(model);
            vm.ImageUrl = model.ImageUrl;
            return View(vm);
        }

        public ActionResult Delete(int id)
        {
            var model = _postsService.GetById(id);
            if (model is null)
                return View("_NotFound");

            var vm = _mapper.Map<Post, PostViewModel>(model);
            vm.ImageUrl = model.ImageUrl;
            return View(vm);
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var post = _postsService.GetById(id);
            if (post is null)
                return View("_NotFound");

            _postsService.Delete(post);
            return RedirectToAction("Index");
        }

    }
}