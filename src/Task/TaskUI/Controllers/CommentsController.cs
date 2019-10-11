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
    public class CommentsController : Controller
    {
        private readonly IGenericService<Comment> _commentsService;
        private readonly IMapper _mapper;

        public CommentsController(IGenericService<Comment> commentService, IMapper mapper)
        {
            _commentsService = commentService;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult Create(CommentViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {Message = "Please add comment" });
            }
            var model = _mapper.Map<CommentViewModel, Comment>(commentViewModel);
            _commentsService.Insert(model);
            Response.StatusCode = Convert.ToInt32(System.Net.HttpStatusCode.Created);
            return Json(commentViewModel);
        }

    }
}