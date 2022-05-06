using Blog.Data;
using Blog.Models.Dto;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly BlogDbContext dbContext;

        public PostsController(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await dbContext.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetPostById")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post != null)
            {
                return Ok(post);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            //convert DTO to Entity
            var post = new Post()
            {
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                Author = addPostRequest.Author,
                FeatureImageUrl = addPostRequest.FeatureImageUrl,
                Summary = addPostRequest.Summary,
                UpdatedDate = addPostRequest.UpdatedDate,
                Visible = addPostRequest.Visible,
                UrlHandle = addPostRequest.UrlHandle,
                PushlishDate = addPostRequest.PushlishDate
            };

            post.Id = Guid.NewGuid();

            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest updatePostRequest)
        {
            var existingPost = await dbContext.Posts.FindAsync(id);
            if (existingPost != null)
            {
                existingPost.Title = updatePostRequest.Title;
                existingPost.Content = updatePostRequest.Content;
                existingPost.Author = updatePostRequest.Author;
                existingPost.FeatureImageUrl = updatePostRequest.FeatureImageUrl;
                existingPost.Summary = updatePostRequest.Summary;
                existingPost.UpdatedDate = updatePostRequest.UpdatedDate;
                existingPost.Visible = updatePostRequest.Visible;
                existingPost.UrlHandle = updatePostRequest.UrlHandle;
                existingPost.PushlishDate = updatePostRequest.PushlishDate;

                await dbContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existingPost = await dbContext.Posts.FindAsync(id);
            if (existingPost != null)
            {
                dbContext.Remove(existingPost);
                await dbContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }
    }
}