using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnemliBookStore.Entity.Dtos
{
    public class BookDto
    {
        // validasyonları iş katmanında yapmak en doğru yaklaşım. Yarın bigün angular vb. projesi geldiğinde
        // dal, business vb. katmanlarıda kullanabiliriz o zaman validasyonları tekrar yazmak zorunda kalmayız.


        // client validation için jquery validation ve unobtrusive güzel olabilir butona tıklamadan uyarıyor.
        // server validation
        // request model olarak kullandığım için validasyon işlemlerini burda yapabilirm 
        public int BookId { get; set; }
        //[Display(Name = "Book Name", Prompt = "Enter book name")]
        //[Required(ErrorMessage = "Name is Required")]
        //[StringLength(100, MinimumLength = 1, ErrorMessage ="Name must be between 1 and 100 characters")]
        public string Name { get; set; }

        //[Display(Name = "Book Price", Prompt = "Enter a price")]
        //[Required(ErrorMessage = "Price is Required")]
        //[Range(1,10000, ErrorMessage = "Price must be between 1-10000 ")]
        public double? Price { get; set; }

        //[Display(Name = "Book Description", Prompt = "Enter description name")]
        //[StringLength(1000, MinimumLength = 5, ErrorMessage = "Description must be between 1 and 1000 characters")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Image is Required")]
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category> Categories { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}
