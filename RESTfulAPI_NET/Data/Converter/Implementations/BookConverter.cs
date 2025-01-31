﻿using RESTfulAPI_NET.Data.Converter.Contract;
using RESTfulAPI_NET.Data.VO;
using RESTfulAPI_NET.Model;

namespace RESTfulAPI_NET.Data.Converter
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null)
            {
                return null;
            }

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title,
            };
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null)
            {
                return null;
            }

            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title,
            };
        }

        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null)
            {
                return null;
            }

            return origin.Select(item => Parse(item)).ToList();
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null)
            {
                return null;
            }

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
