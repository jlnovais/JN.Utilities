namespace JN.Utilities.API.AuthorizationHandlers
{
    public static class ConstantsAuthentication
    {
        public enum UserRoles
        {
            Optimization,
            OptimizationReader
        }

        public enum AuthResult
        {
            Ok,
            NotFound = -1,
            InvalidUserOrPass = -2,
            NotAllowed = -3
        }
    }
}