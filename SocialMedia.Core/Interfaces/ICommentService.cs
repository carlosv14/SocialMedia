using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface ICommentService
    {
        ServiceResult<IReadOnlyList<Comment>> GetComments();

        ServiceResult<Comment> GetCommentsById(int id);

        ServiceResult<Comment> CreateComment(Comment entity);

        ServiceResult<IReadOnlyList<Comment>> FilterCommentsByPostId(int postId);
    }
}
