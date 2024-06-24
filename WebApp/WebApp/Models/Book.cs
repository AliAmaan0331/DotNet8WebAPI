using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? Author { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class Result
    {
        public List<Book>? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
