using Application.Common.Exceptions;
using Application.TodoCategories.Commands.UpdateTodoCategory;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using TodoList.Application.TodoCategories.Commands.CreateTodoCategory;

namespace Application.IntegrationTests.TodoCategories.Commands
{
    using static Testing;
    public class UpdateTodoCategoryTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoListId()
        {
            var command = new UpdateTodoCategoryCommand
            {
                Id = 99,
                CategoryTitle = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            var listId = await SendAsync(new CreateTodoCategoryCommand
            {
                CategoryTitle = "New List"
            });

            await SendAsync(new CreateTodoCategoryCommand
            {
                CategoryTitle = "Other List"
            });

            var command = new UpdateTodoCategoryCommand
            {
                Id = listId,
                CategoryTitle = "Other List"
            };

            FluentActions.Invoking(() =>
                SendAsync(command))
                    .Should().Throw<ValidationException>().Where(ex => ex.Errors.ContainsKey("CategoryTitle"))
                    .And.Errors["CategoryTitle"].Should().Contain("The specified title already exists.");
        }

        [Test]
        public async Task ShouldUpdateTodoList()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateTodoCategoryCommand
            {
                CategoryTitle = "New List"
            });

            var command = new UpdateTodoCategoryCommand
            {
                Id = listId,
                CategoryTitle = "Updated List Title"
            };

            await SendAsync(command);

            var list = await FindAsync<TodoCategory>(listId);

            list.CategoryTitle.Should().Be(command.CategoryTitle);
        }
    }
}