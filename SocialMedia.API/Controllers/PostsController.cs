using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.API.Database;
using SocialMedia.API.Database.Entities;
using SocialMedia.API.Models;

namespace SocialMedia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly SocialMediaDbContext _socialMediaContext;

        public PostsController(SocialMediaDbContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> Get()
        {
            var posts = _socialMediaContext.Posts;
            var returnList = new List<PostDto>();
            foreach (var post in posts)
            {
                returnList.Add(new PostDto
                {
                    PostId = post.Id,
                    Content = post.Content,
                    Date = post.Date
                });
            }

            //return posts.Select(x => new PostDto
            //{
            //    PostId = x.Id,
            //    Content = x.Content,
            //    Date = x.Date
            //});

            return Ok(returnList);
        }

        [HttpGet("{id}")]
        public ActionResult<PostDto> Get(int id)
        {
            //var post = _socialMediaContext
            //    .Posts
            //    .FirstOrDefault(x => x.Id == id);
            foreach (var post in _socialMediaContext.Posts)
            {
                if (post.Id == id)
                {
                    return Ok(new PostDto
                    {
                        PostId = post.Id,
                        Date = post.Date,
                        Content = post.Content
                    });
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Post([FromBody] PostDto post)
        {
            var postEntity = new Post
            {
                Content = post.Content,
                Date = DateTime.Now
            };

            await _socialMediaContext.Posts.AddAsync(postEntity);
            await _socialMediaContext.SaveChangesAsync();
            return Ok(post);
        }

        //posts/1/comments
        [HttpPost("{postId}/comments")]
        public async Task<ActionResult<CommentDto>> Post(int postId, [FromBody] CommentDto comment)
        {
            var post = _socialMediaContext
                .Posts
                .FirstOrDefault(x => x.Id == postId);

            if (post == null)
            {
                return BadRequest($"El post con id {postId} no existe");
            }

            var commentEntity = new Comment
            {
                Content = comment.Content,
                PostId = postId
            };

            await _socialMediaContext.Comments.AddAsync(commentEntity);
            await _socialMediaContext.SaveChangesAsync();

            return Ok(comment);
        }

        //posts/1/comments
        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsForPost(int postId)
        {
            var comments = await _socialMediaContext.Comments.Where(c => c.PostId == postId).ToListAsync();
            return Ok(comments.Select(c => new CommentDto
            {
                Content = c.Content,
                PostId = c.PostId,
                CommentId = c.Id
            }));
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult<bool>> Delete(int postId)
        {
            var commentToDelete = await _socialMediaContext.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            _socialMediaContext.Remove(commentToDelete);
            await _socialMediaContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
