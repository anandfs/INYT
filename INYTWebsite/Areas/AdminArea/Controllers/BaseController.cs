﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INYTWebsite.Code;
using Microsoft.AspNetCore.Mvc;

namespace INYTWebsite.Areas.AdminArea.Controllers
{
    public class BaseController : Controller
    {
        private ModelFactory _modelFactory;
        private Repository _repo;

        public BaseController(Repository repo)
        {
            _repo = repo;
        }

        protected Repository TheRepository
        {
            get
            {
                return _repo;
            }
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory();
                }
                return _modelFactory;
            }
        }
    }
}