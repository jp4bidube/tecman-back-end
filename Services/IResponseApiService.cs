/**
 * Created: Daniel Quintal
 * Date: November, 10, 2021
 * Modified: Daniel Quintal
 * Date: November, 10, 2021
 * Modified: Daniel Quintal
 * Date: January, 03, 2022
 *
 * Responses Api interface file - Front
 * 
 **/

using Tecman.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.Services
{
    public interface IResponseApiService
    {
        public ApiMessage ResponseApi(int errorCode,object genericObject);

    }
}
