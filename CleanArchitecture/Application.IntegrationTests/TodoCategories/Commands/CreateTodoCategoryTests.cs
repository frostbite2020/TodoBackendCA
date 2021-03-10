using Application.Common.Exceptions;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using TodoList.Application.TodoCategories.Commands.CreateTodoCategory;

namespace Application.IntegrationTests.TodoCategories.Commands
{
    using static Testing;
    public class CreateTodoCategoryTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTodoCategoryCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            await SendAsync(new CreateTodoCategoryCommand
            {
                CategoryTitle = "Shopping"
            });

            var command = new CreateTodoCategoryCommand
            {
                CategoryTitle = "Shopping"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTodoList()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateTodoCategoryCommand
            {
                CategoryTitle = "Tasks"
            };

            var id = await SendAsync(command);

            var list = await FindAsync<TodoCategory>(id);

            list.Should().NotBeNull();
            list.CategoryTitle.Should().Be(command.CategoryTitle);
        }
    }
}
