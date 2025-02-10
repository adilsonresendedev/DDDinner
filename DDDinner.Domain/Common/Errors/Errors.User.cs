using ErrorOr;

namespace DDDinner.Domain.Common.Errors
{
    public static class Errors
    {
        public static class User
        {
            public static Error EmailDuplicated => Error.Conflict("User.EmailIsDuplicated", "Provided email already exists.");
            public static Error EmailNotFound => Error.Unauthorized("User.EmailNotFound", "Provided user name already exists.");
            public static Error Unauthorized => Error.Unauthorized("User.Unauthorized", "Invalid user or password.");
        }
    }
}
