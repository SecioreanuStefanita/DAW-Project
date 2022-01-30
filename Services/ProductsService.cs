using Proiect.Data;
using Proiect.Models;
using Proiect.Models.DTOs;
using Proiect.Utilities;
using Proiect.Utilities.JWTUtilis;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Proiect.Core.IRepositories;
using System.Threading.Tasks;
using Proiect.Core.IConfig;
namespace Proiect.Services
{
    public class ProductsService : IProductsService
    {

        public ApplicationDbContext _ApplicationDbContext;
        private IJWTUtils _iJWtUtils;

        private readonly IUnitofWork _unitOfWork;


        public ProductsService(ApplicationDbContext ApplicationDbContext, IJWTUtils iJWtUtils,  IUnitofWork unitOfWork)
        {
            _ApplicationDbContext = ApplicationDbContext;
            _iJWtUtils = iJWtUtils;
            _unitOfWork = unitOfWork;
        }
    
    }
}
