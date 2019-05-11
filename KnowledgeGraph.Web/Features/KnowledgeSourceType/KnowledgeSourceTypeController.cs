using AutoMapper;
using KnowledgeGraph.Application.Command;
using KnowledgeGraph.Application.Request;
using KnowledgeGraph.Data;
using KnowledgeGraph.Web.Controllers;
using KnowledgeGraph.Web.Features.KnowledgeSourceType.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Features.KnowledgeSourceType
{
    [Authorize]
    public class KnowledgeSourceTypeController : UserController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<KnowledgeSourceTypeController> _localizer;
        private readonly IStringLocalizer<SharedResources> _sharedResources;

        public KnowledgeSourceTypeController(
            IMediator mediator, 
            IMapper mapper,
            IStringLocalizer<KnowledgeSourceTypeController> localizer,
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
            var result = await _mediator.Send(new GetAllKnowledgeSourceTypesRequest(GetAuthenticatedUserId()));
            return View(_mapper.Map<IEnumerable<ListKnowledgeSourceTypeViewModel>>(result));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeSourceTypeByIdRequest(id));
            return View(_mapper.Map<DetailsKnowledgeSourceTypeViewModel>(result));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateKnowledgeSourceTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            var result = await _mediator.Send(new CreateKnowledgeSourceTypeCommand(model.Name, model.Comment, GetAuthenticatedUserId()));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Source Type has been created."].Value;
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeSourceTypeByIdRequest(id));
            return View(_mapper.Map<EditKnowledgeSourceTypeViewModel>(result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditKnowledgeSourceTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new UpdateKnowledgeSourceTypeCommand(model.Id, model.Name, model.Comment, GetAuthenticatedUserId()));

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            else
            {
                TempData["Success"] = _localizer["Source Type has been modified."].Value;
                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteKnowledgeSourceTypesCommand(id));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Source Type has been deleted."].Value;

            }
            else
            {
                TempData["Error"] = $"{_sharedResources["Error"].Value}. {result.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
