using HaiphongTech.Application.Services.BaseServices.HardDelete;
using HaiphongTech.Application.Services.BaseServices.Recovery;
using HaiphongTech.Application.Services.BaseServices.SoftDelete;
using HaiphongTech.SharedKernel.Base;
using HaiphongTech.SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaiphongTech.API.Controllers.BaseApi
{
  /// <summary>
  /// Lớp trừu tượng chứa api về xóa, khôi phục và xóa vĩnh viễn
  /// </summary>
  /// <typeparam name="TService">LocalService tương ứng</typeparam>
  /// <typeparam name="TEntity">Entitiy tương ứng</typeparam>
  public abstract class BaseDeleteAndRecoveryController<TEntity> : ControllerBase
    where TEntity : EntityBase, IAuditable, IAggregateRoot
  {
      private readonly ISender _mediator;
      private readonly ICurrentUser _currentUser;

      protected abstract string SoftDeletePermission { get; }

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
            var command = new SoftDeleteCommand<TEntity>(ids, userId);
            var result = await _mediator.Send(command);
            return Ok(result);
      }

      [HttpDelete("hard-delete")]
      public async Task<IActionResult> HardDeleteAsync([FromBody] List<int> ids)
      {
        //   if (_currentUser.UserId is not int userId)
        //       return Unauthorized("Không xác định được người dùng.");
            int userId = 1;
          var command = new HardDeleteCommand<TEntity>(ids, userId);
          var result = await _mediator.Send(command);
          return Ok(result);
      }

      [HttpPatch("recovery/{id}")]
      public async Task<IActionResult> RecoveryAsync(int id)
      {
        //   if (_currentUser.UserId is not int userId)
        //       return Unauthorized("Không xác định được người dùng.");
            int userId = 1;
          var command = new RecoveryCommand<TEntity>(id, userId);
          var result = await _mediator.Send(command);
          return Ok(result);
      }
  }
}