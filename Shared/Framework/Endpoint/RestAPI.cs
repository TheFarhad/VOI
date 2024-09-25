namespace Framework.Endpoint;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Core.Contract.Shared;
using Core.Contract.Application.Shared;
using Framework.Core.Application.Shared.Pipeline;
using Framework.Core.Application.Shared;

[ApiController]
[Route("api/[Controller]")]
public abstract class RestAPI : ControllerBase
{
    protected RequestController RequestPipeline => HttpContext.RequestPipeline();

    protected async Task<IResult> GetAsync<TRequest, TOutput>(TRequest source, CancellationToken token = default!)
        where TRequest : IRequest<TOutput>
    {
        var response = await RequestPipeline.SendAsync<TRequest, TOutput>(source, token);
        return Json(response);
    }

    protected async Task<IResult> PostAsync<TRequest, TOutput>(TRequest command, CancellationToken token = default!)
        where TRequest : IRequest<TOutput>
    {
        var response = await RequestPipeline.SendAsync<TRequest, TOutput>(command, token);
        return Json(response);
    }

    protected async Task<IResult> PutAsync<TRequest, TOutput>(TRequest command, CancellationToken token = default!)
        where TRequest : IRequest<TOutput>
    {
        var response = await RequestPipeline.SendAsync<TRequest, TOutput>(command, token);
        return Json(response);
    }

    protected async Task<IResult> DeleteAsync<TRequest, TOutput>(TRequest command, CancellationToken token = default!)
        where TRequest : IRequest<TOutput>
    {
        var response = await RequestPipeline.SendAsync<TRequest, TOutput>(command, token);
        return Json(response);
    }

    private IResult Json<TOutput>(Response<TOutput> source)
        => Results.Json<Response<TOutput>>(source);
}

#region Old

//using System.Net;
//using System.Windows.Input;
//using Microsoft.AspNetCore.Mvc;
//using Core.Contract.Shared;
//using Core.Contract.Application.Query;
//using Core.Contract.Application.Shared;
//using Core.Contract.Application.Command;

//public abstract class RestAPI : ControllerBase
//{
//    protected CommandPipeline CommandPipeline => HttpContext.CommandPipeline();
//    protected QueryPipeline QueryPipeline => HttpContext.QueryPipeline();

//    /*
//     در اینترسپتور نمونه سازی شود 
//     protected DomainEventPipeline EventPipeline => HttpContext.EventPipeline();
//    */

//    protected async Task<IActionResult> GetAsync<TQuery, TPayload>(TQuery source, CancellationToken token = default!)
//        where TQuery : IQuery<TPayload>
//    {
//        var response = await QueryPipeline.ExecuteAsync<TQuery, TPayload>(source, token);

//        return
//            response.Status is ReplyStatus.Ok
//            ?
//            StatusCode((int)HttpStatusCode.OK, response.Source)
//            :
//            response is { Status: ReplyStatus.NotFound, Source: null }
//            ?
//            StatusCode((int)HttpStatusCode.NoContent, source)
//            :
//             response is { Status: ReplyStatus.InvalidDomainState | ReplyStatus.InvalidDomainState }
//            ?
//            BadRequest(response.Errors)
//            :
//            BadRequest(response.Errors);
//    }

//    protected async Task<IActionResult> PostAsync<TCommand>(TCommand source, CancellationToken token = default!)
//        where TCommand : ICommand
//    {
//        var response = await CommandPipeline.ExecuteAsync(source, token);
//        return
//            response.Status is ReplyStatus.Ok
//            ?
//            StatusCode((int)HttpStatusCode.Created)
//            :
//             BadRequest(response.Errors);
//    }

//    protected async Task<IActionResult> PostAsync<TCommand, TPayload>(TCommand source, CancellationToken token = default!)
//        where TCommand : ICommand<TPayload>
//    {
//        var response = await CommandPipeline.ExecuteAsync<TCommand, TPayload>(source, token);

//        return
//            response.Status is ReplyStatus.Ok
//            ?
//            StatusCode((int)HttpStatusCode.Created, response.Source)
//            :
//             BadRequest(response.Errors);
//    }

//    protected async Task<IActionResult> PutAsync<TCommand>(TCommand source, CancellationToken token = default!)
//        where TCommand : ICommand
//    {
//        var response = await CommandPipeline.ExecuteAsync(source, token);

//        return
//         response.Status is ReplyStatus.Ok
//         ?
//         StatusCode((int)HttpStatusCode.OK)
//         :
//         response.Status is ReplyStatus.NotFound
//         ?
//          StatusCode((int)HttpStatusCode.NotFound, source)
//          :
//          BadRequest(response.Errors);
//    }

//    protected async Task<IActionResult> PutAsync<TCommand, TPayload>(TCommand source, CancellationToken token = default!)
//        where TCommand : ICommand<TPayload>
//    {
//        var response = await CommandPipeline.ExecuteAsync<TCommand, TPayload>(source, token);

//        return
//       response.Status is ReplyStatus.Ok
//       ?
//       StatusCode((int)HttpStatusCode.OK, response.Source)
//       :
//       response.Status is ReplyStatus.NotFound
//       ?
//        StatusCode((int)HttpStatusCode.NotFound, source)
//        :
//        BadRequest(response.Errors);
//    }

//    protected async Task<IActionResult> DeleteAsync<TCommand>(TCommand source, CancellationToken token = default!)
//        where TCommand : ICommand
//    {
//        var response = await CommandPipeline.ExecuteAsync(source, token);

//        return
//         response.Status is ReplyStatus.Ok
//         ?
//         StatusCode((int)HttpStatusCode.OK)
//         :
//         response.Status is ReplyStatus.NotFound
//         ?
//          StatusCode((int)HttpStatusCode.NotFound, source)
//          :
//          BadRequest(response.Errors);
//    }

//    protected async Task<IActionResult> DeleteAsync<TCommand, TPayload>(TCommand source, CancellationToken token = default!) where TCommand : ICommand<TPayload>
//    {
//        var response = await CommandPipeline.ExecuteAsync<TCommand, TPayload>(source, token);

//        return
//         response.Status is ReplyStatus.Ok
//         ?
//         StatusCode((int)HttpStatusCode.OK, response.Source)
//         :
//         response.Status is ReplyStatus.NotFound
//         ?
//          StatusCode((int)HttpStatusCode.NotFound, source)
//          :
//          BadRequest(response.Errors);
//    }
//}

#endregion




