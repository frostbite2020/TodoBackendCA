using Application.Common.Exceptions;
using Application.TodoCategories.Commands.DeleteTodoCategory;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Application.TodoCategories.Commands.CreateTodoCategory;

namespace Application.IntegrationTests.TodoCategories.Commands
{
    using static Testing;
    public class DeleteTodoCategoryTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoCategoryId()
        {
            var command = new DeleteTodoCategoryCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoCategory()
        {
            var categoryId = await SendAsync(new CreateTodoCategoryCommand
            {
                CategoryTitle = "Test"
            });

            await SendAsync(new DeleteTodoCategoryCommand
            {
                Id = categoryId
            });

            var list = await FindAsync<TodoCategory>(categoryId);
            list.Should().BeNull();
        }
    }
}
