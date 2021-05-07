using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia.API.Models;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enum;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostsController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> Get()
        {
            var posts = _postService.GetPosts()
                .Result
                .Select(x => new PostDto
                {
                    Content = x.Content,
                    Date = x.Date,
                    PostId = x.Id
                });
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult<PostDto> Get(int id)
        {
            var postResult = _postService.GetPostsById(id);
            if (postResult.ResponseCode == ResponseCode.NotFound)
            {
                return NotFound(postResult.Error);
            }

            return Ok(new PostDto
            {
                Content = postResult.Result.Content,
                Date = postResult.Result.Date,
                PostId = postResult.Result.Id
            });
        }

        [HttpPost]
        public ActionResult<PostDto> Post([FromBody] PostDto post)
        {
            var postEntity = new Post
            {
                Content = post.Content,
                Date = DateTime.Now
            };

            var result = _postService.CreatePost(postEntity);

            if (result.ResponseCode == ResponseCode.Error)
            {
                return BadRequest(result.Error);
            }

            return Ok(post);
        }

        //posts/1/comments
        [HttpPost("{postId}/comments")]
        public ActionResult<CommentDto> Post(int postId, [FromBody] CommentDto comment)
        {
            var commentEntity = new Comment
            {
                Content = comment.Content,
                PostId = postId
            };

            var result = _commentService.CreateComment(commentEntity);

            if (result.ResponseCode == ResponseCode.NotFound)
            {
                return NotFound(result.Error);
            }
            
            return Ok(comment);
        }

        //posts/1/comments
        [HttpGet("{postId}/comments")]
        public ActionResult<IEnumerable<CommentDto>> GetCommentsForPost(int postId)
        {
            var result = _commentService.FilterCommentsByPostId(postId);
            if (result.ResponseCode == ResponseCode.NotFound)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Result.Select(c => new CommentDto
            {
                Content = c.Content,
                PostId = c.PostId,
                CommentId = c.Id
            }));
        }
    }
}
