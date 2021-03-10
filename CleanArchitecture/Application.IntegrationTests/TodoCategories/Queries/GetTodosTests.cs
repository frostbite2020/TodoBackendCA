using Application.TodoCategories.Queries.GetTodoCategory;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.TodoCategories.Queries
{
    using static Testing;
    public class GetTodosTests : TestBase
    {
        [Test]
        public async Task ShouldReturnPriorityLevels()
        {
            var query = new GetTodosQuery();
            
            var result = await SendAsync(query);

            result.PriorityLevels.Should().NotBeEmpty();
        }
        [Test]
        public async Task ShouldReturnAllCategoryAndItems()
        {
            await AddAsync(new TodoCategory
            {
                CategoryTitle = "Shopping",
                Lists =
                    {
                        new TodoItem { ActivityTitle = "Apples", Done = true },
                        new TodoItem { ActivityTitle = "Milk", Done = true },
                        new TodoItem { ActivityTitle = "Bread", Done = true },
                        new TodoItem { ActivityTitle = "Toilet paper" },
                        new TodoItem { ActivityTitle = "Pasta" },
                        new TodoItem { ActivityTitle = "Tissues" },
                        new TodoItem { ActivityTitle = "Tuna" }
                    },
            });

            var query = new GetTodosQuery();

            var result = await SendAsync(query);

            result.Categories.Should().HaveCount(1);
            result.Categories.First().Lists.Should().HaveCount(7);
        }
    }
}
