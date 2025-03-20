using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Comment> CreateAsync(Comment commentRequest)
        {
            await _context.Comments.AddAsync(commentRequest);
            await _context.SaveChangesAsync();

            return commentRequest;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentStorage = await _context.Comments.FindAsync(id);
            if (commentStorage == null) 
            {
                return null;
            }

            _context.Comments.Remove(commentStorage);
            await _context.SaveChangesAsync();

            return commentStorage;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(el => el.User).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(el => el.User).FirstOrDefaultAsync(el => el.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentRequest)
        {
            var commentStorage = await _context.Comments.FindAsync(id);
            if (commentStorage == null) 
            {
                return null;
            }

            commentStorage.Title = commentRequest.Title;
            commentStorage.Content = commentRequest.Content;

            await _context.SaveChangesAsync();

            return commentStorage;
        }
    }
}