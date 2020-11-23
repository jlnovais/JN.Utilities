namespace JN.Utilities.Core.Dto
{
    public class Result<T>
    {
        public bool Success;
        public int ErrorCode;
        public string ErrorDescription;
        public string ErrorDescription2;
        public T ReturnedObject;
    } 

    public class Result
    {
        public bool Success;
        public int ErrorCode;
        public string ErrorDescription;
        public string ErrorDescription2;
    }
}
