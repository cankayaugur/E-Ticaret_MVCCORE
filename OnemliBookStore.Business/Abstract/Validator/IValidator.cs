using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Business.Abstract.Validator
{
    public interface IValidator<T>
    {
        public string ErrorMessage { get; set; }
        bool Validate(T entity);
    }
}
