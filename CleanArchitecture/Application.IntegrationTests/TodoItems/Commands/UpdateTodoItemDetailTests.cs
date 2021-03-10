using Application.Common.Exceptions;
using Application.TodoItems.Commands.UpdateTodoItemDetail;
using Application.TodoLists.Commands.CreateTodoList;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.TodoCategories.Commands.CreateTodoCategory;

namespace Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;
    public class UpdateTodoItemDetailTests : TestBase
    {
        [Test]
        public void ShouldBeRealId()
        {
            var command = new UpdateTodoItemDetailCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItemDetail()
        {
            var categoryId = await SendAsync(new CreateTodoCategoryCommand
            {
                CategoryTitle = "Belajar"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = categoryId,
                ActivityTitle = "Belajar Sejarah"
            });

            var command = new UpdateTodoItemDetailCommand()
            {
                Id = itemId,
                ListId = categoryId,
                Note = "Ini note ubah detail ya",
                Priority = PriorityLevel.Medium
            };

            await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.Should().NotBeNull();
            item.ListId.Should().Be(command.ListId);
            item.Note.Should().Be(command.Note);
            item.Priority.Should().Be(command.Priority);
        }
    }
}
