using SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.BaseServices.SoftDelete;
using Application.UseCases.BaseServices.HardDelete;
using Application.UseCases.BaseServices.Recovery;

namespace API.Controllers.BaseApi
{
  /// <summary>
  /// Lớp trừu tượng chứa api về xóa, khôi phục và xóa vĩnh viễn
  /// </summary>
  /// <typeparam name="TService">LocalService tương ứng</typeparam>
  /// <typeparam name="TEntity">Entitiy tương ứng</typeparam>
  public abstract class BaseDeleteAndRecoveryController<TSoftDelete, THardDelete, TRecovery> : ControllerBase
    where TSoftDelete : GenericSoftDeleteCommand
    where THardDelete : GenericHardDeleteCommand
    where TRecovery : GenericRecoveryCommand
  {
      private readonly ISender _mediator;
      private readonly ICurrentUser _currentUser;

      protected abstract Func<List<int>, int, TSoftDelete> CreateSoftDeleteCommand { get; }
      protected abstract Func<List<int>, int, THardDelete> CreateHardDeleteCommand { get; }
      protected abstract Func<int, int, TRecovery> CreateRecoveryCommand { get; }
      protected BaseDeleteAndRecoveryController(ISender mediator, ICurrentUser currentUser)
      {
          _mediator = mediator;
          _currentUser = currentUser;
      }

      [HttpDelete("soft-delete")]
      public async Task<IActionResult> SoftDeleteAsync([FromBody] List<int> ids)
      {
            //   if (_currentUser.UserId is not int userId)
            //       return Unauthorized("Không xác định được người dùng.");

            int userId = 1;
            var command = CreateSoftDeleteCommand(ids, userId);
            var result = await _mediator.Send(command);
            return Ok(result);
      }

      [HttpDelete("hard-delete")]
      public async Task<IActionResult> HardDeleteAsync([FromBody] List<int> ids)
      {
        //   if (_currentUser.UserId is not int userId)
        //       return Unauthorized("Không xác định được người dùng.");
            int userId = 1;
          var command = CreateHardDeleteCommand(ids, userId);
          var result = await _mediator.Send(command);
          return Ok(result);
      }

      [HttpPatch("recovery/{id}")]
      public async Task<IActionResult> RecoveryAsync(int id)
      {
        //   if (_currentUser.UserId is not int userId)
        //       return Unauthorized("Không xác định được người dùng.");
            int userId = 1;
          var command = CreateRecoveryCommand(id, userId);
          var result = await _mediator.Send(command);
          return Ok(result);
      }
  }
}