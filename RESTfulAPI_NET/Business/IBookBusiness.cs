using RESTfulAPI_NET.Data.VO;
using RESTfulAPI_NET.Model;

namespace RESTfulAPI_NET.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
