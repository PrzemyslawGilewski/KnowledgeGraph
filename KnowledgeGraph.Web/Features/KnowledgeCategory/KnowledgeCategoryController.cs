using AutoMapper;
using KnowledgeGraph.Application.Command;
using KnowledgeGraph.Application.Request;
using KnowledgeGraph.Data;
using KnowledgeGraph.Web.Controllers;
using KnowledgeGraph.Web.Features.KnowledgeCategory.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Features.KnowledgeCategory
{
    [Authorize]
    public class KnowledgeCategoryController : UserController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<KnowledgeCategoryController> _localizer;
        private readonly IStringLocalizer<SharedResources> _sharedResources;

        public KnowledgeCategoryController(
            IMediator mediator,
            IMapper mapper,
            IStringLocalizer<KnowledgeCategoryController> localizer,
            IStringLocalizer<SharedResources> sharedResources,
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _localizer = localizer;
            _sharedResources = sharedResources;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetAllKnowledgeCategoriesWithConceptsRequest(GetAuthenticatedUserId(),false));
            var model = _mapper.Map<IEnumerable<ListKnowledgeCategoryViewModel>>(result);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeCategoryWithConceptsByIdRequest(id));
            return View(_mapper.Map<DetailsKnowledgeCategoryViewModel>(result));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateKnowledgeCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var result = await _mediator.Send(new CreateKnowledgeCategoryCommand(model.Name, model.Comment, GetAuthenticatedUserId()));
                if (result.IsSuccess)
                {
                    TempData["Success"] = _localizer["Category has been added."].Value;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeCategoryByIdRequest(id));
            return View(_mapper.Map<EditKnowledgeCategoryViewModel>(result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditKnowledgeCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new UpdateKnowledgeCategoryCommand(model.Id, model.Name, model.Comment, GetAuthenticatedUserId()));

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            else
            {
                TempData["Success"] = _localizer["Category has been modified."].Value;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteKnowledgeCategoryCommand(id));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Category has been deleted."].Value;
            }
            else
            {
                TempData["Error"] = $"{ _sharedResources["Error"]}. { result.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
