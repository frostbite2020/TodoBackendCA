using Application.Common.Exceptions;
using Application.TodoItems.Commands.UpdateTodoItem;
using Application.TodoLists.Commands.CreateTodoList;
using Domain.Entities;
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
    public class UpdateTodoItemsTests : TestBase
    {
        [Test]
        public void ShouldBeRealId()
        {
            var command = new UpdateTodoItemCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var categoryId = await SendAsync(new CreateTodoCategoryCommand
            {
                CategoryTitle = "Belajar"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                CategoryId = categoryId,
                ActivityTitle = "Belajar Sejarah"
            });

            var command = new UpdateTodoItemCommand
            {
                Id = itemId,
                Done = true
            };

            await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.Should().NotBeNull();
            item.Done.Should().Be(command.Done);
        }
    }
}
