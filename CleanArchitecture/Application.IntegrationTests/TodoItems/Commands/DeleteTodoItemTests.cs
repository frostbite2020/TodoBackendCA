using Application.Common.Exceptions;
using Application.TodoItems.Commands.DeleteTodoItem;
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
    public class DeleteTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireRealId()
        {
            var command = new DeleteTodoItemCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoItem()
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

            await SendAsync(new DeleteTodoItemCommand
            {
                Id = itemId
            });

            var list = await FindAsync<TodoItem>(categoryId);

            list.Should().BeNull();
        }
    }
}
