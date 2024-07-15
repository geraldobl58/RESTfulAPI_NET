using RESTfulAPI_NET.Data.Converter;
using RESTfulAPI_NET.Data.Converter.Implementations;
using RESTfulAPI_NET.Data.VO;
using RESTfulAPI_NET.Model;
using RESTfulAPI_NET.Repository;
using System;

namespace RESTfulAPI_NET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            var personEntity = _converter.Parse(book);
            personEntity = _repository.Create(personEntity);

            return _converter.Parse(personEntity);
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            var personEntity = _converter.Parse(book);
            personEntity = _repository.Update(personEntity);

            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
