using Tecman.ValueObject;

namespace Tecman.Services.Implementation
{
    public class ResponseApiService : IResponseApiService
    {

        // Gets the ErrorCode and selects Response JSON to returns Api Message.
        public ApiMessage ResponseApi(int errorCode, object genericObject)
        {
            string message;
            bool success;
            object result = null ;

            switch (errorCode){
                case 0:
                    message = "Success";
                    success = true;
                    result = genericObject;
                    break;
                // Generic Errors
                case -1:
                    message = "Invalid Request";
                    success = false;
                    result = genericObject;
                    break;
                case -2:
                    message = "Internal Server Error";
                    success = false;
                    result = genericObject;
                    break;
                case -100:
                    message = "Usuario não encontrado!";
                    success = false;
                    result = genericObject;
                    break;
                case -101:
                    message = "CPF já vinculado a outro funcionario!";
                    success = false;
                    result = genericObject;
                    break;
                case -401:
                    message = "Usuario ou senha inválido!";
                    success = false;
                    result = genericObject;
                    break;

                default:
                    message = "Error";
                    success = false;
                    break;
            }

            return new ApiMessage{
                Message = message,
                Success = success,
                ErrorCode = errorCode,
                Result = result,
            };
        }
    }
}