using AutoMapper;
using KnowledgeGraph.Application.Command;
using KnowledgeGraph.Application.Request;
using KnowledgeGraph.Data;
using KnowledgeGraph.Web.Controllers;
using KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels;
using KnowledgeGraph.Web.Features.KnowledgeSourceType.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Features.KnowledgeSource
{
    [Authorize]
    public class KnowledgeSourceController : UserController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly IStringLocalizer<KnowledgeSourceController> _localizer;
        private readonly IStringLocalizer<SharedResources> _sharedResources;

        public KnowledgeSourceController(
            IMediator mediator,
            IMapper mapper,
            IStringLocalizer<KnowledgeSourceController> localizer,
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
            var result = await _mediator.Send(new GetAllKnowledgeSourceRequest(GetAuthenticatedUserId()));
            return View(_mapper.Map<IEnumerable<ListKnowledgeSourceViewModel>>(result));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeSourceByIdRequest(id));
            return View(_mapper.Map<DetailsKnowledgeSourceViewModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int authorId, int sourceTypeId)
        {
            CreateKnowledgeSourceViewModel model = new CreateKnowledgeSourceViewModel
            {
                SourceTypes = await getSourceTypes(),
                Authors = await getAuthors()
            };

            if(authorId != 0)
            {
                model.AuthorIds = new List<int> { authorId };
            }

            if (sourceTypeId != 0)
            {
                model.SourceTypeId = sourceTypeId;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateKnowledgeSourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await getAuthors();
                model.SourceTypes = await getSourceTypes();
                return View(model);
            }
            model.AuthorIds?.Remove(0);
            var result = await _mediator.Send(new CreateKnowledgeSourceCommand(model.Name, model.SourceTypeId, model.AuthorIds, model.Comment, GetAuthenticatedUserId()));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Source has been created."].Value;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                model.Authors = await getAuthors();
                model.SourceTypes = await getSourceTypes();
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeSourceByIdRequest(id));
            var model = _mapper.Map<EditKnowledgeSourceViewModel>(result);
            model.SourceTypes = await getSourceTypes();
            model.Authors = await getAuthors();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditKnowledgeSourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await getAuthors();
                model.SourceTypes = await getSourceTypes();
                return View(model);
            }

            model.AuthorIds?.Remove(0);
            var result = await _mediator.Send(new UpdateKnowledgeSourceCommand(model.Id, model.Name, model.Comment, model.SourceTypeId, model.AuthorIds, GetAuthenticatedUserId()));

            if (!result.IsSuccess)
            {
                model.Authors = await getAuthors();
                model.SourceTypes = await getSourceTypes();
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            else
            {
                TempData["Success"] = _localizer["Source has been modified."].Value;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteKnowledgeSourceCommand(id));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Source has been deleted."].Value;

            }
            else
            {
                TempData["Error"] = $"{_sharedResources["Error"].Value}. {result.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<KnowledgeAuthorForSourceViewModel>> getAuthors()
        {
            var authorsDto = await _mediator.Send(new GetAllSimpleKnowledgeAuthorsRequest(GetAuthenticatedUserId()));
            return _mapper.Map<IEnumerable<KnowledgeAuthorForSourceViewModel>>(authorsDto);
        }

        private async Task<IList<ListKnowledgeSourceTypeViewModel>> getSourceTypes()
        {
            var result = await _mediator.Send(new GetAllKnowledgeSourceTypesRequest(GetAuthenticatedUserId()));
            return _mapper.Map<IEnumerable<ListKnowledgeSourceTypeViewModel>>(result).ToList();
        }
    }
}
