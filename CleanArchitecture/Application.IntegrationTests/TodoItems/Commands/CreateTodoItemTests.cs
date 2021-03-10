using Application.Common.Exceptions;
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
    public class CreateTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTodoItemCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }
        [Test]
        public async Task ShouldCreateTodoItem()
        {
            var listId = await SendAsync(new CreateTodoCategoryCommand
            {
                CategoryTitle = "Belajar"
            });

            var command = new CreateTodoItemCommand
            {
                ListId = listId,
                ActivityTitle = "Belajar Membaca"
            };

            var itemId = await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.Should().NotBeNull();
            item.ListId.Should().Be(command.ListId);
            item.ActivityTitle.Should().Be(command.ActivityTitle);

        }
    }
}
