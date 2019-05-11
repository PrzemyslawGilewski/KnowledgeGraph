using AutoMapper;
using KnowledgeGraph.Application.Command;
using KnowledgeGraph.Application.Request;
using KnowledgeGraph.Data;
using KnowledgeGraph.Web.Controllers;
using KnowledgeGraph.Web.Features.KnowledgeCategory.ViewModels;
using KnowledgeGraph.Web.Features.KnowledgeConcept.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Features.KnowledgeConcept
{
    [Authorize]
    public class KnowledgeConceptController : UserController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<KnowledgeConceptController> _localizer;
        private readonly IStringLocalizer<SharedResources> _sharedResources;

        public KnowledgeConceptController(
            IMediator mediator,
            IMapper mapper,
            IStringLocalizer<KnowledgeConceptController> localizer,
            IStringLocalizer<SharedResources> sharedResources,
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _localizer = localizer;
            _sharedResources = sharedResources;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetAllKnowledgeCategoriesWithConceptsRequest(GetAuthenticatedUserId(),true));

            IndexKnowledgeConceptViewModel model = new IndexKnowledgeConceptViewModel
            {
                Categories = _mapper.Map<IEnumerable<KnowledgeCategoryWithConceptsViewModel>>(result)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeConceptByIdRequest(id));

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                var model = _mapper.Map<DetailsKnowledgeConceptViewModel>(result);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(int categoryId)
        {
            CreateKnowledgeConceptViewModel model = new CreateKnowledgeConceptViewModel
            {
                Categories = await getCategories()
            };

            if (categoryId != 0)
            {
                model.CategoryId = categoryId;
            }

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateKnowledgeConceptViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await getCategories();
                return View(model);
            }

            var result = await _mediator.Send(new CreateKnowledgeConceptCommand(model.Name, model.Comment, model.CategoryId, GetAuthenticatedUserId()));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Concept has been created."].Value;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", result.Message);

                model.Categories = await getCategories();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var knowledgeConceptDto = await _mediator.Send(new GetKnowledgeConceptByIdRequest(id));

            if (knowledgeConceptDto == null)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new GetAllKnowledgeCategoriesRequest(GetAuthenticatedUserId()));

            var model = _mapper.Map<EditKnowledgeConceptViewModel>(knowledgeConceptDto);

            model.Categories = _mapper.Map<IEnumerable<KnowledgeCategoryViewModel>>(result);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditKnowledgeConceptViewModel model)
        {
            var result = await _mediator.Send(new UpdateKnowledgeConceptCommand(model.Id, model.Name, model.Comment, model.CategoryId, GetAuthenticatedUserId()));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Concept has been modified."].Value;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", result.Message);

                model.Categories = await getCategories();
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteKnowledgeConceptCommand(id));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Concept has been deleted."].Value;
            }
            else
            {
                TempData["Error"] = $"{_sharedResources["Error"]}. {result.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<KnowledgeCategoryViewModel>> getCategories()
        {
            var result = await _mediator.Send(new GetAllKnowledgeCategoriesRequest(GetAuthenticatedUserId()));
            return _mapper.Map<IEnumerable<KnowledgeCategoryViewModel>>(result);
        }
    }
}
