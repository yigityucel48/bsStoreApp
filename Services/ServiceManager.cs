﻿using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        public ServiceManager(IRepositoryManager manager,ILoggerService logger,IMapper mapper)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(manager,logger,mapper));
        }
        public IBookService BookService => _bookService.Value;
    }
}
