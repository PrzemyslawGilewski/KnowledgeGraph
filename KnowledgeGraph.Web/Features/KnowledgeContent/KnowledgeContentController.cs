using AutoMapper;
using KnowledgeGraph.Application.Command;
using KnowledgeGraph.Application.Request;
using KnowledgeGraph.Data;
using KnowledgeGraph.Web.Controllers;
using KnowledgeGraph.Web.Features.KnowledgeContent.ViewModels;
using KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Features.KnowledgeContent
{
    [Authorize]
    public class KnowledgeContentController : UserController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<KnowledgeContentController> _localizer;
        private readonly IStringLocalizer<SharedResources> _sharedResources;

        public KnowledgeContentController(
            IMediator mediator,
            IMapper mapper,
            IStringLocalizer<KnowledgeContentController> localizer,
            IStringLocalizer<SharedResources> sharedResources,
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _localizer = localizer;
            _sharedResources = sharedResources;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int sourceId)
        {
            if (sourceId != 0)
            {
                var result = await _mediator.Send(new GetAllKnowledgeContentForSourceRequest(sourceId));

                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    var model = _mapper.Map<IndexKnowledgeContentViewModel>(result);
                    return View(model);
                }
            }
            else
            {
                var result = await _mediator.Send(new GetAllKnowledgeContentOutConceptRequest(GetAuthenticatedUserId()));

                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    IndexKnowledgeContentViewModel indexKnowledgeContentViewModel = new IndexKnowledgeContentViewModel
                    {
                        KnowledgeContentViewModels = _mapper.Map<IEnumerable<KnowledgeContentViewModel>>(result).OrderByDescending(kcvm => kcvm.CreationDate)
                    };
                    return View(indexKnowledgeContentViewModel);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id, int sourceId)
        {
            CreateKnowledgeContentViewModel createKnowledgeSectionViewModel;

            if (id == 0)
            {
                createKnowledgeSectionViewModel = new CreateKnowledgeContentViewModel
                {
                    Attached = false,
                    Sources = await getKnowledgeSourceViewModelList(),
                    UserId = GetAuthenticatedUserId(),
                    CategoriesWithConcepts = await getCategoriesWithConceptsSelectList()
                };
            }
            else
            {
                createKnowledgeSectionViewModel = new CreateKnowledgeContentViewModel
                {
                    Attached = true,
                    Sources = await getKnowledgeSourceViewModelList(),
                    UserId = GetAuthenticatedUserId(),
                    ConceptId = id,
                    ConceptName = await getConceptName(id),
                    CategoriesWithConcepts = await getCategoriesWithConceptsSelectList()
                };
            }

            if (sourceId != 0)
            {
                createKnowledgeSectionViewModel.SourceId = sourceId;
            }

            return View(createKnowledgeSectionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateKnowledgeContentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CategoriesWithConcepts = await getCategoriesWithConceptsSelectList();
                return View(model);
            }

            var result = await _mediator.Send(new CreateKnowledgeContentCommand(model.Content, model.Comment, GetAuthenticatedUserId(), model.ConceptId, model.SourceId));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Content has been created."].Value;
                if (model.ConceptId != 0)
                {
                    return RedirectToAction("Details", "KnowledgeConcept", new { id = model.ConceptId });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                model.CategoriesWithConcepts = await getCategoriesWithConceptsSelectList();
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeContentByIdRequest(id));
            if (result == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditKnowledgeContentViewModel>(result);

            var sources = await _mediator.Send(new GetAllKnowledgeSourceRequest(GetAuthenticatedUserId()));

            model.Sources = _mapper.Map<IEnumerable<KnowledgeSourceViewModel>>(sources);
            model.CategoriesWithConcepts = await getCategoriesWithConceptsSelectList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditKnowledgeContentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CategoriesWithConcepts = await getCategoriesWithConceptsSelectList();
                return View(model);
            }

            var result = await _mediator.Send(new UpdateKnowledgeContentCommand(model.Id, model.Content, model.Comment, GetAuthenticatedUserId(), model.ConceptId, model.SourceId));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Content has been updated."].Value;
                if (model.ConceptId != 0)
                {
                    return RedirectToAction("Details", "KnowledgeConcept", new { id = model.ConceptId });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                model.CategoriesWithConcepts = await getCategoriesWithConceptsSelectList();
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var content = await _mediator.Send(new GetKnowledgeContentByIdRequest(id));

            var result = await _mediator.Send(new DeleteKnowledgeContentCommand(id));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Content has been deleted."].Value;
            }
            else
            {
                TempData["Error"] = $"Błąd. {result.Message}";
            }

            if (content.ConceptId != 0)
            {
                return RedirectToAction("Details", "KnowledgeConcept", new { id = content.ConceptId });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task<List<SelectListItem>> getCategoriesWithConceptsSelectList()
        {
            var knowledgeCategoriesWithConceptsDto = await _mediator.Send(new GetAllKnowledgeCategoriesWithConceptsRequest(GetAuthenticatedUserId(), true));

            List<SelectListItem> categoriesWithConceptsSelectList = new List<SelectListItem>();

            foreach (var categoryWithConcepts in knowledgeCategoriesWithConceptsDto)
            {
                var group = new SelectListGroup { Name = categoryWithConcepts.Category == null ? _sharedResources["No Category"] : categoryWithConcepts.Category.Name };

                foreach (var concept in categoryWithConcepts.KnowledgeConcepts)
                {
                    categoriesWithConceptsSelectList.Add(new SelectListItem
                    {
                        Value = concept.Id.ToString(),
                        Text = concept.Name,
                        Group = group
                    });
                }
            }

            return categoriesWithConceptsSelectList;
        }

        private async Task<string> getConceptName(int id)
        {
            var knowledgeConceptDto = await _mediator.Send(new GetKnowledgeConceptByIdRequest(id));
            return knowledgeConceptDto.Name;
        }

        private async Task<IEnumerable<KnowledgeSourceViewModel>> getKnowledgeSourceViewModelList()
        {
            var knowledgeSourcesDto = await _mediator.Send(new GetAllKnowledgeSourceRequest(GetAuthenticatedUserId()));
            return _mapper.Map<IEnumerable<KnowledgeSourceViewModel>>(knowledgeSourcesDto);

        }
    }
}
