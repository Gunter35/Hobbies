namespace Hobbies.Infrastructure.Data.Constants
{
    public class DataConstants
    {
        public class BookGenre
        {
            public const int MaxGenreName = 50;
            public const int MinGenreName = 5;
        }
        public class Book
        {
            public const int MaxBookTitle = 50;
            public const int MinBookTitle = 5;

            public const int MaxBookAuthor = 50;
            public const int MinBookAuthor = 5;

            public const int MaxBookDescription = 5000;
            public const int MinBookDescription = 5;
        }
        public class MovieGenre
        {
            public const int MaxGenreName = 50;
            public const int MinGenreName = 5;
        }
        public class Movie
        {
            public const int MaxMovieTitle = 50;
            public const int MinMovieTitle = 5;

            public const int MaxMovieDirector = 50;
            public const int MinMovieDirector = 5;

            public const int MaxMovieDescription = 5000;
            public const int MinMovieDescription = 5;
        }
        public class User
        {
            public const int MaxUserUsername = 20;
            public const int MinUserUsername = 5;

            public const int MaxUserEmail = 60;
            public const int MinUserEmail = 10;

            public const int MaxUserPassword = 20;
            public const int MinUserPassword = 5;
        }
    }
}
