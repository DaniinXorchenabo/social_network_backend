// using Microsoft.AspNetCore.Mvc;
// using socialNetworkApp.api.controllers.messages;
// using socialNetworkApp.api.responses;
//
// namespace socialNetworkApp.api.controllers.posts;
//
//
// [ApiController]
// [Route("api/test")]
// [Produces("application/json")]
// public class  TestController : Controller
// {
//     [HttpGet("test_enum_shame")]
//     public ResultModel Test()
//     {
//         return new ResultModel
//             {
//                 Answer = new Dictionary<TypeAnswer, string>(){}
//             };
//     }
//
// }
//
// public class ResultModel
// {
//     public Dictionary<TypeAnswer, string> Answer { get; set; }
// }
//
// public enum TypeAnswer
// {
//     one, two
// }