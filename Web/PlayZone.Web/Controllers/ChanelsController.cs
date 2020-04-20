﻿namespace PlayZone.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PlayZone.Data.Models;
    using PlayZone.Services.Data;
    using PlayZone.Web.ViewModels.Chanels;

    public class ChanelsController : BaseController
    {
        private const int ItemsPerPage = 6;

        private readonly IChanelsService chanelsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ChanelsController(IChanelsService chanelsService, UserManager<ApplicationUser> userManager)
        {
            this.chanelsService = chanelsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ChanelCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.ChanelId != null)
            {
                return this.RedirectToAction("Details", new { Id = user.ChanelId });
            }

            if (!this.ModelState.IsValid || !this.chanelsService.IsValidChanel(input.Title))
            {
                return this.View(input);
            }

            var chanelId = await this.chanelsService.CreateChanelAsync(input.Title, input.Description, user);

            return this.RedirectToAction("Details", new { Id = chanelId });
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = this.chanelsService.GetChanelById<ChanelDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.RedirectToAction("Create");
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (user != null)
            {
                viewModel.IsCreator = this.chanelsService.IsOwner(id, user);
            }

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult ImageUpload()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ImageUpload(IFormFile file, string id)
        {
            if (file == null || id == null || file.Length > 10485760)
            {
                return this.View();
            }

            await this.chanelsService.UploadAsync(file, id);

            return this.RedirectToAction("Details", new { Id = id });
        }

        public IActionResult Description(string id)
        {
            var viewModel = this.chanelsService.GetChanelById<ChanelDescriptionViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Videos(string id, int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }

            var viewModel = new AllVideosByChanelViewModel();
            viewModel.Chanel = this.chanelsService.GetChanelById<ChanelViewModel>(id);
            viewModel.Videos = this.chanelsService.GetVieosByChanel<VideoByChanelViewModel>(id, ItemsPerPage, (page - 1) * ItemsPerPage);
            var count = this.chanelsService.GetAllVideosByChanelCount(id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var viewModel = this.chanelsService.GetChanelById<ChanelEditInputModel>(id);
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(ChanelEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            //this.chanelsService.IsValidChanel(input.Title);

            await this.chanelsService.EditChanelAsync(input.Id, input.Title, input.Description);

            return this.RedirectToAction("Details", new { Id = input.Id });
        }
    }
}
