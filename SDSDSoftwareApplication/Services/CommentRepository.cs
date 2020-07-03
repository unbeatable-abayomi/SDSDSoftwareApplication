using SDSDSoftwareApplication.Data;
using SDSDSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.Services
{
    public class CommentRepository : IComment
    {
        private readonly ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }


        public void AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public List<Comment> GetAllComments()
        {
            return context.Comments.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await context.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
