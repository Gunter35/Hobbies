//using Hobbies.Core.Contracts;
//using Hobbies.Core.Models.Comment;
//using Microsoft.AspNetCore.Mvc;

//namespace Hobbies.Controllers
//{
//    public class CommentsController : Controller
//    {
//        private readonly IBookService bookService;
//        private readonly IGameService gameService;
//        private readonly IMovieService movieService;
//        public CommentsController(IBookService _bookService,
//            IGameService _gameService,
//            IMovieService _movieService)
//        {
//            bookService = _bookService;
//            gameService = _gameService;
//            movieService = _movieService;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        [HttpGet]
//        public Task<IActionResult> AddBookComment()
//        {
//            if (User.Identity?.IsAuthenticated ?? false)
//            {
//                var model = new AddCommentViewModel()
//                {
//                    Description = "adawdawdawdaww"
//                };

//                return View(model);
//            }

//            return RedirectToAction("Index", "Comments");
//        }
//    }
//}
