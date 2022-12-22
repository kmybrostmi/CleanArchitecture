using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Helpers.Interfaces;
public interface IPasswordHelper
{
    string EncodePasswordMd5(string passWord);
}
