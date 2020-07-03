using SDSDSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Services
{
    public interface IComment
    {
        void AddComment(Comment comment);

        List<Comment> GetAllComments();

        Task<bool> SaveChangesAsync();
    }
}
