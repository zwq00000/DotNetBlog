﻿using DotNetBlog.Core.Model.Page;
using DotNetBlog.Core.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBlog.Web.Areas.Api.Controllers
{
    [Area("Api")]
    [Route("api/page")]
    public class PageController : ControllerBase
    {
        private PageService PageService { get; set; }

        public PageController(PageService pageService)
        {
            this.PageService = pageService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var pageList = await this.PageService.Query();

            return this.Success(pageList);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddPageModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return this.InvalidRequest();
            }

            var result = await this.PageService.Add(model);

            if (result.Success)
            {
                return this.Success(result.Data);
            }
            else
            {
                return this.Error(result.ErrorMessage);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var pageModel = await this.PageService.Get(id);

            return this.Success(pageModel);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]EditPageModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return this.InvalidRequest();
            }

            model.ID = id;

            var result = await this.PageService.Edit(model);

            if (result.Success)
            {
                return this.Success(result.Data);
            }
            else
            {
                return this.Error(result.ErrorMessage);
            }
        }
    }
}
