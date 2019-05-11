using AutoMapper;
using KnowledgeGraph.Application.Command;
using KnowledgeGraph.Application.Request;
using KnowledgeGraph.Data;
using KnowledgeGraph.Web.Features.KnowledgeAuthor.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Controllers
{
    [Authorize]
    public class KnowledgeAuthorController : UserController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<KnowledgeAuthorController> _localizer;
        private readonly IStringLocalizer<SharedResources> _sharedResources;

        public KnowledgeAuthorController(
            IMediator mediator, 
            IMapper mapper, 
            IStringLocalizer<KnowledgeAuthorController> localizer,
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
            var result = await _mediator.Send(new GetAllKnowledgeAuthorsRequest(GetAuthenticatedUserId()));

            IndexKnowledgeAuthorViewModel model = new IndexKnowledgeAuthorViewModel
            {
                Authors = _mapper.Map<IEnumerable<KnowledgeAuthorViewModel>>(result)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateKnowledgeAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new CreateKnowledgeAuthorCommand(model.FirstName,model.LastName,model.Comment,GetAuthenticatedUserId()));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Author has been created."].Value;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", $"{_sharedResources["Error"].Value}. {result.Message}");
                return View(model);
            }

           
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeAuthorByIdRequest(id));
            var model = _mapper.Map<EditKnowledgeAuthorViewModel>(result);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditKnowledgeAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _mediator.Send(new UpdateKnowledgeAuthorCommand(model.Id, model.FirstName,model.LastName, model.Comment, GetAuthenticatedUserId()));

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            else
            {
                TempData["Success"] = _localizer["Author has been modified."].Value;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _mediator.Send(new GetKnowledgeAuthorByIdRequest(id));
            return View(_mapper.Map<DetailsKnowledgeAuthorViewModel>(result));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteKnowledgeAuthorCommand(id));

            if (result.IsSuccess)
            {
                TempData["Success"] = _localizer["Author has been deleted."].Value;

            }
            else
            {
                TempData["Error"] = $"{_sharedResources["Error"].Value}. {result.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
